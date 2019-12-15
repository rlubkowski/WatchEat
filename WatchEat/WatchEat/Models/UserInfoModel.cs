using WatchEat.Enums;

namespace WatchEat.Models
{
    public class UserInfoModel
    {
        public UserInfoModel(int age, int height, decimal weight, Gender gender, ActivityLevel activityFactor)
        {
            Age = age;
            Height = height;
            Weight = weight;
            Gender = gender;
            ActivityLevel = activityFactor;
        }
        
        public ActivityLevel ActivityLevel { get; private set; }
        
        public Gender Gender { get; private set; }

        public int Age { get; private set; }

        public decimal Weight { get; private set; }

        public int Height { get; private set; }
    }
}
