using WatchEat.Enums;
using WatchEat.Models;

namespace WatchEat.Services.Interfaces
{
    public interface INutritionService
    {
        decimal GetDailyCaloriesEstimation(decimal bmr, ActivityLevel activityFactor);

        NutritionRecommendation CalculateNutritionRecommendation(int calories);

        decimal BMRHarrisBenedict(decimal weight, decimal height, int age, Gender gender, bool imperialUnits = false);

        decimal BMRMifflinStJeor(decimal weight, decimal height, int age, Gender gender, bool imperialUnits = false);

        WeightChangeEstimationResult CalculateWeightChangeEstimation(int age, decimal height, decimal weight, Gender gender, ActivityLevel activityFactor, GoalTimePeriod timePeriod, int periodNumber, GoalType goalType, decimal weightToLoose, bool imperialUnits = false);

        decimal CalculateDailyWaterIntake(decimal weight, bool imperialUnits = false);
    }
}
