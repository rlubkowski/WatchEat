using System;
using System.Windows.Input;
using WatchEat.Helpers;
using WatchEat.Helpers.MethodExtensions;
using WatchEat.Models;
using Xamarin.Forms;

namespace WatchEat.ViewModels.EventSelection
{
    public class WaterViewModel : BaseViewModel
    {
        public WaterViewModel(DateTime date)
        {   
            Title = "New Water Entry";            
            SelectedDate = date;
            SelectedTime = DateTime.Now.ToTimespan();
        }

        DateTime _date;
        public DateTime SelectedDate
        {
            get => _date;
            set => SetProperty(ref _date, value);
        }

        TimeSpan _time;
        public TimeSpan SelectedTime
        {
            get => _time;
            set
            {
                SetProperty(ref _time, value);
                var date = SelectedDate;
                SelectedDate = new DateTime(date.Year, date.Month, date.Day, value.Hours, value.Minutes, value.Seconds);
            }
        }

        decimal _amount = default(decimal);
        public decimal Amount
        {
            get => _amount;
            set { SetProperty(ref _amount, value); }
        }

        public ICommand Add => new AsyncCommand(async () =>
        {
            MessagingCenter.Send(this, CommandNames.AddWaterEntry, new WaterEntryModel(Amount, SelectedDate));
            await Navigation.PopModalToRootAsync();
        });

        public ICommand Cancel => new AsyncCommand(async () =>
        {
            await Navigation.PopModalAsync();
        });
    }
}
