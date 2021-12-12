using HarmonyLib;

namespace ReadWhenHungry
{
    class Patches
    {
        [HarmonyPatch(typeof(Panel_Inventory_Examine), "MaybeAbortReadingWithHUDMessage")]
        [HarmonyPriority(Priority.Last)]
        class ReadWhenHungry
        {
            // true result means abort reading. False means you're allowed to read
            static bool Prefix(ref bool __result)
            {
                __result = ShouldPreventReading();
                return false; // Don't execute original method afterwards
            }

            private static bool HasAffliction(AfflictionType afflictionType)
            {
                return GameManager.GetConditionComponent().HasSpecificAffliction(afflictionType);
            }

            private static bool ShouldPreventReading()
            {
                var settings = ReadWhenHungrySettings.options;
                if (GameManager.GetWeatherComponent().IsTooDarkForAction(ActionsToBlock.Reading))
                {
                    HUDMessage.AddMessage(Localization.Get("GAMEPLAY_TooDarkToRead"), false);
                    return true;
                }
                if (!settings.allowReadingWhenTired && GameManager.GetFatigueComponent().IsExhausted())
                {
                    HUDMessage.AddMessage(Localization.Get("GAMEPLAY_TooTiredToRead"), false);
                    return true;
                }
                if (!settings.allowReadingWhenFreezing && GameManager.GetFreezingComponent().IsFreezing())
                {
                    HUDMessage.AddMessage(Localization.Get("GAMEPLAY_TooColdToRead"), false);
                    return true;
                }
                if (!settings.allowReadingWhenHungry && GameManager.GetHungerComponent().IsStarving())
                {
                    HUDMessage.AddMessage(Localization.Get("GAMEPLAY_TooHungryToRead"), false);
                    return true;
                }
                if (!settings.allowReadingWhenThirsty && GameManager.GetThirstComponent().IsDehydrated())
                {
                    HUDMessage.AddMessage(Localization.Get("GAMEPLAY_TooThirstyToRead"), false);
                    return true;
                }
                if (!settings.allowReadingWhenWounded && GameManager.GetConditionComponent().GetNormalizedCondition() < 0.1f)
                {
                    HUDMessage.AddMessage(Localization.Get("GAMEPLAY_TooWoundedToRead"), false);
                    return true;
                }
                if (GameManager.GetConditionComponent().HasNonRiskAffliction())
                {
                    if ((!settings.allowReadingWithCabinFever && HasAffliction(AfflictionType.CabinFever))
                        || (!settings.allowReadingWithFoodPoisoning && HasAffliction(AfflictionType.FoodPoisioning))
                        || (!settings.allowReadingWithDysentery && HasAffliction(AfflictionType.Dysentery))
                        || (!settings.allowReadingWithInfection && HasAffliction(AfflictionType.Infection))
                        || (!settings.allowReadingWithHypothermia && HasAffliction(AfflictionType.Hypothermia))
                        || (!settings.allowReadingWithIntestinalParasites && HasAffliction(AfflictionType.IntestinalParasites))

                        || (!settings.allowReadingWithBloodLoss && HasAffliction(AfflictionType.BloodLoss))
                        || (!settings.allowReadingWithBrokenRib && HasAffliction(AfflictionType.BrokenRib))
                        || (!settings.allowReadingWithBurns && HasAffliction(AfflictionType.Burns))
                        || (!settings.allowReadingWithSprainedAnkle && HasAffliction(AfflictionType.SprainedAnkle))
                        || (!settings.allowReadingWithSprainedWrist && HasAffliction(AfflictionType.SprainedWrist)))
                    {
                        HUDMessage.AddMessage(Localization.Get("GAMEPLAY_CannotReadWithAfflictions"), false);
                        return true;
                    }
                    return false;
                }
                return false;
            }
        }
    }
}
