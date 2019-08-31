using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using WatchEat.Helpers;
using WatchEat.Models;
using Xamarin.Forms;

namespace WatchEat.ViewModels.EventSelection
{
    public class TrainingActivitySelectionPageViewModel : BaseViewModel
    {
        public TrainingActivitySelectionPageViewModel(DateTime dateTime)
        {
            SelectedDate = dateTime;
            Activities = new ObservableCollection<Models.Database.TrainingActivity>();
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

        public ObservableCollection<Models.Database.TrainingActivity> Activities { get; private set; }     

        public ICommand ActivitySelected => new AsyncCommand(async (param) =>
        {
            var activity = (Models.Database.TrainingActivity)param;
            MessagingCenter.Send(this, CommandNames.TrainingActivitySelected, new TrainingActivitySelectionModel(activity, SelectedDate));
            await Navigation.PopModalAsync();
        });

        public ICommand Cancel => new AsyncCommand(async () =>
        {
            await Navigation.PopModalAsync();
        });

        public async override Task InitializeAsync(INavigation navigation)
        {
            if (!IsInitialized)
            {
                foreach (var activity in await DataStore.Activities.Get())
                {
                    Activities.Add(activity);
                }
                await base.InitializeAsync(navigation);
            }
        }
    }
}
