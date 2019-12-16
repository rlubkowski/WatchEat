using Plugin.Settings;
using Plugin.Settings.Abstractions;
using WatchEat.Enums;
using WatchEat.Models;
using WatchEat.Services.Interfaces;

namespace WatchEat.Services
{
    public class UserSettings : IUserSettings
    {
        const string USER_AGE = nameof(USER_AGE);
        const string USER_HEIGHT = nameof(USER_HEIGHT);
        const string USER_WEIGHT = nameof(USER_WEIGHT);
        const string USER_GENDER = nameof(USER_GENDER);
        const string USER_ACTIVITY = nameof(USER_ACTIVITY);

        const string GOAL_TYPE = nameof(GOAL_TYPE);
        const string GOAL_TIME_PERIOD = nameof(GOAL_TIME_PERIOD);
        const string GOAL_PERIOD = nameof(GOAL_PERIOD);
        const string GOAL_LOSE_GAIN = nameof(GOAL_LOSE_GAIN);

        const string USER_INFO_DEFINED = nameof(USER_INFO_DEFINED);
        const string GOAL_INFO_DEFINED = nameof(GOAL_INFO_DEFINED);

        public bool IsUserInfoDefined => Settings.GetValueOrDefault(USER_INFO_DEFINED, false);
        public bool IsUserGoalDefined => Settings.GetValueOrDefault(GOAL_INFO_DEFINED, false);
        public bool AppReadyToUse => IsUserGoalDefined && IsUserInfoDefined;

        private ISettings Settings => CrossSettings.Current;

        public UserGoalModel GetUserGoal()
        {
            GoalType goalType = default(GoalType);
            GoalTimePeriod goalTimePeriod = default(GoalTimePeriod);
            int goalPeriod = 0;
            decimal loseGainWeitgh = 0;
            goalType = (GoalType)Settings.GetValueOrDefault(GOAL_TYPE, (int)goalType);
            goalTimePeriod = (GoalTimePeriod)Settings.GetValueOrDefault(GOAL_TIME_PERIOD, (int)goalTimePeriod);
            goalPeriod = Settings.GetValueOrDefault(GOAL_PERIOD, goalPeriod);
            loseGainWeitgh = Settings.GetValueOrDefault(GOAL_LOSE_GAIN, loseGainWeitgh);
            return new UserGoalModel(goalType, goalTimePeriod, goalPeriod, loseGainWeitgh);
        }

        public UserInfoModel GetUserInformation()
        {
            int age = 0;
            int height = 0;
            decimal weight = 0;
            Gender gender = default(Gender);
            ActivityLevel activityLevel = default(ActivityLevel);
            age = Settings.GetValueOrDefault(USER_AGE, age);            
            height = Settings.GetValueOrDefault(USER_HEIGHT, height);            
            weight = Settings.GetValueOrDefault(USER_WEIGHT, weight);
            gender = (Gender)Settings.GetValueOrDefault(USER_GENDER,(int)gender);
            activityLevel = (ActivityLevel)Settings.GetValueOrDefault(USER_ACTIVITY, (int)activityLevel);
            return new UserInfoModel(age, height, weight, gender, activityLevel);
        }

        public void UpdateUserGoals(UserGoalModel userGoalsModel)
        {
            Settings.AddOrUpdateValue(GOAL_TYPE, (int)userGoalsModel.GoalType);
            Settings.AddOrUpdateValue(GOAL_TIME_PERIOD, (int)userGoalsModel.GoalTimePeriod);
            Settings.AddOrUpdateValue(GOAL_PERIOD, userGoalsModel.GoalPeriod);
            Settings.AddOrUpdateValue(GOAL_LOSE_GAIN, userGoalsModel.LoseGainWeight);
            Settings.AddOrUpdateValue(GOAL_INFO_DEFINED, true);
        }

        public void UpdateUserInformation(UserInfoModel userInfoModel)
        {
            Settings.AddOrUpdateValue(USER_WEIGHT, userInfoModel.Weight);
            Settings.AddOrUpdateValue(USER_HEIGHT, userInfoModel.Height);
            Settings.AddOrUpdateValue(USER_AGE, userInfoModel.Age);
            Settings.AddOrUpdateValue(USER_ACTIVITY, (int)userInfoModel.ActivityLevel);
            Settings.AddOrUpdateValue(USER_GENDER, (int)userInfoModel.Gender);
            Settings.AddOrUpdateValue(USER_INFO_DEFINED, true);
        }

        public void UpdateUserWeight(decimal weight)
        {
            Settings.AddOrUpdateValue(USER_WEIGHT, weight);
        }
    }
}
