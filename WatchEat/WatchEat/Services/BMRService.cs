using WatchEat.Enums;
using WatchEat.Helpers;
using WatchEat.Services.Interfaces;

namespace WatchEat.Services
{
    public class BMRService : IBMRService
    {
        public decimal BMRHarrisBenedict(decimal weight, decimal height, int age, Gender gender, bool imperialUnits = false)
        {
            //Imperial
            //Women: BMR = 655 + (4.35 x weight in pounds) + (4.7 x height in inches) - (4.7 x age in years)
            //Men: BMR = 66 + (6.23 x weight in pounds) + (12.7 x height in inches) - (6.8 x age in years)

            //Metric
            //Women: BMR = 655 + (9.6 x weight in kg) + (1.8 x height in cm) - (4.7 x age in years)
            //Men: BMR = 66 + (13.7 x weight in kg) + (5 x height in cm) - (6.8 x age in years)

            switch (gender)
            {
                case Gender.Male:
                    return CalculateMaleBMR(weight, height, age, imperialUnits);
                case Gender.Female:
                    return CalculateFemaleBMR(weight, height, age, imperialUnits);
                default: return 0;
            }
        }

        public decimal BMRMifflinStJeor(decimal weight, decimal height, int age, Gender gender, bool imperialUnits = false)
        {
            //Men: 10 x weight(kg) +6.25 x height(cm) – 5 x Age +5
            //Women: 10 x weight (kg) + 6.25 x height (cm) – 5 x Age – 161

            if (imperialUnits)
            {
                height = UnitConverter.FeetToCentimeters(height);
                weight = UnitConverter.PoundsToKilograms(weight);
            }
            if (gender == Gender.Male)
            {
                return 10m * weight + 6.25m * height - 5 * age + 5;
            }
            else if (gender == Gender.Female)
            {
                return 10m * weight + 6.25m * height - 5 * age + 161;
            }
            return 0;
        }
        
        public decimal CalculateWeightChangeEstimation(int age, decimal height, decimal weight, Gender gender, ActivityFactor activityFactor, TimePeriod timePeriod, int periodNumber, GoalType goalType, decimal weightToLoose, bool imperialUnits = false)
        {
            if (imperialUnits)
            {

            }
            var bmr = BMRHarrisBenedict(weight, height, age, gender, imperialUnits);
            return bmr * 1.9m;
        }

        private decimal CalculateFemaleBMR(decimal weight, decimal height, int age, bool imperialUnits)
        {
            if (imperialUnits)
            {
                return 655m + (4.35m * weight) + (4.7m * height) - (4.7m * age);
            }
            else
            {
                return 665m + (9.6m * weight) + (1.8m * height) - (4.7m * age);
            }
        }

        private decimal CalculateMaleBMR(decimal weight, decimal height, int age, bool imperialUnits)
        {
            if (imperialUnits)
            {
                return 66m + (6.23m * weight) + (12.7m * height) - (6.8m * age);
            }
            else
            {
                return 66m + (13.7m * weight) + (5m * height) - (6.8m * age);
            }
        }
    }
}
