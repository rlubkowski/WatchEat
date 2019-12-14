namespace WatchEat.Models
{
    public class WeightChangeEstimationResult
    {
        public WeightChangeEstimationResult()
        {
            IsSafe = false;
        }

        public WeightChangeEstimationResult(int caloriesToReachGoal, int caloriesToMaintainGoal)
        {
            ReachCalories = caloriesToReachGoal;
            MaintainCalories = caloriesToMaintainGoal;
            IsSafe = true;
        }

        public int ReachCalories { get; private set; }

        public int MaintainCalories { get; private set; }

        public bool IsSafe { get; private set; }
    }
}
