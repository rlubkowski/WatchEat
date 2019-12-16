using System;
using System.Threading.Tasks;
using System.Windows.Input;
using WatchEat.Adapters;
using WatchEat.Controls;
using WatchEat.Enums;
using WatchEat.Helpers;
using WatchEat.Helpers.MethodExtensions;
using WatchEat.Models;
using WatchEat.Models.Database;
using WatchEat.Resources;
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
            Entries = new SortableObservableCollection<JournalEntry>() 
            { 
                SortingSelector = x => x.Date, 
                Descending = true 
            };
            Entries.CollectionChanged += OnEntriesChanged;
        }

        private void OnEntriesChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Move:
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Replace:
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Reset:
                    break;
                default:
                    break;
            }
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
            Entries.Clear();
            var tommorow = dateTime.AddDays(1);
            var entries = await DataStore.JournalEntries.Get(x => x.Date >= dateTime && x.Date < tommorow, x => x.Date);
            Entries.AddRange(entries);
        }

        public ICommand AddEvent => new AsyncCommand(async () =>
        {
            if (IsBusy)
                return;
            await Navigation.PushModalAsync(new StyledNavigationPage(new EventSelectionPage(new EventSelectionViewModel(SelectedDay, SubscribeEventSelection, UnsubscribeEventSelection))));
        });
        
        public ICommand EntrySelected => new AsyncCommand(async (entry) =>
        {
            var journalEntry = entry as JournalEntry;
            await Navigation.PushModalAsync(new StyledNavigationPage(new JournalEntryEditPage(new JournalEntryEditViewModel(journalEntry))));
        });

        public ICommand EditEntry => new AsyncCommand(async (entry) =>
        {
            var journalEntry = entry as JournalEntry;
            await Navigation.PushModalAsync(new StyledNavigationPage(new JournalEntryEditPage(new JournalEntryEditViewModel(journalEntry))));
        });

        public ICommand RemoveEntry => new AsyncCommand(async (entry) =>
        {
            if (await DialogService.DisplayAlert(AppResource.ConfirmRemove, AppResource.DoYouWantToRemoveSelectedItem, AppResource.Yes, AppResource.No))
            {
                var journalEntry = entry as JournalEntry;
                Entries.Remove(journalEntry);
                await DataStore.JournalEntries.Delete(journalEntry);
            }
        });

        private void SubscribeEventSelection()
        {
            MessagingCenter.Subscribe<FoodSelectionViewModel, SelectionModel<FoodEntry>>(this, CommandNames.FoodSelected, async (obj, item) =>
            {
                await HandleAddEntryEvent(item);
            });

            MessagingCenter.Subscribe<ActivitySelectionViewModel, SelectionModel<ActivityEntry>>(this, CommandNames.ActivitySelected, async (obj, item) =>
            {
                await HandleAddEntryEvent(item);
            });

            MessagingCenter.Subscribe<WaterViewModel, WaterEntryModel>(this, CommandNames.AddWaterEntry, async (obj, item) =>
            {
                await HandleAddEntryEvent(item);
            });

            MessagingCenter.Subscribe<WeightViewModel, WeightEntryModel>(this, CommandNames.AddWeightEntry, async (obj, item) =>
            {
                await HandleAddEntryEvent(item);
            });
        }

        private async Task HandleAddEntryEvent(IJournalEntryAdapter entry) 
        {
            var jEntry = entry.ToJournalEntry();
            Entries.Add(jEntry);
            await DataStore.JournalEntries.Insert(jEntry);
        }

        private void UnsubscribeEventSelection()
        {
            MessagingCenter.Unsubscribe<FoodSelectionViewModel, SelectionModel<FoodEntry>>(this, CommandNames.FoodSelected);
            MessagingCenter.Unsubscribe<ActivitySelectionViewModel, SelectionModel<ActivityEntry>>(this, CommandNames.ActivitySelected);
            MessagingCenter.Unsubscribe<WaterViewModel, WaterEntryModel>(this, CommandNames.AddWaterEntry);
            MessagingCenter.Unsubscribe<WeightViewModel, WeightEntryModel>(this, CommandNames.AddWeightEntry);
        }

        private DateTime _selectedDay;
        public DateTime SelectedDay
        {
            get => _selectedDay;
            set => SetProperty(ref _selectedDay, value);
        }

        private SortableObservableCollection<JournalEntry> _entries;
        public SortableObservableCollection<JournalEntry> Entries
        {
            get => _entries;
            set => SetProperty(ref _entries, value);
        }

        public override async Task InitializeAsync(INavigation navigation)
        {
            await base.InitializeAsync(navigation);
            await LoadDayActivitiesAsync(SelectedDay);
        }
    }
}
