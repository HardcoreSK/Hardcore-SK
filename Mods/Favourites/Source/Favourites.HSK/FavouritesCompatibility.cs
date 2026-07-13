using HarmonyLib;
using RimworldFavourites;
using UnityEngine;
using Verse;

namespace RimworldFavourites.HSK
{
    [StaticConstructorOnStartup]
    internal static class FavouritesCompatibility
    {
        static FavouritesCompatibility()
        {
            new Harmony("hardcore.sk.favourites.stackcompat").PatchAll();
        }
    }

    [HarmonyPatch(typeof(ThingWithComps), nameof(ThingWithComps.CanStackWith))]
    internal static class Patch_ThingWithComps_CanStackWith
    {
        private static void Postfix(ThingWithComps __instance, Thing other, ref bool __result)
        {
            if (!__result)
            {
                return;
            }

            CompFavouritable first = __instance.TryGetComp<CompFavouritable>();
            CompFavouritable second = other.TryGetComp<CompFavouritable>();
            if (first == null || second == null)
            {
                return;
            }

            if (first.Favourited != second.Favourited || first.Junk != second.Junk)
            {
                __result = false;
                return;
            }

            if (first.Favourited && first.StarColour != second.StarColour)
            {
                __result = false;
                return;
            }

            if (first.Junk && first.BinColour != second.BinColour)
            {
                __result = false;
            }
        }
    }

    [HarmonyPatch(typeof(ThingWithComps), nameof(ThingWithComps.SplitOff))]
    internal static class Patch_ThingWithComps_SplitOff
    {
        private static void Postfix(ThingWithComps __instance, Thing __result)
        {
            if (__result == null || ReferenceEquals(__instance, __result))
            {
                return;
            }

            CompFavouritable source = __instance.TryGetComp<CompFavouritable>();
            CompFavouritable split = __result.TryGetComp<CompFavouritable>();
            if (source == null || split == null)
            {
                return;
            }

            split.StarColour = source.StarColour;
            split.BinColour = source.BinColour;
            split.Favourited = source.Favourited;
            split.Junk = source.Junk;
        }
    }
}
