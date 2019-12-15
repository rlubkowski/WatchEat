using WatchEat.Models;

namespace WatchEat.Services.Interfaces
{
    public interface IUserSettings
    {
        void UpdateUserWeight(decimal weight);
        UserInfoModel GetUserInformation();
        void UpdateUserInformation(UserInfoModel userInfoModel);
        UserGoalModel GetUserGoals();
        void UpdateUserGoals(UserGoalModel userGoalsModel);
    }
}
