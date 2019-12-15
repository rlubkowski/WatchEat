using WatchEat.Enums;

namespace WatchEat.Models
{
    public class UserGoalModel
    {
        public UserGoalModel(GoalType goalType, GoalTimePeriod goalTimePeriod, int goalPeriod, decimal loseGainWeight)
        {
            GoalType = goalType;
            GoalTimePeriod = goalTimePeriod;
            GoalPeriod = goalPeriod;
            LoseGainWeight = loseGainWeight;
        }
        
        public GoalType GoalType { get; private set; }

        public GoalTimePeriod GoalTimePeriod { get; private set; }
        
        public int GoalPeriod { get; private set; }

        public decimal LoseGainWeight { get; private set; }
    }
}
