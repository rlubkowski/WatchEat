using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using WatchEat.Enums;
using WatchEat.Models.Database;

namespace WatchEat.Models
{
    public class JournalEntryModel : INotifyPropertyChanged
    {
        public JournalEntryModel(SelectionModel<FoodEntry> foodSelection)
        {
            Date = foodSelection.Time;
            Type = JournalEntryType.Food;
            EntityReference = foodSelection.SelectedEntity;
        }

        public JournalEntryModel(SelectionModel<ActivityEntry> activitySelection)
        {
            Date = activitySelection.Time;
            Type = JournalEntryType.Activity;
            EntityReference = activitySelection.SelectedEntity;
        }

        public JournalEntryModel(ActivityEntry trainingActivity, DateTime dateTime)
        {
            Date = dateTime;
            Type = JournalEntryType.Activity;
            EntityReference = trainingActivity;
        }

        public JournalEntryModel(FoodEntry food, DateTime dateTime)
        {
            Date = dateTime;
            Type = JournalEntryType.Food;
            EntityReference = food;
        }

        public JournalEntryModel(WaterEntry waterEntry)
        {
            Date = waterEntry.Date;
            Type = JournalEntryType.Water;
            EntityReference =  waterEntry;
        }

        public JournalEntryModel(WeightEntry weightEntry)
        {
            Date = weightEntry.Date;
            Type = JournalEntryType.Weight;
            EntityReference = weightEntry;
        }

        DateTime _date;
        public DateTime Date
        {
            get => _date;
            set
            {
                _date = value;
                OnPropertyChanged(nameof(Date));
            }
        }

        JournalEntryType _type;
        public JournalEntryType Type
        {
            get => _type;
            set
            {
                _type = value;
                OnPropertyChanged(nameof(Type));
            }
        }
        public EntityBase EntityReference { get; private set; }

        public JournalEntry ToEntity()
        {
            return new JournalEntry
            {
                Date = Date,
                EntryType = Type,             
                Reference = EntityReference.Id
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
