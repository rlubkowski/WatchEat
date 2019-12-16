using Microcharts;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WatchEat.Enums;
using WatchEat.Helpers;
using WatchEat.Resources;
using WatchEat.ViewModels.Abstract;
using Xamarin.Forms;

namespace WatchEat.ViewModels.Statistics
{
    public class YearlyStatisticsViewModel : StatisticViewModel
    {
        public YearlyStatisticsViewModel()
        {
            Title = AppResource.Last12Months;
        }

        protected override IEnumerable<Tuple<DateTime, DateTime>> GetDateRanges()
        {
            var today = DateTime.Today;
            var lastDayOfMonth = DateTime.DaysInMonth(today.Year, today.Month);
            var result = new List<Tuple<DateTime, DateTime>>();
            for (int i = 0; i < 12; i++)
            {
                result.Add(new Tuple<DateTime, DateTime>(new DateTime(today.Year, today.Month, 1).AddMonths(-i), new DateTime(today.Year, today.Month, lastDayOfMonth).AddMonths(-i)));
            }
            result.Reverse();
            return result;
        }

        public override async Task InitializeAsync(INavigation navigation)
        {
            var consumedCalories = new List<Microcharts.Entry>();
            var burnedCalories = new List<Microcharts.Entry>();
            var water = new List<Microcharts.Entry>();
            var salt = new List<Microcharts.Entry>();
            var fiber = new List<Microcharts.Entry>();
            var fat = new List<Microcharts.Entry>();
            var protein = new List<Microcharts.Entry>();
            var carbs = new List<Microcharts.Entry>();
            var weight = new List<Microcharts.Entry>();
            foreach (var range in GetDateRanges())
            {
                var result = await DataStore.JournalEntries.Get(x => x.Date >= range.Item1 && x.Date < range.Item2, x => x.Date);
                var totalConsumedCalories = 0m;
                var totalBurnedCalories = 0m;
                var totalSalt = 0m;
                var totalWater = 0m;
                var totalFiber = 0m;
                var totalFat = 0m;
                var totalProtein = 0m;
                var totalCarbs = 0m;
                var lastWeight = 0m;
                bool hasWeight = false;

                foreach (var entry in result)
                {
                    switch (entry.EntryType)
                    {
                        case JournalEntryType.Activity:
                            totalBurnedCalories += entry.Calories;
                            break;
                        case JournalEntryType.Food:
                            totalSalt += entry.Salt;
                            totalConsumedCalories += entry.Calories;
                            totalFiber += entry.Fiber;
                            totalFat += entry.Fat;
                            totalProtein += entry.Protein;
                            totalCarbs += entry.Carbs;
                            break;
                        case JournalEntryType.Water:
                            totalWater += entry.Amount;
                            break;
                        case JournalEntryType.Weight:
                            lastWeight = entry.Weight;
                            hasWeight = true;
                            break;
                    }
                }
                totalWater = Math.Round(UnitConverter.MililitersToLiters(totalWater), 2);
                if (hasWeight)
                {
                    weight.Add(new Microcharts.Entry(Convert.ToSingle(lastWeight))
                    {
                        Color = SKColor.Parse(CHART_COLOR),
                        Label = range.Item1.ToString("MMM yyyy"),
                        ValueLabel = Math.Round(lastWeight, 2).ToString()
                    });
                }
                consumedCalories.Add(new Microcharts.Entry(Convert.ToSingle(totalConsumedCalories))
                {
                    Color = SKColor.Parse(CHART_COLOR),
                    Label = range.Item1.ToString("MMM yyyy"),
                    ValueLabel = Math.Round(totalConsumedCalories).ToString()
                });
                burnedCalories.Add(new Microcharts.Entry(Convert.ToSingle(totalBurnedCalories))
                {
                    Color = SKColor.Parse(CHART_COLOR),
                    Label = range.Item1.ToString("MMM yyyy"),
                    ValueLabel = Math.Round(totalBurnedCalories).ToString()
                });
                water.Add(new Microcharts.Entry(Convert.ToSingle(totalWater))
                {
                    Color = SKColor.Parse(CHART_COLOR),
                    Label = range.Item1.ToString("MMM yyyy"),
                    ValueLabel = Math.Round(totalWater).ToString()
                });
                salt.Add(new Microcharts.Entry(Convert.ToSingle(totalSalt))
                {
                    Color = SKColor.Parse(CHART_COLOR),
                    Label = range.Item1.ToString("MMM yyyy"),
                    ValueLabel = Math.Round(totalSalt).ToString()
                });
                fiber.Add(new Microcharts.Entry(Convert.ToSingle(totalFiber))
                {
                    Color = SKColor.Parse(CHART_COLOR),
                    Label = range.Item1.ToString("MMM yyyy"),
                    ValueLabel = Math.Round(totalFiber).ToString()
                });
                protein.Add(new Microcharts.Entry(Convert.ToSingle(totalProtein))
                {
                    Color = SKColor.Parse(CHART_COLOR),
                    Label = range.Item1.ToString("MMM yyyy"),
                    ValueLabel = Math.Round(totalProtein).ToString()
                });
                carbs.Add(new Microcharts.Entry(Convert.ToSingle(totalCarbs))
                {
                    Color = SKColor.Parse(CHART_COLOR),
                    Label = range.Item1.ToString("MMM yyyy"),
                    ValueLabel = Math.Round(totalCarbs).ToString()
                });
                fat.Add(new Microcharts.Entry(Convert.ToSingle(totalFat))
                {
                    Color = SKColor.Parse(CHART_COLOR),
                    Label = range.Item1.ToString("MMM yyyy"),
                    ValueLabel = Math.Round(totalFat).ToString()
                });
            }

            BurnedCalories = new LineChart
            {
                Entries = burnedCalories,
                LabelTextSize = FontSize,
            };
            ConsumedCalories = new LineChart
            {
                Entries = consumedCalories,
                LabelTextSize = FontSize,
            };
            Water = new LineChart
            {
                Entries = water,
                LabelTextSize = FontSize,
            };
            Weight = new LineChart
            {
                Entries = weight,
                LabelTextSize = FontSize,
            };
            Carbs = new LineChart
            {
                Entries = carbs,
                LabelTextSize = FontSize,
            };
            Protein = new LineChart
            {
                Entries = protein,
                LabelTextSize = FontSize,
            };
            Fat = new LineChart
            {
                Entries = fat,
                LabelTextSize = FontSize,
            };
            Salt = new LineChart
            {
                Entries = salt,
                LabelTextSize = FontSize,
            };
            Fiber = new LineChart
            {
                Entries = fiber,
                LabelTextSize = FontSize,
            };
        }    
    }
}
