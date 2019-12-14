using System;
using WatchEat.Enums;
using WatchEat.Helpers;
using WatchEat.Services.Interfaces;

namespace WatchEat.Services
{
	public class IBWService : IIBWService
	{
		//The Broca Index
		//Men: Ideal Body Weight(kg) = [Height(cm) - 100] - ([Height(cm) - 100] x 10%)
		//Women: Ideal Body Weight(kg) = [Height(cm) - 100] + ([Height(cm) - 100] x 15%)
		public decimal CalculateBroca(decimal height, Gender gender, bool imperialUnits = false)
		{
			if (imperialUnits)
			{
				height = UnitConverter.FeetToCentimeters(height);
			}
			if (gender == Gender.Male)
			{
				var result = (height - 100m) - ((height - 100m) * 0.10m);
				return imperialUnits ? UnitConverter.KilogramsToPounds(result) : result;
			}
			else if (gender == Gender.Female)
			{
				var result = (height - 100m) + ((height - 100m) * 0.15m);
				return imperialUnits ? UnitConverter.KilogramsToPounds(result) : result;
			}
			return 0;
		}

		//The Devine formula
		//Men: Ideal Body Weight(kg) = 50 kg + 2.3 kg per inch over 5 feet.
		//Women: Ideal Body Weight(kg) = 45.5 kg + 2.3 kg per inch over 5 feet.
		public decimal CalculateDevine(decimal height, Gender gender, bool imperialUnits = false)
		{
			if (!imperialUnits)
			{
				height = UnitConverter.CentimetersToFeet(height);
			}
			var remainingFeet = height - 5.0m;
			var remainingInches = UnitConverter.FeetToInches(remainingFeet);
			if (gender == Gender.Male)
			{
				var result = 50.0m + (remainingInches * 2.3m);
				return imperialUnits ? UnitConverter.KilogramsToPounds(result) : result;
			}
			else if (gender == Gender.Female)
			{
				var result = 45.5m + (remainingInches * 2.3m);
				return imperialUnits ? UnitConverter.KilogramsToPounds(result) : result;
			}
			return 0;
		}

		//The Hamwi formula:
		//Men: Ideal Body Weight(in kilograms) = 48 kg + 2.7 kg for each inch over 5 feet
		//Women: Ideal Body Weight(in kilograms) = 45.5 kg + 2.2 kg for each inch over 5 feet.		
		public decimal CalculateHamwi(decimal height, Gender gender, bool imperialUnits = false)
		{
			if(!imperialUnits)
			{
				height = UnitConverter.CentimetersToFeet(height);
			}
			var remainingFeet = height - 5.0m;
			var remainingInches = UnitConverter.FeetToInches(remainingFeet);
			if (gender == Gender.Male)
			{
				var result =  48.0m + (remainingInches * 2.7m);
				return imperialUnits ? UnitConverter.KilogramsToPounds(result) : result;
			}
			else if (gender == Gender.Female)
			{
				var result = 45.5m + (remainingInches * 2.2m);
				return imperialUnits ? UnitConverter.KilogramsToPounds(result) : result;
			}
			return 0;
		}

		//Ideal Body Weight(kg) = 22 x height^2 (meter)
		public decimal CalculateLemmens(decimal height, bool imperialUnits = false)
		{
			var heightInMeters = height / 100m;
			var result = Convert.ToDecimal(22 * Math.Pow(Convert.ToDouble(heightInMeters), 2));
			return imperialUnits ? UnitConverter.KilogramsToPounds(result) : result;
		}

		//The Miller formula:
		//Men: Ideal Body Weight(kg) = 56.2 kg + 1.41 kg per inch over 5 feet.
		//Women: Ideal Body Weight(kg) = 53.1 kg + 1.36 kg per inch over 5 feet.
		public decimal CalculateMiller(decimal height, Gender gender, bool imperialUnits = false)
		{
			if (!imperialUnits)
			{
				height = UnitConverter.CentimetersToFeet(height);
			}
			var remainingFeet = height - 5.0m;
			var remainingInches = UnitConverter.FeetToInches(remainingFeet);
			if (gender == Gender.Male)
			{
				var result = 56.2m + (remainingInches * 1.41m);
				return imperialUnits ? UnitConverter.KilogramsToPounds(result) : result;
			}
			else if (gender == Gender.Female)
			{
				var result = 53.1m + (remainingInches * 1.36m);
				return imperialUnits ? UnitConverter.KilogramsToPounds(result) : result;
			}
			return 0;
		}

		//Robinson Formula
		//Men: Ideal Body Weight(kg) = 52 kg + 1.9 kg per inch over 5 feet.
		//Women: Ideal Body Weight(kg) = 49 kg + 1.7 kg per inch over 5 feet.
		public decimal CalculateRobinson(decimal height, Gender gender, bool imperialUnits = false)
		{
			if (!imperialUnits)
			{
				height = UnitConverter.CentimetersToFeet(height);
			}
			var remainingFeet = height - 5.0m;
			var remainingInches = UnitConverter.FeetToInches(remainingFeet);
			if (gender == Gender.Male)
			{
				var result = 52.0m + (remainingInches * 1.9m);
				return imperialUnits ? UnitConverter.KilogramsToPounds(result) : result;
			}
			else if (gender == Gender.Female)
			{
				var result = 49.0m + (remainingInches * 1.7m);
				return imperialUnits ? UnitConverter.KilogramsToPounds(result) : result;
			}
			return 0;
		}
	}
}
