using System;
using WatchEat.Adapters;
using WatchEat.Models.Database;

namespace WatchEat.Models
{
    public class WeightEntryModel : IJournalEntryAdapter
    {
        public WeightEntryModel(decimal weight, DateTime date)
        {
            Weight = weight;
            Date = date;
        }

        public decimal Weight { get; private set; }

        public DateTime Date { get; private set; }

        public JournalEntry ToJournalEntry()
        {
            return new JournalEntry
            {
                Name = string.Empty,
                Weight = Weight,
                EntryType = Enums.JournalEntryType.Weight,
                Date = Date
            };
        }
    }
}
