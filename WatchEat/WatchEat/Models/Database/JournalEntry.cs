using System;

namespace WatchEat.Models.Database
{
    public class JournalEntry : EntityBase
    {
        public int Type { get; set; }

        DateTime _date = default(DateTime);
        public DateTime Date
        {
            get { return _date; }
            set
            {
                _date = value;
                OnPropertyChanged(nameof(Date));
            }
        }
    }
}
