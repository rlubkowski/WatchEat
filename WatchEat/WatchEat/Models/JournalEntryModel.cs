using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using WatchEat.Enums;
using WatchEat.Models.Database;

namespace WatchEat.Models
{
    public class JournalEntryModel : INotifyPropertyChanged
    {
        public JournalEntryModel(FoodSelectionModel foodSelection)
        {
            Date = foodSelection.Time;
            Type = JournalEntryType.Food;
            Food = foodSelection.Food;
        }

        public JournalEntryModel(TrainingActivitySelectionModel trainingSelection)
        {
            Date = trainingSelection.Time;
            Type = JournalEntryType.TrainingActivity;
            TrainingActivity = trainingSelection.TrainingActivity;
        }

        public JournalEntryModel(WaterEntry waterEntry)
        {
            Date = waterEntry.Date;
            Type = JournalEntryType.Water;
            Water =  waterEntry;
        }

        public JournalEntryModel(WeightEntry weightEntry)
        {
            Date = weightEntry.Date;
            Type = JournalEntryType.Weight;
            Weight = weightEntry;
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
        public Food Food { get; private set; }

        public TrainingActivity TrainingActivity { get; private set; }

        public WaterEntry Water { get; private set; }

        public WeightEntry Weight { get; private set; }

        public JournalEntry ToEntity()
        {
            int reference = 0;
            switch (Type)
            {
                case JournalEntryType.TrainingActivity:
                    reference = TrainingActivity.Id;
                    break;
                case JournalEntryType.Weight:
                    reference = Weight.Id;
                    break;
                case JournalEntryType.Food:
                    reference = Food.Id;
                    break;
                case JournalEntryType.Water:
                    reference = Water.Id;
                    break;                
            }

            return new JournalEntry
            {
                Date = Date,
                Type = (int)Type,             
                Reference = reference
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
