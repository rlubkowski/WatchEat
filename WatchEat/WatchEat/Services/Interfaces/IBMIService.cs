using WatchEat.Enums;
using WatchEat.Models;

namespace WatchEat.Services.Interfaces
{
    public interface IBMIService
    {
        BMICalculationResult Calculate(decimal weight, decimal height, bool imperialUnits = false);

        BMI ValueToFactor(decimal bmi);
    }
}
