using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace WatchEat.Models
{
    public class DailyActivityModel : INotifyPropertyChanged
    {
        public DateTime Date { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
