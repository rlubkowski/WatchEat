using WatchEat.Enums;

namespace WatchEat.Services.Interfaces
{
    public interface IBodyFatService
    {
        decimal EstimateBodyFatPercentage(decimal bmi, int age, Gender gender);

        BodyFatLevel GetBodyFatLevel(decimal bodyFat, int age, Gender gender);
    }
}
