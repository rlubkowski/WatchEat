using System;
using System.Threading.Tasks;
using System.Windows.Input;
using WatchEat.Controls;
using WatchEat.Enums;
using WatchEat.Helpers;
using WatchEat.Models;
using WatchEat.Resources;
using WatchEat.Services.Interfaces;
using WatchEat.Views;
using Xamarin.Forms;

namespace WatchEat.ViewModels
{
    public class GoalViewModel : ViewModelWithChildPages
    {
        public GoalViewModel()
        {
            Title = AppResource.Goal;
        }

        protected INutritionService NutritionService => DependencyService.Get<INutritionService>();
        protected IUserSettings UserSettings => DependencyService.Get<IUserSettings>();

        GoalType _goalType;
        public GoalType GoalType
        {
            get => _goalType;
            set { SetProperty(ref _goalType, value); }
        }

        GoalTimePeriod _goalTimePeriod;
        public GoalTimePeriod GoalTimePeriod
        {
            get => _goalTimePeriod;
            set { SetProperty(ref _goalTimePeriod, value); }
        }

        int _goalPeriod;
        public int GoalPeriod
        {
            get => _goalPeriod;
            set { SetProperty(ref _goalPeriod, value); }
        }

        decimal _loseGainWeight = 0;
        public decimal LoseGainWeight
        {
            get => _loseGainWeight;
            set { SetProperty(ref _loseGainWeight, value); }
        }

        bool _readyToUse = false;
        public bool ReadyToUse
        {
            get => _readyToUse;
            set { SetProperty(ref _readyToUse, value); }
        }

        public ICommand Update => new AsyncCommand(async () =>
        {
            var page = new StyledNavigationPage(new EditGoalPage());
            HandleChildPageEvents(page);
            await Navigation.PushModalAsync(page);
        });

        private void UpdateUserGoal(UserGoalModel userGoalModel)
        {
            GoalType = userGoalModel.GoalType;
            GoalTimePeriod = userGoalModel.GoalTimePeriod;
            GoalPeriod = userGoalModel.GoalPeriod;
            LoseGainWeight = userGoalModel.LoseGainWeight;
            ReadyToUse = UserSettings.AppReadyToUse;
        }

        protected override void OnChildPageAppearing(object sender, EventArgs e)
        {
            MessagingCenter.Subscribe<EditGoalViewModel, UserGoalModel>(this, CommandNames.UserGoalUpdated, (obj, item) =>
            {
                UpdateUserGoal(item);
            });
            base.OnChildPageAppearing(sender, e);
        }

        protected override void OnChildPageDisappearing(object sender, EventArgs e)
        {
            MessagingCenter.Unsubscribe<EditGoalViewModel, UserGoalModel>(this, CommandNames.UserGoalUpdated);
            base.OnChildPageDisappearing(sender, e);
        }

        public override async Task InitializeAsync(INavigation navigation)
        {
            var userGoal = UserSettings.GetUserGoal();
            UpdateUserGoal(userGoal);
            await base.InitializeAsync(navigation);
        }
    }
}
