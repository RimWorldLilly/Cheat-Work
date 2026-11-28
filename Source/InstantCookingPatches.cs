using System;
using HarmonyLib;
using RimWorld;
using Verse;
using Verse.AI;

namespace Lilly.CheatWork
{
    // Complete cooking bills as soon as their ingredients have reached the work
    // stage. The vanilla tick action still performs bill notifications and
    // advances to the normal recipe finalization toil.
    [HarmonyPatch(typeof(Toils_Recipe), nameof(Toils_Recipe.DoRecipeWork))]
    internal static class Toils_Recipe_DoRecipeWork_InstantCooking_Patch
    {
        [HarmonyPostfix]
        private static void Postfix(Toil __result)
        {
            if (__result == null || __result.tickIntervalAction == null)
                return;

            Action<int> vanillaTickIntervalAction = __result.tickIntervalAction;
            __result.tickIntervalAction = delta =>
            {
                Pawn actor = __result.actor;
                if (CheatWorkMod.InstantCooking &&
                    actor != null &&
                    actor.jobs != null &&
                    actor.jobs.curJob != null &&
                    actor.jobs.curJob.RecipeDef != null &&
                    actor.jobs.curJob.RecipeDef.workSkill == SkillDefOf.Cooking)
                {
                    JobDriver_DoBill billDriver = actor.jobs.curDriver as JobDriver_DoBill;
                    if (billDriver != null)
                    {
                        // Let the vanilla work tick handle Notify_PawnDidWork,
                        // Notify_BillWorkFinished and ReadyForNextToil.
                        billDriver.workLeft = 0f;
                    }
                }

                vanillaTickIntervalAction(delta);
            };
        }
    }
}
