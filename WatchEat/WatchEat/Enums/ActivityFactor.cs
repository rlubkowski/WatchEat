using WatchEat.Helpers.Attributes;

namespace WatchEat.Enums
{
    public enum ActivityFactor
    {
        [LocalizedDescription(nameof(Sedentary))]
        Sedentary,              //little or no exercise
        [LocalizedDescription(nameof(LightlyActive))]
        LightlyActive,          //light exercise/sports 1-3 days/week
        [LocalizedDescription(nameof(ModeratetelyActive))]
        ModeratetelyActive,     //moderate exercise/sports 3-5 days/week
        [LocalizedDescription(nameof(VeryActive))]
        VeryActive,             //hard exercise/sports 6-7 days a week
        [LocalizedDescription(nameof(ExtraActive))]
        ExtraActive             //very hard exercise/sports & physical job or 2x training
    }
}
