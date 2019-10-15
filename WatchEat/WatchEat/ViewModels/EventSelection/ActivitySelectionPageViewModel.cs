using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using WatchEat.Helpers;
using WatchEat.Models;
using Xamarin.Forms;

namespace WatchEat.ViewModels.EventSelection
{
    public class ActivitySelectionPageViewModel : BaseViewModel
    {
        public ActivitySelectionPageViewModel(DateTime dateTime)
        {
            SelectedDate = dateTime;
            SelectedTime = DateTime.Now.ToTimespan();
            Activities = new ObservableCollection<Models.Database.ActivityEntry>();
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

        public ObservableCollection<Models.Database.ActivityEntry> Activities { get; private set; }

        public ICommand ActivitySelected => new AsyncCommand(async (param) =>
        {
            var activity = (Models.Database.ActivityEntry)param;
            MessagingCenter.Send(this, CommandNames.ActivitySelected, new ActivitySelectionModel(activity, SelectedDate));
            await Navigation.PopModalToRootAsync();
        });

        public ICommand Cancel => new AsyncCommand(async () =>
        {
            await Navigation.PopModalAsync();
        });

        public async override Task InitializeAsync(INavigation navigation)
        {
            if (!IsInitialized)
            {
                foreach (var activity in await DataStore.ActivityEntries.Get())
                {
                    Activities.Add(activity);
                }
                await base.InitializeAsync(navigation);
            }
        }
    }
}
