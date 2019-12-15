using WatchEat.Helpers.Attributes;

namespace WatchEat.Enums
{
    public enum BodyFatLevel : int
    {
        [LocalizedDescription(nameof(Underfat))]
        Underfat,
        [LocalizedDescription(nameof(Healthy))]
        Healthy,
        [LocalizedDescription(nameof(Overweight))]
        Overweight,
        [LocalizedDescription(nameof(Obese))]
        Obese,
        [LocalizedDescription(nameof(NoData))]
        NoData
    }
}
