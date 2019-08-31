using System;
using System.Windows.Input;
using WatchEat.Helpers;
using WatchEat.Models.Database;
using Xamarin.Forms;

namespace WatchEat.ViewModels.EventSelection
{
    public class WaterActivityPageViewModel : BaseViewModel
    {
        public WaterActivityPageViewModel(DateTime date)
        {
            Title = "New Water Entry";
            IsEditView = false;
            Water = new WaterEntry
            {
                Date = date
            };
        }

        public WaterActivityPageViewModel(WaterEntry water)
        {
            Title = "Edit Water Entry";
            IsEditView = true;
            Water = water;
        }

        WaterEntry _water;
        public WaterEntry Water
        {
            get => _water;
            set
            {
                SetProperty(ref _water, value);
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
                var date = Water.Date;
                Water.Date = new DateTime(date.Year, date.Month, date.Day, value.Hours, value.Minutes, value.Seconds);
            }
        }

        public bool IsEditView { get; private set; }

        public ICommand Save => new AsyncCommand(async () =>
        {
            if (IsEditView)
            {
                MessagingCenter.Send(this, CommandNames.EditWaterEntry, Water);
            }
            else
            {
                MessagingCenter.Send(this, CommandNames.AddWaterEntry, Water);
            }
            await Navigation.PopModalAsync();
        });

        public ICommand Cancel => new AsyncCommand(async () =>
        {
            await Navigation.PopModalAsync();
        });

        public ICommand Remove => new AsyncCommand(async () =>
        {
            if (await DisplayAlert("Confirm Remove", "Do you want to remove selected product?", "Yes", "No"))
            {
                MessagingCenter.Send(this, CommandNames.RemoveWaterEntry, Water);
            }
            await Navigation.PopModalAsync();
        });
    }
}
