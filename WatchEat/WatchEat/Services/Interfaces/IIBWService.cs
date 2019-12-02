using WatchEat.Enums;

namespace WatchEat.Services.Interfaces
{
    public interface IIBWService
    {
        decimal CalculateBroca(decimal height, Gender gender, bool imperialUnits = false);

        decimal CalculateDevine(decimal height, Gender gender, bool imperialUnits = false);

        decimal CalculateRobinson(decimal height, Gender gender, bool imperialUnits = false);

        decimal CalculateMiller(decimal height, Gender gender, bool imperialUnits = false);

        decimal CalculateHamwi(decimal height, Gender gender, bool imperialUnits = false);

        decimal CalculateLemmens(decimal height, bool imperialUnits = false);
    }
}
