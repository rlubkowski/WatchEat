using WatchEat.Enums;

namespace WatchEat.Services.Interfaces
{
    public interface IBodyFatService
    {
        //Women:
        //(1.20 x BMI) + (0.23 x Age) - 5.4 = Body Fat Percentage

        //Men:
        //(1.20 x BMI) + (0.23 x Age) - 16.2 = Body Fat Percentage

        //Women:
        //20-40 yrs old: Underfat: under 21 percent, Healthy: 21-33 percent, Overweight: 33-39 percent, Obese: Over 39 percent
        //41-60 yrs old: Underfat: under 23 percent, Healthy: 23-35 percent, Overweight : 35-40 percent Obese: over 40 percent
        //61-79 yrs old: Underfat: under 24 percent, Healthy: 24-36 percent, Overweight: 36-42 percent, Obese: over 42 percent

        //Men:
        //20-40 yrs old: Underfat: under 8 percent, Healthy: 8-19 percent, Overweight: 19-25 percent, Obese: over 25 percent
        //41-60 yrs old: Underfat: under 11 percent, Healthy: 11-22 percent, Overweight: 22-27 percent, Obese: over 27 percent
        //61-79 yrs old: Underfat: under 13 percent, Healthy: 13-25 percent, Overweight: 25-30 percent, Obese: over 30 percent

        decimal EstimateBodyFatPercentage(decimal bmi, int age, Gender gender);

        BodyFatLevel GetBodyFatLevel(decimal bodyFat, int age, Gender gender);
    }
}
