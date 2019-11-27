using WatchEat.Enums;
using WatchEat.Models;
using WatchEat.Services.Interfaces;

namespace WatchEat.Services
{
    public class BMRService : IBMRService
    {
        public BMRCalculationResult Calculate(decimal weight, decimal height, int age, Gender gender, bool imperialUnits = false)
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
                    break;
                case Gender.Female:
                    break;
                default:
                    break;
            }

            return new BMRCalculationResult();
        }

        private decimal CalculateFemale(decimal weight, decimal height, int age, bool imperialUnits)
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

        private decimal CalculateMale(decimal weight, decimal height, int age, bool imperialUnits)
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
