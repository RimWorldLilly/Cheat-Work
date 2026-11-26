using UnityEngine;
using Verse;

namespace Lilly.CheatWork
{
    public sealed class CheatWorkMod : Mod
    {
        private readonly CheatWorkSettings settings;

        public static CheatWorkMod Instance { get; private set; }

        public CheatWorkMod(ModContentPack content) : base(content)
        {
            Instance = this;
            settings = GetSettings<CheatWorkSettings>();
        }

        public override void DoSettingsWindowContents(Rect inRect)
        {
            Listing_Standard listing = new Listing_Standard();
            listing.Begin(inRect);

            listing.Label("즉시 작업 처리");
            listing.CheckboxLabeled("즉시 채굴 및 광맥 채굴", ref settings.InstantMining);
            listing.CheckboxLabeled("즉시 건물 해체", ref settings.InstantDeconstruction);
            listing.CheckboxLabeled("즉시 바닥 및 기초 제거", ref settings.InstantFloorRemoval);
            listing.CheckboxLabeled("즉시 식물 제초", ref settings.InstantPlantCutting);
            listing.CheckboxLabeled("즉시 식물 수확", ref settings.InstantPlantHarvest);
            listing.CheckboxLabeled("즉시 파종", ref settings.InstantSowing);
            listing.CheckboxLabeled("지붕 없음 구역 지정 시 즉시 지붕 제거", ref settings.InstantRoofRemoval);

            listing.Gap();
            listing.Label("추가 기능");
            listing.CheckboxLabeled("조리로 인한 식중독 방지", ref settings.PreventCookingFoodPoisoning);

            listing.Gap();
            listing.Label("결과물 처리");
            listing.CheckboxLabeled("작업 결과물을 저장고로 즉시 이동", ref settings.AutoStoreWorkOutputs);
            listing.Label("작업 중 생성된 운반 가능 아이템을 허용하는 저장고로 이동합니다. 저장 공간이 없으면 생성 위치에 남습니다.");

            listing.End();
        }

        public override string SettingsCategory()
        {
            return "Cheat Work";
        }

        public static bool InstantMining => IsEnabled(s => s.InstantMining);
        public static bool InstantDeconstruction => IsEnabled(s => s.InstantDeconstruction);
        public static bool InstantFloorRemoval => IsEnabled(s => s.InstantFloorRemoval);
        public static bool InstantPlantCutting => IsEnabled(s => s.InstantPlantCutting);
        public static bool InstantPlantHarvest => IsEnabled(s => s.InstantPlantHarvest);
        public static bool InstantSowing => IsEnabled(s => s.InstantSowing);
        public static bool InstantRoofRemoval => IsEnabled(s => s.InstantRoofRemoval);
        public static bool PreventCookingFoodPoisoning => IsEnabled(s => s.PreventCookingFoodPoisoning);

        private static bool IsEnabled(System.Func<CheatWorkSettings, bool> selector)
        {
            return Instance == null || Instance.settings == null || selector(Instance.settings);
        }

        public static bool AutoStoreWorkOutputs
        {
            get
            {
                return Instance == null || Instance.settings == null ||
                    Instance.settings.AutoStoreWorkOutputs;
            }
        }
    }

    public sealed class CheatWorkSettings : ModSettings
    {
        public bool InstantMining = true;
        public bool InstantDeconstruction = true;
        public bool InstantFloorRemoval = true;
        public bool InstantPlantCutting = true;
        public bool InstantPlantHarvest = true;
        public bool InstantSowing = true;
        public bool InstantRoofRemoval = true;
        public bool PreventCookingFoodPoisoning = true;
        public bool AutoStoreWorkOutputs = true;

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look(ref InstantMining, "instantMining", true);
            Scribe_Values.Look(ref InstantDeconstruction, "instantDeconstruction", true);
            Scribe_Values.Look(ref InstantFloorRemoval, "instantFloorRemoval", true);
            Scribe_Values.Look(ref InstantPlantCutting, "instantPlantCutting", true);
            Scribe_Values.Look(ref InstantPlantHarvest, "instantPlantHarvest", true);
            Scribe_Values.Look(ref InstantSowing, "instantSowing", true);
            Scribe_Values.Look(ref InstantRoofRemoval, "instantRoofRemoval", true);
            Scribe_Values.Look(ref PreventCookingFoodPoisoning, "preventCookingFoodPoisoning", true);
            Scribe_Values.Look(ref AutoStoreWorkOutputs, "autoStoreWorkOutputs", true);
        }
    }
}