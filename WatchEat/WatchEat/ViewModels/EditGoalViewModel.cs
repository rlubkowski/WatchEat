using System.Threading.Tasks;
using System.Windows.Input;
using WatchEat.Enums;
using WatchEat.Helpers;
using WatchEat.Helpers.MethodExtensions;
using WatchEat.Models;
using WatchEat.Resources;
using WatchEat.Services.Interfaces;
using Xamarin.Forms;

namespace WatchEat.ViewModels
{
    public class EditGoalViewModel : BaseViewModel
    {
        public EditGoalViewModel()
        {
            Title = AppResource.EditGoal;
        }

        public ICommand Cancel => new AsyncCommand(async (entry) =>
        {
            await Navigation.PopModalAsync();
        });

        public ICommand Save => new AsyncCommand(async (entry) =>
        {
            if (IsValid)
            {
                var userGoal = new UserGoalModel(GoalType, GoalTimePeriod, GoalPeriod, LoseGainWeight);
                UserSettings.UpdateUserGoals(userGoal);
                MessagingCenter.Send(this, CommandNames.UserGoalUpdated, userGoal);
                await Navigation.PopModalToRootAsync();
            }
            else
            {
                await DialogService.DisplayAlert(AppResource.Validation, AppResource.ValidationValuesIncorrect, AppResource.Cancel);
            }
        });

        public override async Task InitializeAsync(INavigation navigation)
        {
            var userGoal = UserSettings.GetUserGoal();
            GoalPeriod = userGoal.GoalPeriod;
            GoalTimePeriod = userGoal.GoalTimePeriod;
            GoalType = userGoal.GoalType;
            LoseGainWeight = userGoal.LoseGainWeight;
            await base.InitializeAsync(navigation);
        }

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

        bool _isGoalPeriodValid = false;
        public bool IsGoalPeriodValid
        {
            get => _isGoalPeriodValid;
            set { SetProperty(ref _isGoalPeriodValid, value); }
        }

        bool _isGoalWeightValid = false;
        public bool IsGoalWeightValid
        {
            get => _isGoalWeightValid;
            set { SetProperty(ref _isGoalWeightValid, value); }
        }

        public bool IsValid
        {
            get => GoalType != GoalType.Maintain ? IsGoalWeightValid && IsGoalPeriodValid : true;
        }
    }
}
