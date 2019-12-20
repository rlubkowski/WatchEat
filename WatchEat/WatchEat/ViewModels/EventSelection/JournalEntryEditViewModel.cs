using System;
using System.Windows.Input;
using WatchEat.Enums;
using WatchEat.Helpers;
using WatchEat.Models.Database;
using WatchEat.Resources;
using WatchEat.ViewModels.Interfaces;
using Xamarin.Forms;

namespace WatchEat.ViewModels.EventSelection
{
    public class JournalEntryEditViewModel : BaseViewModel, IValid
    {
        public JournalEntryEditViewModel(JournalEntry entry)
        {
            Entry = entry;
            Amount = Entry.Amount;
            Calories = Entry.Calories;
            Carbs = Entry.Carbs;
            Time = Entry.Time;
            Protein = Entry.Protein;
            Fat = Entry.Fat;
            Fiber = Entry.Fiber;
            Salt = Entry.Salt;            
            Weight = Entry.Weight;
            Name = Entry.Name;
            Title = AppResource.EditEntry;
        }

        public JournalEntry Entry { get; private set; }

        public bool ActivityVisible => Entry != null && Entry.EntryType == JournalEntryType.Activity;

        public bool WeightVisible => Entry != null && Entry.EntryType == JournalEntryType.Weight;

        public bool FoodVisible => Entry != null && Entry.EntryType == JournalEntryType.Food;

        public bool WaterVisible => Entry != null && Entry.EntryType == JournalEntryType.Water;

        public bool CaloriesVisible => Entry != null && (Entry.EntryType == JournalEntryType.Food || Entry.EntryType == JournalEntryType.Activity);

        public bool NameVisible => CaloriesVisible;

        TimeSpan _time;
        public TimeSpan Time
        {
            get => _time;
            set { SetProperty(ref _time, value); }
        }

        string _name = string.Empty;
        public string Name
        {
            get => _name;
            set { SetProperty(ref _name, value); }
        }

        decimal _calories = 0;
        public decimal Calories
        {
            get => _calories;
            set { SetProperty(ref _calories, value); }
        }

        decimal _carbs = 0;
        public decimal Carbs
        {
            get => _carbs;
            set { SetProperty(ref _carbs, value); }
        }

        decimal _protein = 0;
        public decimal Protein
        {
            get => _protein;
            set { SetProperty(ref _protein, value); }
        }

        decimal _fat = 0;
        public decimal Fat
        {
            get => _fat;
            set { SetProperty(ref _fat, value); }
        }

        decimal _fiber = 0;
        public decimal Fiber
        {
            get => _fiber;
            set { SetProperty(ref _fiber, value); }
        }

        decimal _salt = 0;
        public decimal Salt
        {
            get => _salt;
            set { SetProperty(ref _salt, value); }       
        }

        decimal _amount = 0;
        public decimal Amount
        {
            get => _amount;
            set { SetProperty(ref _amount, value); }
        }

        decimal _weight = 0;
        public decimal Weight
        {
            get => _weight;
            set { SetProperty(ref _weight, value); }
        }

        public ICommand Save => new AsyncCommand(async () =>
        {
            Entry.Amount = Amount;
            Entry.Calories = Calories;
            Entry.Carbs = Carbs;
            Entry.Time = Time;
            Entry.Protein = Protein;
            Entry.Fat = Fat;
            Entry.Fiber = Fiber;
            Entry.Salt = Salt;            
            Entry.Weight = Weight;
            Entry.Name = Name;

            if (!IsValid)
            {
                await DialogService.DisplayAlert(AppResource.Validation, AppResource.ValidationValuesIncorrect, AppResource.Cancel);
                return;
            }

            await DataStore.JournalEntries.Update(Entry);
            MessagingCenter.Send(this, CommandNames.JournalEntryUpdated, Entry);
            await Navigation.PopModalAsync();
        });

        public ICommand Remove => new AsyncCommand(async () =>
        {
            if (await DialogService.DisplayAlert(AppResource.ConfirmRemove, AppResource.DoYouWantToRemoveSelectedItem, AppResource.Yes, AppResource.No))
            {
                await DataStore.JournalEntries.Delete(Entry);
                MessagingCenter.Send(this, CommandNames.JournalEntryRemoved, Entry);
                await Navigation.PopModalAsync();
            }
        });

        public ICommand Cancel => new AsyncCommand(async () =>
        {
            await Navigation.PopModalAsync();
        });

        public bool IsValid
        {
            get
            {
                bool isValid = false;
                if (NameVisible || CaloriesVisible)
                {
                    isValid = Entry.Calories > 0 && !string.IsNullOrWhiteSpace(Entry.Name);
                }

                if(WaterVisible)
                {
                    isValid = Entry.Amount > 0;
                }

                if (WeightVisible)
                {
                    isValid = Entry.Weight > 0;
                }

                return isValid;
            }
        }
    }
}
