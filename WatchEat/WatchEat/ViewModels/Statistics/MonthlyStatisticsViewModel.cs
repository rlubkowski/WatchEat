using System;
using System.Collections.Generic;
using WatchEat.Resources;
using WatchEat.ViewModels.Abstract;

namespace WatchEat.ViewModels.Statistics
{
    public class MonthlyStatisticsViewModel : StatisticViewModel
    {
        public MonthlyStatisticsViewModel()
        {
            Title = AppResource.Last30Days;
        }

        protected override int FontSize => 25;
        protected override IEnumerable<Tuple<DateTime, DateTime>> GetDateRanges()
        {
            var result = new List<Tuple<DateTime, DateTime>>();
            for (int i = 0; i < 30; i++)
            {
                result.Add(new Tuple<DateTime, DateTime>(DateTime.Today.AddDays(-i), DateTime.Today.AddDays(-i + 1)));
            }
            result.Reverse();
            return result;
        }
    }
}
