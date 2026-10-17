using System;
using System.Collections.Generic;
using HarmonyLib;
using Verse;

namespace Core_SK.Patches;

/// <summary>
/// Automatically initializes the Harmony patch on RimWorld startup.
/// </summary>
[StaticConstructorOnStartup]
public static class TruncateHeightOptimizationInit
{
    static TruncateHeightOptimizationInit()
    {
        var harmony = new Harmony("hsk.core_sk.truncate_fix");
        harmony.PatchAll();
    }
}

/// <summary>
/// Fixes massive UI FPS drop in Architect InfoBox (Verse.GenText.TruncateHeight).
/// 
/// Root Cause:
/// Vanilla trims overflowing text 1 character at a time in a while loop calling Text.CalcHeight per char with cache=null,
/// causing 30,000+ GUI font measurements per second when building descriptions overflow available rect height.
/// 
/// Solution:
/// 1. O(log N) binary search for text truncation instead of O(N) linear iteration.
/// 2. Global per-dimension cache (text + width + height + font) so repeated OnGUI calls take 0ms.
/// </summary>
public static class Patch_GenText_TruncateHeight
{
    private struct CacheKey : IEquatable<CacheKey>
    {
        public readonly string Text;
        public readonly float Width;
        public readonly float Height;
        public readonly GameFont Font;
        private readonly int Hash;

        public CacheKey(string text, float width, float height, GameFont font)
        {
            Text = text;
            Width = width;
            Height = height;
            Font = font;
            Hash = (text?.GetHashCode() ?? 0) ^ (width.GetHashCode() << 2) ^ (height.GetHashCode() >> 2) ^ ((int)font << 6);
        }

        public bool Equals(CacheKey other) => Width == other.Width && Height == other.Height && Font == other.Font && Text == other.Text;
        public override bool Equals(object obj) => obj is CacheKey other && Equals(other);
        public override int GetHashCode() => Hash;
    }

    private static readonly Dictionary<CacheKey, string> StringCache = new(512);
    private static readonly Dictionary<CacheKey, TaggedString> TaggedStringCache = new(512);

    [HarmonyPatch(typeof(GenText), nameof(GenText.TruncateHeight), typeof(string), typeof(float), typeof(float), typeof(Dictionary<string, string>))]
    public static class Patch_String
    {
        public static bool Prefix(string str, float width, float height, ref string __result)
        {
            if (string.IsNullOrEmpty(str)) return true;

            var key = new CacheKey(str, width, height, Text.Font);
            if (StringCache.TryGetValue(key, out var cached))
            {
                __result = cached;
                return false;
            }

            if (Text.CalcHeight(str, width) <= height)
            {
                StringCache[key] = str;
                __result = str;
                return false;
            }

            int low = 0;
            int high = str.Length;
            string best = "";

            while (low <= high)
            {
                int mid = (low + high) / 2;
                string candidate = GenText.AddEllipses(str.Substring(0, mid));
                if (Text.CalcHeight(candidate, width) <= height)
                {
                    best = candidate;
                    low = mid + 1;
                }
                else
                {
                    high = mid - 1;
                }
            }

            StringCache[key] = best;
            __result = best;
            return false;
        }
    }

    [HarmonyPatch(typeof(GenText), nameof(GenText.TruncateHeight), typeof(TaggedString), typeof(float), typeof(float), typeof(Dictionary<string, TaggedString>))]
    public static class Patch_TaggedString
    {
        public static bool Prefix(TaggedString str, float width, float height, ref TaggedString __result)
        {
            if (str.NullOrEmpty()) return true;

            var key = new CacheKey(str.RawText, width, height, Text.Font);
            if (TaggedStringCache.TryGetValue(key, out var cached))
            {
                __result = cached;
                return false;
            }

            string stripped = str.RawText.StripTags();
            if (Text.CalcHeight(stripped, width) <= height)
            {
                TaggedStringCache[key] = str;
                __result = str;
                return false;
            }

            int low = 0;
            int high = str.RawText.Length;
            TaggedString best = TaggedString.Empty;

            while (low <= high)
            {
                int mid = (low + high) / 2;
                string subRaw = str.RawText.Substring(0, mid);
                string candidateStripped = GenText.AddEllipses(subRaw.StripTags());
                if (Text.CalcHeight(candidateStripped, width) <= height)
                {
                    best = GenText.AddEllipses(subRaw);
                    low = mid + 1;
                }
                else
                {
                    high = mid - 1;
                }
            }

            TaggedStringCache[key] = best;
            __result = best;
            return false;
        }
    }
}
