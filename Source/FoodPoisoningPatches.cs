using HarmonyLib;
using RimWorld;

namespace Lilly.CheatWork
{
    [HarmonyPatch(typeof(CompFoodPoisonable), nameof(CompFoodPoisonable.SetPoisoned))]
    internal static class CompFoodPoisonable_SetPoisoned_Patch
    {
        private static bool Prefix(FoodPoisonCause newCause)
        {
            return !CheatWorkMod.PreventCookingFoodPoisoning ||
                (newCause != FoodPoisonCause.FilthyKitchen &&
                 newCause != FoodPoisonCause.IncompetentCook);
        }
    }
}
