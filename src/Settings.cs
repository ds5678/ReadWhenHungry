using ModSettings;

namespace ReadWhenHungry
{
    internal class ReadWhenHungrySettings : JsonModSettings
    {
        [Section("Basics")]
        [Name("Allow reading when hungry")]
        [Description("Allow reading when starving hungry (0 calories stored).")]
        public bool allowReadingWhenHungry = true;

        [Name("Allow reading when thirsty")]
        [Description("Allow reading when you are thirsty (0 water drank)")]
        public bool allowReadingWhenThirsty = false;

        [Name("Allow reading when freezing")]
        [Description("Allow reading when you are freezing (empty/red temperature bar)")]
        public bool allowReadingWhenFreezing = false;

        [Name("Allow reading when wounded")]
        [Description("Allow reading when you are below 10% health")]
        public bool allowReadingWhenWounded = false;

        [Name("Allow reading when exhausted")]
        [Description("Allow reading when you are completely exhausted / tired")]
        public bool allowReadingWhenTired = false;

        [Section("Afflictions")]
        [Name("Allow reading with cabin fever")]
        public bool allowReadingWithCabinFever = true;

        [Name("Allow reading with food poisoning")]
        public bool allowReadingWithFoodPoisoning = false;

        [Name("Allow reading with dysentery")]
        public bool allowReadingWithDysentery = false;

        [Name("Allow reading with infection")]
        public bool allowReadingWithInfection = false;

        [Name("Allow reading with hypothermia")]
        public bool allowReadingWithHypothermia = false;

        [Name("Allow reading with intestinal parasites")]
        public bool allowReadingWithIntestinalParasites = true;

        [Name("Allow reading when losing blood")]
        public bool allowReadingWithBloodLoss = false;

        [Name("Allow reading with broken ribs")]
        public bool allowReadingWithBrokenRib = true;

        [Name("Allow reading with burns")]
        public bool allowReadingWithBurns = false;

        [Name("Allow reading with a sprained ankle")]
        public bool allowReadingWithSprainedAnkle = true;

        [Name("Allow reading with a sprained wrist")]
        public bool allowReadingWithSprainedWrist = true;
    }
    internal static class Settings
    {
        internal static ReadWhenHungrySettings options;
        internal static void OnLoad()
        {
            options = new ReadWhenHungrySettings();
            options.AddToModSettings("Read When Hungry", MenuType.Both);
        }
    }
}
