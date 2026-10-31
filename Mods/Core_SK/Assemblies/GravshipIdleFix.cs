using System.Reflection;
using HarmonyLib;
using RimWorld;
using Verse;
using Verse.AI;

namespace HSK.GravshipIdleFix
{
    // Common Sense safe-wander writes an off-map cell into targetC, so its goto transpiler truncates the
    // path to zero cells and the job ends instantly: idle pawns on a gravship never wander again.
    // Clearing targetC restores a plain StartPath; the destination is then kept inside the ship hull.
    [StaticConstructorOnStartup]
    public static class Bootstrap
    {
        static Bootstrap()
        {
            new Harmony("HSK.GravshipIdleFix").Patch(
                AccessTools.Method(typeof(JobGiver_Wander), "TryGiveJob"),
                postfix: new HarmonyMethod(AccessTools.Method(typeof(Bootstrap), "Postfix"), -1, null, new[] { "avilmask.CommonSense" }));
        }

        static void Postfix(Pawn pawn, ref Job __result)
        {
            if (pawn == null || __result == null || __result.def != JobDefOf.GotoWander)
            {
                return;
            }
            Map map = pawn.MapHeld;
            if (map == null || !map.Biome.inVacuum)
            {
                return;
            }
            Building_GravEngine engine = GravshipUtility.GetPlayerGravEngine_NewTemp(map);
            if (engine == null)
            {
                return;
            }
            IntVec3 offMap = __result.targetC.Cell;
            if (offMap.IsValid && !offMap.InBounds(map))
            {
                __result.targetC = LocalTargetInfo.Invalid;
            }
            if (Onboard(__result.targetA.Cell, map, engine, pawn))
            {
                return;
            }
            IntVec3 spot;
            __result.targetA = GravshipUtility.TryFindSpotOnGravship(pawn, engine, out spot) ? spot : pawn.PositionHeld;
        }

        // Vanilla IsOnboardGravshipNREs on cells with no room, so filter those out first.
        static bool Onboard(IntVec3 cell, Map map, Building_GravEngine engine, Pawn pawn)
        {
            return cell.InBounds(map) && cell.GetRoom(map) != null && GravshipUtility.IsOnboardGravship(cell, engine, pawn);
        }
    }
}
