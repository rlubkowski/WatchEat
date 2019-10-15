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
            var entries = await DataStore.JournalEntries.Get(x => x.Date >= dateTime && x.Date < tommorow, x => x.Date);
            Activities.Clear();
            foreach (var entry in entries)
            {
                switch (entry.EntryType)
                {
                    case JournalEntryType.Activity:
                        var activity = await DataStore.ActivityEntries.Get(entry.Reference);
                        Activities.Add(new JournalEntryModel(activity, entry.Date));
                        break;
                    case JournalEntryType.Weight:
                        var weight = await DataStore.WeightEntries.Get(entry.Reference);
                        Activities.Add(new JournalEntryModel(weight));
                        break;
                    case JournalEntryType.Food:
                        var food = await DataStore.FoodEntries.Get(entry.Reference);
                        Activities.Add(new JournalEntryModel(food, entry.Date));
                        break;
                    case JournalEntryType.Water:
                        var water = await DataStore.WaterEntries.Get(entry.Reference);
                        Activities.Add(new JournalEntryModel(water));
                        break;                    
                }
            }
        }

        public ICommand AddEvent => new AsyncCommand(async() =>
        {
            if (IsBusy)
                return;
            await Navigation.PushModalAsync(new NavigationPage(new EventSelectionPage(new EventSelectionPageViewModel(SelectedDay))));
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

            MessagingCenter.Subscribe<FoodSelectionPageViewModel, FoodSelectionModel>(this, CommandNames.FoodSelected, async (obj, item) =>
            {
                var entryModel = new JournalEntryModel(item);
                Activities.Add(entryModel);

                await DataStore.JournalEntries.Insert(entryModel.ToEntity());
            });

            MessagingCenter.Subscribe<ActivitySelectionPageViewModel, ActivitySelectionModel>(this, CommandNames.ActivitySelected, async (obj, item) =>
            {
                var entryModel = new JournalEntryModel(item);
                Activities.Add(entryModel);

                await DataStore.JournalEntries.Insert(entryModel.ToEntity());
            });
           
            await LoadDayActivitiesAsync(SelectedDay);
        }      
    }
}
