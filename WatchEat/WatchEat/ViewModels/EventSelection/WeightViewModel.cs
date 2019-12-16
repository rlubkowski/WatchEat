using System;
using System.Windows.Input;
using WatchEat.Helpers;
using WatchEat.Helpers.MethodExtensions;
using WatchEat.Models;
using WatchEat.Resources;
using Xamarin.Forms;

namespace WatchEat.ViewModels.EventSelection
{
    public class WeightViewModel : BaseViewModel
    {
        public WeightViewModel(DateTime dateTime)
        {
            Title = AppResource.WeightEntry;            
            SelectedDate = dateTime.AppendCurrentTime();            
        }

        DateTime _date;
        public DateTime SelectedDate
        {
            get => _date;
            set => SetProperty(ref _date, value);
        }

        public TimeSpan SelectedTime
        {
            get => SelectedDate.ToTimespan();
            set
            {
                var date = SelectedDate;
                SelectedDate = new DateTime(date.Year, date.Month, date.Day, value.Hours, value.Minutes, value.Seconds);
                OnPropertyChanged(nameof(SelectedTime));
            }
        }

        decimal _weight = default(decimal);
        public decimal Weight 
        {
            get => _weight;
            set { SetProperty(ref _weight, value); }
        }

        bool _isWeightValid = default(bool);
        public bool IsWeightValid
        {
            get => _isWeightValid;
            set { SetProperty(ref _isWeightValid, value); }
        }

        public ICommand Add => new AsyncCommand(async () =>
        {
            if (IsWeightValid)
            {
                MessagingCenter.Send(this, CommandNames.AddWeightEntry, new WeightEntryModel(Weight, SelectedDate));
                await Navigation.PopModalToRootAsync();
            }
            else
            {
                await DialogService.DisplayAlert(AppResource.Validation, AppResource.ValidationWeight, AppResource.Cancel);
            }
        });

        public ICommand Cancel => new AsyncCommand(async () =>
        {
            await Navigation.PopModalAsync();
        });
    }
}
