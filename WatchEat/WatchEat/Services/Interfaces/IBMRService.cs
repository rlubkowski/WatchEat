using WatchEat.Enums;

namespace WatchEat.Services.Interfaces
{
    public interface IBMRService
    {
        decimal BMRHarrisBenedict(decimal weight, decimal height, int age, Gender gender, bool imperialUnits = false);

        decimal BMRMifflinStJeor(decimal weight, decimal height, int age, Gender gender, bool imperialUnits = false);

        decimal CalculateWeightChangeEstimation(int age, decimal height, decimal weight, Gender gender, ActivityFactor activityFactor, TimePeriod timePeriod, int periodNumber, GoalType goalType, decimal weightToLoose, bool imperialUnits = false);
    }
}
