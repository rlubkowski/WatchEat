using System;
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
    public class FoodSelectionViewModel : BaseViewModel
    {
        public FoodSelectionViewModel(DateTime dateTime)
        {
            SelectedDate = dateTime.AppendCurrentTime();            
            Foods = new ObservableCollection<FoodEntry>();
            Title = AppResource.SelectFood;
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

        public ObservableCollection<FoodEntry> Foods { get; private set; }

        public ICommand FoodSelected => new AsyncCommand(async (param) =>
        {
            var food = (FoodEntry)param;
            MessagingCenter.Send(this, CommandNames.FoodSelected, new SelectionModel<FoodEntry>(food, SelectedDate));
            await Navigation.PopModalToRootAsync();
        });

        public ICommand Cancel => new AsyncCommand(async () =>
        {
            await Navigation.PopModalAsync();
        });

        public async override Task InitializeAsync(INavigation navigation)
        {
            await base.InitializeAsync(navigation);
            Foods.Clear();
            Foods.AddRange(await DataStore.FoodEntries.Get());
        }
    }
}