using System;
using System.Windows.Input;
using WatchEat.Helpers;
using WatchEat.Models.Database;
using Xamarin.Forms;

namespace WatchEat.ViewModels.EventSelection
{
    public class WeightViewModel : BaseViewModel
    {
        public WeightViewModel(DateTime date)
        {
            Title = "New Weight Entry";
            IsEditView = false;
            SelectedTime = DateTime.Now.ToTimespan();
            Weight = new WeightEntry            
            {
                Date = date
            };
        }

        public WeightViewModel(WeightEntry water)
        {
            Title = "Edit Weight Entry";
            IsEditView = true;
            SelectedTime = DateTime.Now.ToTimespan();
            Weight = water;
        }

        WeightEntry _weight;
        public WeightEntry Weight
        {
            get => _weight;
            set
            {
                SetProperty(ref _weight, value);
                SelectedTime = new TimeSpan(value.Date.Hour, value.Date.Minute, value.Date.Second);
            }
        }

        TimeSpan _time;
        public TimeSpan SelectedTime
        {
            get => _time;
            set
            {
                SetProperty(ref _time, value);
                var date = Weight.Date;
                Weight.Date = new DateTime(date.Year, date.Month, date.Day, value.Hours, value.Minutes, value.Seconds);
            }
        }

        public bool IsEditView { get; private set; }

        public ICommand Save => new AsyncCommand(async () =>
        {
            if (IsEditView)
            {
                MessagingCenter.Send(this, CommandNames.EditWeightEntry, Weight);
            }
            else
            {
                MessagingCenter.Send(this, CommandNames.AddWeightEntry, Weight);
            }
            await Navigation.PopModalToRootAsync();
        });

        public ICommand Cancel => new AsyncCommand(async () =>
        {
            await Navigation.PopModalAsync();
        });

        public ICommand Remove => new AsyncCommand(async () =>
        {
            if (await DisplayAlert("Confirm Remove", "Do you want to remove selected product?", "Yes", "No"))
            {
                MessagingCenter.Send(this, CommandNames.RemoveWeightEntry, Weight);
            }
            await Navigation.PopModalAsync();
        });
    }
}
