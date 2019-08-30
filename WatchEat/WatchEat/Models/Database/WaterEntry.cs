using System;

namespace WatchEat.Models.Database
{
    public class WaterEntry : EntityBase
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
    }
}
