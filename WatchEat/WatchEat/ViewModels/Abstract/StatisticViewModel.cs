using Microcharts;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WatchEat.Enums;
using WatchEat.Helpers;
using Xamarin.Forms;

namespace WatchEat.ViewModels.Abstract
{
    public abstract class StatisticViewModel : BaseViewModel
    {
        protected const string CHART_COLOR = "#0ABF58";

        LineChart _consumedCalories;
        public LineChart ConsumedCalories
        {
            get => _consumedCalories;
            set { SetProperty(ref _consumedCalories, value); }
        }

        LineChart _burnedCalories;
        public LineChart BurnedCalories
        {
            get => _burnedCalories;
            set { SetProperty(ref _burnedCalories, value); }
        }

        LineChart _weight;
        public LineChart Weight
        {
            get => _weight;
            set { SetProperty(ref _weight, value); }
        }

        LineChart _water;
        public LineChart Water
        {
            get => _water;
            set { SetProperty(ref _water, value); }
        }

        LineChart _salt;
        public LineChart Salt
        {
            get => _salt;
            set { SetProperty(ref _salt, value); }
        }

        LineChart _fiber;
        public LineChart Fiber
        {
            get => _fiber;
            set { SetProperty(ref _fiber, value); }
        }

        LineChart _fat;
        public LineChart Fat
        {
            get => _fat;
            set { SetProperty(ref _fat, value); }
        }

        LineChart _protein;
        public LineChart Protein
        {
            get => _protein;
            set { SetProperty(ref _protein, value); }
        }

        LineChart _carbs;
        public LineChart Carbs
        {
            get => _carbs;
            set { SetProperty(ref _carbs, value); }
        }
        
        protected virtual IEnumerable<Tuple<DateTime, DateTime>> GetDateRanges()
        {
            throw new NotImplementedException();
        }

        protected virtual int FontSize => 35;

        public override async Task InitializeAsync(INavigation navigation)
        {
            await base.InitializeAsync(navigation);
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
                        Label = range.Item1.ToShortDateString(),
                        ValueLabel = Math.Round(lastWeight, 2).ToString()
                    });
                }
                consumedCalories.Add(new Microcharts.Entry(Convert.ToSingle(totalConsumedCalories))
                {
                    Color = SKColor.Parse(CHART_COLOR),
                    Label = range.Item1.ToShortDateString(),
                    ValueLabel = Math.Round(totalConsumedCalories).ToString()
                });
                burnedCalories.Add(new Microcharts.Entry(Convert.ToSingle(totalBurnedCalories))
                {
                    Color = SKColor.Parse(CHART_COLOR),
                    Label = range.Item1.ToShortDateString(),
                    ValueLabel = Math.Round(totalBurnedCalories).ToString()
                });
                water.Add(new Microcharts.Entry(Convert.ToSingle(totalWater))
                {
                    Color = SKColor.Parse(CHART_COLOR),
                    Label = range.Item1.ToShortDateString(),
                    ValueLabel = Math.Round(totalWater).ToString()
                });
                salt.Add(new Microcharts.Entry(Convert.ToSingle(totalSalt))
                {
                    Color = SKColor.Parse(CHART_COLOR),
                    Label = range.Item1.ToShortDateString(),
                    ValueLabel = Math.Round(totalSalt).ToString()
                });
                fiber.Add(new Microcharts.Entry(Convert.ToSingle(totalFiber))
                {
                    Color = SKColor.Parse(CHART_COLOR),
                    Label = range.Item1.ToShortDateString(),
                    ValueLabel = Math.Round(totalFiber).ToString()
                });
                protein.Add(new Microcharts.Entry(Convert.ToSingle(totalProtein))
                {
                    Color = SKColor.Parse(CHART_COLOR),
                    Label = range.Item1.ToShortDateString(),
                    ValueLabel = Math.Round(totalProtein).ToString()
                });
                carbs.Add(new Microcharts.Entry(Convert.ToSingle(totalCarbs))
                {
                    Color = SKColor.Parse(CHART_COLOR),
                    Label = range.Item1.ToShortDateString(),
                    ValueLabel = Math.Round(totalCarbs).ToString()
                });
                fat.Add(new Microcharts.Entry(Convert.ToSingle(totalFat))
                {
                    Color = SKColor.Parse(CHART_COLOR),
                    Label = range.Item1.ToShortDateString(),
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
