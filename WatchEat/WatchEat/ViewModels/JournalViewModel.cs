using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using WatchEat.Enums;
using WatchEat.Helpers;
using WatchEat.Models;
using WatchEat.ViewModels.EventSelection;
using WatchEat.Views.EventSelection;
using Xamarin.Forms;

namespace WatchEat.ViewModels
{
    public class JournalViewModel : BaseViewModel
    {
        public JournalViewModel()
        {
            SelectedDay = DateTime.Today;
            Activities = new ObservableCollection<JournalEntryModel>();
        }

        public ICommand Navigate => new AsyncCommand(async (direction) =>
        {
            if (IsBusy)
                return;
            IsBusy = true;
            if ((NavigationDirection)direction == NavigationDirection.Forward)
            {
                SelectedDay = SelectedDay.AddDays(1);
                await LoadDayActivitiesAsync(SelectedDay);
            }
            else if ((NavigationDirection)direction == NavigationDirection.Backward)
            {
                SelectedDay = SelectedDay.AddDays(-1);
                await LoadDayActivitiesAsync(SelectedDay);
            }
            IsBusy = false;
        });

        private async Task LoadDayActivitiesAsync(DateTime dateTime)
        {            
            var tommorow = dateTime.AddDays(1);
            var entries = await DataStore.Entries.Get(x => x.Date >= dateTime && x.Date < tommorow, x => x.Date);
            Activities.Clear();
            foreach (var entry in entries)
            {
                switch (entry.EntryType)
                {
                    case JournalEntryType.TrainingActivity:
                        var activity = await DataStore.Activities.Get(entry.Reference);
                        Activities.Add(new JournalEntryModel(activity, entry.Date));
                        break;
                    case JournalEntryType.Weight:
                        var weight = await DataStore.WeightRecords.Get(entry.Reference);
                        Activities.Add(new JournalEntryModel(weight));
                        break;
                    case JournalEntryType.Food:
                        var food = await DataStore.FoodProducts.Get(entry.Reference);
                        Activities.Add(new JournalEntryModel(food, entry.Date));
                        break;
                    case JournalEntryType.Water:
                        var water = await DataStore.WaterConsumptions.Get(entry.Reference);
                        Activities.Add(new JournalEntryModel(water));
                        break;                    
                }
            }
        }

        public ICommand AddActivity => new AsyncCommand(async() =>
        {
            if (IsBusy)
                return;
            await Navigation.PushModalAsync(new NavigationPage(new ActivitySelectionPage(new ActivitySelectionPageViewModel(SelectedDay))));
        });

        private DateTime _selectedDay;
        public DateTime SelectedDay
        {
            get => _selectedDay;
            set => SetProperty(ref _selectedDay, value);
        }

        private ObservableCollection<JournalEntryModel> _activities;
        public ObservableCollection<JournalEntryModel> Activities
        {
            get => _activities;
            set => SetProperty(ref _activities, value);
        }
        public override async Task InitializeAsync(INavigation navigation)
        {
            await base.InitializeAsync(navigation);

            MessagingCenter.Subscribe<FoodSelectionPageViewModel, FoodSelectionModel>(this, CommandNames.FoodProductSelected, async (obj, item) =>
            {
                Activities.Add(new JournalEntryModel(item));

                await DataStore.Entries.Insert(new Models.Database.JournalEntry
                {
                    Date = item.Time,
                    EntryType = JournalEntryType.Food,
                    Reference = item.Food.Id
                });
            });

            MessagingCenter.Subscribe<TrainingActivitySelectionPageViewModel, TrainingActivitySelectionModel>(this, CommandNames.TrainingActivitySelected, async (obj, item) =>
            {
                Activities.Add(new JournalEntryModel(item));

                await DataStore.Entries.Insert(new Models.Database.JournalEntry
                {
                    Date = item.Time,
                    EntryType = JournalEntryType.TrainingActivity,
                    Reference = item.TrainingActivity.Id
                });
            });
           
            await LoadDayActivitiesAsync(SelectedDay);
        }      
    }
}
