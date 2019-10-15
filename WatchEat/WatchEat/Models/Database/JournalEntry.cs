using SQLite;
using System;
using WatchEat.Enums;

namespace WatchEat.Models.Database
{
    public class JournalEntry : EntityBase
    {
        int _type = default(int);
        public int Type
        {
            get => _type;
            set
            {
                _type = value;
                OnPropertyChanged(nameof(Type));
            }
        }
        
        [Ignore]
        public JournalEntryType EntryType
        {
            get => (JournalEntryType)Type;
            set
            {
                Type = (int)value;
                OnPropertyChanged(nameof(EntryType));
            }
        }

        DateTime _date = default(DateTime);
        public DateTime Date
        {
            get => _date; 
            set
            {
                _date = value;
                OnPropertyChanged(nameof(Date));
            }
        }

        int _reference = default(int);
        public int Reference
        {
            get => _reference;
            set
            {
                _reference = value;
                OnPropertyChanged(nameof(Type));
            }
        }
    }
}
