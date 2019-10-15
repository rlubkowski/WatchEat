using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using WatchEat.Helpers;
using WatchEat.Models;
using Xamarin.Forms;


namespace WatchEat.ViewModels.EventSelection
{
    public class FoodSelectionPageViewModel : BaseViewModel
    {
        public FoodSelectionPageViewModel(DateTime dateTime)
        {
            SelectedDate = dateTime;
            SelectedTime = DateTime.Now.ToTimespan();
            FoodProducts = new ObservableCollection<Models.Database.FoodEntry>();
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

        public ObservableCollection<Models.Database.FoodEntry> FoodProducts { get; private set; }

        public ICommand FoodSelected => new AsyncCommand(async (param) =>
        {
            var foodProduct = (Models.Database.FoodEntry)param;
            MessagingCenter.Send(this, CommandNames.FoodSelected, new FoodSelectionModel(foodProduct, SelectedDate));
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
                foreach (var product in await DataStore.FoodEntries.Get())
                {
                    FoodProducts.Add(product);
                }
                await base.InitializeAsync(navigation);
            }
        }
    }
}