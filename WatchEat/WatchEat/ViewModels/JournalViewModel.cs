using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using WatchEat.Enums;
using WatchEat.Models;
using Xamarin.Forms;

namespace WatchEat.ViewModels
{
    public class JournalViewModel : BaseViewModel
    {
        public JournalViewModel()
        {
            SelectedDay = DateTime.Now;
            Activities = new ObservableCollection<DailyActivityModel>();
            for (int i = 0; i < 50; i++)
            {
                Activities.Add(new DailyActivityModel());
            }
        }

        public ICommand Navigate => new Command<NavigationDirection>((direction) =>
        {
            if (direction == NavigationDirection.Forward)
            {
                SelectedDay = SelectedDay.AddDays(1);
            }
            else if (direction == NavigationDirection.Backward)
            {
                SelectedDay = SelectedDay.AddDays(-1);
            }
        });

        public ICommand AddActivity => new Command(() =>
        {

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
