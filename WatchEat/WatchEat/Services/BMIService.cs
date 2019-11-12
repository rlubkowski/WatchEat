using UnitsNet;
using WatchEat.Models;
using WatchEat.Services.Interfaces;

namespace WatchEat.Services
{
    public class BMIService : IBMIService
    {
        //Metric BMI Formula
        //BMI = weight(kg) / [height(m)]2

        //Imperial BMI Formula
        //BMI = 703 × weight(lbs) / [height(in)]2

        public BMICalculationResult Calculate(Mass weight, Length height)
        {
            double bmiResult = weight.Kilograms / (height.Meters * height.Meters);
            return new BMICalculationResult();
        }
    }
}
