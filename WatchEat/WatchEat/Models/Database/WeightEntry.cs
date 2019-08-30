using System;

namespace WatchEat.Models.Database
{
    public class WeightEntry : EntityBase
    {
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
