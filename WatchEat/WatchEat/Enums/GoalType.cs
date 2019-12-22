using WatchEat.Helpers.Attributes;

namespace WatchEat.Enums
{
    public enum GoalType : int
    {
        [LocalizedDescription(nameof(Maintain))]
        Maintain,
        [LocalizedDescription(nameof(Lose))]
        Lose,
        [LocalizedDescription(nameof(Gain))]
        Gain
    }
}
