using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using WatchEat.Controls;
using WatchEat.Enums;
using WatchEat.Helpers;
using WatchEat.Helpers.MethodExtensions;
using WatchEat.Models;
using WatchEat.Models.Database;
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
            Entries = new ObservableCollection<JournalEntry>();
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

        public ICommand EditEntry => new AsyncCommand(async (entry) =>
        {
            var journalEntry = entry as JournalEntry;
            await Navigation.PushModalAsync(new StyledNavigationPage(new JournalEntryEditPage(new JournalEntryEditViewModel(journalEntry))));
        });

        public ICommand RemoveEntry => new AsyncCommand(async (entry) =>
        {
            if (await DialogService.DisplayAlert("Confirm Remove", "Do you want to remove selected product?", "Yes", "No"))
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
                var entry = item.ToJournalEntry();
                Entries.Add(entry);
                await DataStore.JournalEntries.Insert(entry);
            });

            MessagingCenter.Subscribe<ActivitySelectionViewModel, SelectionModel<ActivityEntry>>(this, CommandNames.ActivitySelected, async (obj, item) =>
            {
                var entry = item.ToJournalEntry();
                Entries.Add(entry);
                await DataStore.JournalEntries.Insert(entry);
            });
        }

        private void UnsubscribeEventSelection()
        {
            MessagingCenter.Unsubscribe<FoodSelectionViewModel, SelectionModel<FoodEntry>>(this, CommandNames.FoodSelected);
            MessagingCenter.Unsubscribe<ActivitySelectionViewModel, SelectionModel<ActivityEntry>>(this, CommandNames.ActivitySelected);
        }

        private DateTime _selectedDay;
        public DateTime SelectedDay
        {
            get => _selectedDay;
            set => SetProperty(ref _selectedDay, value);
        }

        private ObservableCollection<JournalEntry> _entries;
        public ObservableCollection<JournalEntry> Entries
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
