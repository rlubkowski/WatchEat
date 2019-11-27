namespace WatchEat.Models
{
    public class BMRCalculationResult
    {
        public BMRCalculationResult(decimal bmr)
        {
            BMR = bmr;
        }

        public decimal BMR { get; private set; }
    }
}
