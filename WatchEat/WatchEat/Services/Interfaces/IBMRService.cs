using WatchEat.Enums;
using WatchEat.Models;

namespace WatchEat.Services.Interfaces
{
    public interface IBMRService
    {
        BMRCalculationResult Calculate(decimal weight, decimal height, int age, Gender gender, bool imperialUnits = false);
    }
}
