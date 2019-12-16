using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using WatchEat.Adapters;
using WatchEat.Controls;
using WatchEat.Enums;
using WatchEat.Helpers;
using WatchEat.Helpers.MethodExtensions;
using WatchEat.Models;
using WatchEat.Models.Database;
using WatchEat.Services.Interfaces;
using WatchEat.ViewModels.EventSelection;
using WatchEat.Views.EventSelection;
using Xamarin.Forms;

namespace WatchEat.ViewModels
{
    public class JournalViewModel : ViewModelWithChildPages
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

        protected IUserSettings UserSettings => DependencyService.Get<IUserSettings>();

        protected INutritionService NutritionService => DependencyService.Get<INutritionService>();

        private void OnEntriesChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            UpdateTotals(sender as SortableObservableCollection<JournalEntry>);
        }

        decimal _totalCarbs = 0;
        public decimal TotalCarbs 
        {
            get => _totalCarbs;
            set { SetProperty(ref _totalCarbs, value); }
        }

        decimal _totalProtein = 0;
        public decimal TotalProtein
        {
            get => _totalProtein;
            set { SetProperty(ref _totalProtein, value); }
        }

        decimal _totalFat = 0;
        public decimal TotalFat
        {
            get => _totalFat;
            set { SetProperty(ref _totalFat, value); }
        }

        decimal _totalFiber = 0;
        public decimal TotalFiber
        {
            get => _totalFiber;
            set { SetProperty(ref _totalFiber, value); }
        }

        decimal _totalWater = 0;
        public decimal TotalWater
        {
            get => _totalWater;
            set { SetProperty(ref _totalWater, value); }
        }

        decimal _totalSalt = 0;
        public decimal TotalSalt
        {
            get => _totalSalt;
            set { SetProperty(ref _totalSalt, value); }
        }

        decimal _burnedCalories = 0;
        public decimal BurnedCalories
        {
            get => _burnedCalories;
            set { SetProperty(ref _burnedCalories, value); }
        }

        decimal _consumedCalories = 0;
        public decimal ConsumedCalories
        {
            get => _consumedCalories;
            set { SetProperty(ref _consumedCalories, value); }
        }

        decimal _recommendedCalories = 0;
        public decimal RecommendedCalories
        {
            get => _recommendedCalories;
            set { SetProperty(ref _recommendedCalories, value); }
        }

        private void UpdateTotals(IEnumerable<JournalEntry> entries)
        {   
            ConsumedCalories = 0;
            BurnedCalories = 0;
            TotalSalt = 0;
            TotalWater = 0;
            TotalFiber = 0;
            TotalFat = 0;
            TotalProtein = 0;
            TotalCarbs = 0;
            foreach (var entry in entries)
            {
                switch (entry.EntryType)
                {
                    case JournalEntryType.Activity:
                        BurnedCalories += entry.Calories;
                        break;
                    case JournalEntryType.Food:
                        TotalSalt += entry.Salt;
                        ConsumedCalories += entry.Calories;
                        TotalFiber += entry.Fiber;
                        TotalFat += entry.Fat;
                        TotalProtein += entry.Protein;
                        TotalCarbs += entry.Carbs;
                        break;
                    case JournalEntryType.Water:
                        TotalWater += entry.Amount;
                        break;
                }
            }
            TotalWater = Math.Round(UnitConverter.MililitersToLiters(TotalWater), 2);
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

        private async Task LoadDayActivitiesAsync(DateTime selectedDay)
        {
            Entries.Clear();
            var nextDay = selectedDay.AddDays(1);
            var entries = await DataStore.JournalEntries.Get(x => x.Date >= selectedDay && x.Date < nextDay, x => x.Date);
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
            var page = new StyledNavigationPage(new JournalEntryEditPage(new JournalEntryEditViewModel(journalEntry)));
            HandleChildPageEvents(page);
            await Navigation.PushModalAsync(page);
        });

        protected override void OnChildPageDisappearing(object sender, EventArgs e)
        {            
            MessagingCenter.Unsubscribe<JournalEntryEditViewModel, JournalEntry>(this, CommandNames.JournalEntryRemoved);
            MessagingCenter.Unsubscribe<JournalEntryEditViewModel, JournalEntry>(this, CommandNames.JournalEntryUpdated);
            base.OnChildPageDisappearing(sender, e);
        }

        protected override void OnChildPageAppearing(object sender, EventArgs e)
        {
            MessagingCenter.Subscribe<JournalEntryEditViewModel, JournalEntry>(this, CommandNames.JournalEntryRemoved, (obj, item) =>
            {
                Entries.Remove(item);
                if (item.EntryType == JournalEntryType.Weight)
                {
                    UpdateDailyCaloriesRecommendation();
                }
            });

            MessagingCenter.Subscribe<JournalEntryEditViewModel, JournalEntry>(this, CommandNames.JournalEntryUpdated, (obj, item) =>
            {
                UpdateTotals(Entries);
            });
        }
        
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
                UserSettings.UpdateUserWeight(item.Weight);
                UpdateDailyCaloriesRecommendation();
            });
        }

        private async Task HandleAddEntryEvent(IJournalEntryAdapter entry) 
        {
            var jEntry = entry.ToJournalEntry();
            Entries.Add(jEntry);
            await DataStore.JournalEntries.Insert(jEntry);
        }

        private void UpdateDailyCaloriesRecommendation()
        {
            if (UserSettings.AppReadyToUse)
            {
                var goal = UserSettings.GetUserGoal();
                var user = UserSettings.GetUserInformation();
                var estimations = NutritionService.CalculateWeightChangeEstimation(user.Age, user.Height, user.Weight, user.Gender,
                                                                        user.ActivityLevel, goal.GoalTimePeriod, goal.GoalPeriod, goal.GoalType, goal.LoseGainWeight);
                if (goal.GoalType == GoalType.Maintain)
                {
                    RecommendedCalories = estimations.MaintainCalories;
                }
                else
                {
                    RecommendedCalories = estimations.IsSafe ? estimations.ReachCalories : estimations.MaintainCalories;
                }
            }
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
            UpdateDailyCaloriesRecommendation();
        }
    }
}
