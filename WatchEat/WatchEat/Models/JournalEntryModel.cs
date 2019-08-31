using System;
using System.ComponentModel;

namespace WatchEat.Models
{
    public class JournalEntryModel : INotifyPropertyChanged
    {
        public DateTime Date { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
