using System;
using WatchEat.Adapters;
using WatchEat.Models.Database;

namespace WatchEat.Models
{
    public class WaterEntryModel : IJournalEntryAdapter
    {
        public WaterEntryModel(decimal amount, DateTime date)
        {
            Amount = amount;
            Date = date; 
        }

        public decimal Amount { get; private set; }

        public DateTime Date { get; private set; }

        public JournalEntry ToJournalEntry()
        {
            return new JournalEntry
            {
                Amount = Amount,
                Name = "Water Enrty",
                EntryType = Enums.JournalEntryType.Water,
                Date = Date
            };
        }
    }
}
