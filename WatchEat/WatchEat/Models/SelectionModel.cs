using System;
using WatchEat.Adapters;
using WatchEat.Enums;
using WatchEat.Models.Database;
using WatchEat.Models.Database.Abstract;
using WatchEat.Models.Interfaces;

namespace WatchEat.Models
{
    public class SelectionModel<T> : IJournalEntryAdapter where T : EntityBase, ISelectableEntity
    {
        public SelectionModel(T selectedEntity, DateTime date)
        {
            SelectedEntity = selectedEntity;
            Date = date;
        }

        public T SelectedEntity { get; private set; }

        public DateTime Date { get; private set; }

        public JournalEntry ToJournalEntry()
        {
            switch (SelectedEntity)
            {
                case FoodEntry f:
                    return new JournalEntry
                    {
                        EntryType = JournalEntryType.Food,
                        Date = Date,
                        Name = f.Name,
                        Calories = f.Calories,
                        Carbs = f.Carbs,
                        Fat = f.Fat,
                        Fiber = f.Fiber,
                        Salt = f.Salt,
                        Protein = f.Protein
                    };
                case ActivityEntry a:
                    return new JournalEntry
                    {
                        EntryType = JournalEntryType.Activity,
                        Date = Date,
                        Name = a.Name,
                        Calories = a.Calories                        
                    };
                default:
                    throw new ArgumentException($"{nameof(SelectedEntity)} is of unsupported type!");
            }
        }
    }
}
