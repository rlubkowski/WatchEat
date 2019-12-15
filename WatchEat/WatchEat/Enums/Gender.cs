using WatchEat.Helpers.Attributes;

namespace WatchEat.Enums
{
    public enum Gender : int
    {
        [LocalizedDescription(nameof(Male))]
        Male,
        [LocalizedDescription(nameof(Female))]
        Female
    }
}
