using WatchEat.Models;

namespace WatchEat.Services.Interfaces
{
    public interface IBMRService
    {
        BMRCalculationResult Calculate(decimal weight, decimal height, int age, bool imperialUnits = false);
    }
}
