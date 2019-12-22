using WatchEat.Helpers.Attributes;

namespace WatchEat.Enums
{
    public enum GoalTimePeriod : int
    {
        [LocalizedDescription(nameof(Days))]
        Days,
        [LocalizedDescription(nameof(Weeks))]
        Weeks,
        [LocalizedDescription(nameof(Months))]
        Months,
        [LocalizedDescription(nameof(Years))]
        Years
    }
}
