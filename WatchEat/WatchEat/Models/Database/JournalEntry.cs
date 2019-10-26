using SQLite;
using System;
using WatchEat.Enums;
using WatchEat.Models.Database.Abstract;

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

        string _name = string.Empty;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        decimal _calories = default(decimal);
        public decimal Calories
        {
            get { return _calories; }
            set
            {
                _calories = value;
                OnPropertyChanged(nameof(Calories));
            }
        }

        decimal _carbs = default(decimal);
        public decimal Carbs
        {
            get { return _carbs; }
            set
            {
                _carbs = value;
                OnPropertyChanged(nameof(Carbs));
            }
        }

        decimal _protein = default(decimal);
        public decimal Protein
        {
            get { return _protein; }
            set
            {
                _protein = value;
                OnPropertyChanged(nameof(Protein));
            }
        }

        decimal _fat = default(decimal);
        public decimal Fat
        {
            get { return _fat; }
            set
            {
                _fat = value;
                OnPropertyChanged(nameof(Fat));
            }
        }

        decimal _fiber = default(decimal);
        public decimal Fiber
        {
            get { return _fiber; }
            set
            {
                _fiber = value;
                OnPropertyChanged(nameof(Fiber));
            }
        }

        decimal _salt = default(decimal);
        public decimal Salt
        {
            get { return _salt; }
            set
            {
                _salt = value;
                OnPropertyChanged(nameof(Salt));
            }
        }

        decimal _amount = default(decimal);
        public decimal Amount
        {
            get { return _amount; }
            set
            {
                _amount = value;
                OnPropertyChanged(nameof(Amount));
            }
        }

        decimal _weight = default(decimal);
        public decimal Weight
        {
            get { return _weight; }
            set
            {
                _weight = value;
                OnPropertyChanged(nameof(Weight));
            }
        }
    }
}
