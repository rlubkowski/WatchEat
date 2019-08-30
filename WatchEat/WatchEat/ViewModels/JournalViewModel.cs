using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using WatchEat.Enums;
using WatchEat.Helpers;
using WatchEat.Models;
using WatchEat.Views.EventSelection;
using Xamarin.Forms;

namespace WatchEat.ViewModels
{
    public class JournalViewModel : BaseViewModel
    {
        public JournalViewModel()
        {
            SelectedDay = DateTime.Now;
            Activities = new ObservableCollection<DailyActivityModel>();
        }

        public ICommand Navigate => new AsyncCommand(async (direction) =>
        {
            if (IsBusy)
                return;
            IsBusy = true;
            if ((NavigationDirection)direction == NavigationDirection.Forward)
            {
                SelectedDay = SelectedDay.AddDays(1);
            }
            else if ((NavigationDirection)direction == NavigationDirection.Backward)
            {
                SelectedDay = SelectedDay.AddDays(-1);
            }
            IsBusy = false;
        });

        public ICommand AddActivity => new AsyncCommand(async() =>
        {
            if (IsBusy)
                return;
            await Navigation.PushModalAsync(new NavigationPage(new ActivitySelectionPage()));
        });

        private ObservableCollection<DailyActivityModel> _activities;
        public ObservableCollection<DailyActivityModel> Activities
        {
            get => _activities;
            set => SetProperty(ref _activities, value);
        }

        private DateTime _selectedDay;
        public DateTime SelectedDay
        {
            get => _selectedDay;
            set => SetProperty(ref _selectedDay, value);
        }
    }
}
