using WatchEat.Models;

namespace WatchEat.Services.Interfaces
{
    public interface IUserSettings
    {
        void UpdateUserWeight(decimal weight);
        UserInfoModel GetUserInformation();
        void UpdateUserInformation(UserInfoModel userInfoModel);
        UserGoalModel GetUserGoal();
        void UpdateUserGoals(UserGoalModel userGoalsModel);
        bool IsUserInfoDefined { get; }
        bool IsUserGoalDefined { get; }
        bool AppReadyToUse { get; }
    }
}
