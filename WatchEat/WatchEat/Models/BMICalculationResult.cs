using WatchEat.Enums;

namespace WatchEat.Models
{
    public class BMICalculationResult
    {
        public BMICalculationResult(decimal bmiValue, BMI bmiFactor)
        {
            BMIValue = bmiValue;
            BMIFactor = bmiFactor;
        }

        public decimal BMIValue { get; private set; }

        public BMI BMIFactor { get; private set; }
    }
}
