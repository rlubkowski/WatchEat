﻿using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using WatchEat.Helpers;
using WatchEat.Helpers.MethodExtensions;
using WatchEat.Models;
using WatchEat.Models.Database;
using WatchEat.Resources;
using Xamarin.Forms;

namespace WatchEat.ViewModels.EventSelection
{
    public class ActivitySelectionViewModel : BaseViewModel
    {
        public ActivitySelectionViewModel(DateTime dateTime)
        {
            SelectedDate = dateTime.AppendCurrentTime();            
            Activities = new ObservableCollection<ActivityEntry>();
            Title = AppResource.SelectActivity;
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

        public ObservableCollection<ActivityEntry> Activities { get; private set; }

        public ICommand ActivitySelected => new AsyncCommand(async (param) =>
        {
            var activity = (ActivityEntry)param;
            MessagingCenter.Send(this, CommandNames.ActivitySelected, new SelectionModel<ActivityEntry>(activity, SelectedDate));
            await Navigation.PopModalToRootAsync();
        });

        public ICommand Cancel => new AsyncCommand(async () =>
        {
            await Navigation.PopModalAsync();
        });

        public async override Task InitializeAsync(INavigation navigation)
        {
            await base.InitializeAsync(navigation);
            Activities.Clear();
            Activities.AddRange(await DataStore.ActivityEntries.Get());
        }
    }
}
