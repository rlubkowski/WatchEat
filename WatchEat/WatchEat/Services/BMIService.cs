using System;
using WatchEat.Enums;
using WatchEat.Models;
using WatchEat.Services.Interfaces;

namespace WatchEat.Services
{
    public class BMIService : IBMIService
    {
        /// <summary>
        /// Calculates Body Mass Index
        /// </summary>
        /// <param name="weight">Weight in kilograms (pounds)</param>
        /// <param name="height">Height in meters (inches)</param>
        /// <param name="imperialUnits">Indicator whether imperial units were used</param>
        /// <returns></returns>
        public BMICalculationResult Calculate(decimal weight, decimal height, bool imperialUnits = false)
        {
            //Metric BMI Formula
            //BMI = weight(kg) / [height(m)]2

            //Imperial BMI Formula
            //BMI = 703 × weight(lbs) / [height(in)]2

            double dWeight = Convert.ToDouble(weight);
            double dHeight = Convert.ToDouble(height);

            if (imperialUnits)
            {
                decimal bmiResult = Math.Round(Convert.ToDecimal(dWeight * 703 / Math.Pow(dHeight, 2)), 2);
                return new BMICalculationResult(bmiResult, ValueToFactor(bmiResult));
            }
            else
            {
                decimal bmiResult = Math.Round(Convert.ToDecimal(dWeight / Math.Pow(dHeight / 100, 2)), 2);
                return new BMICalculationResult(bmiResult, ValueToFactor(bmiResult));
            }
        }

        public BMI ValueToFactor(decimal bmi)
        {
            if (bmi < 15) return BMI.VerySeverelyUnderweight;
            else if (bmi >= 15m && bmi < 16m) return BMI.SeverelyUnderweight;
            else if (bmi >= 16m && bmi < 18.5m) return BMI.Underweight;
            else if (bmi >= 18.5m && bmi < 25m) return BMI.HealthyWeight;
            else if (bmi >= 25m && bmi < 30m) return BMI.Overweight;
            else if (bmi >= 30m && bmi < 35m) return BMI.ModeratelyObese;
            else if (bmi >= 35m && bmi < 40m) return BMI.SeverelyObese;
            else return BMI.VerySeverelyObese;
        }
    }
}
