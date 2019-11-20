using WatchEat.Enums;

namespace WatchEat.ViewModels
{
    public class EditUserViewModel : BaseViewModel
    {
        ActivityFactor _activityFactor;
        public ActivityFactor ActivityFactor
        {
            get => _activityFactor;
            set { SetProperty(ref _activityFactor, value); }
        }
        
        Gender _gender;
        public Gender Gender
        {
            get => _gender;
            set { SetProperty(ref _gender, value); }
        }
        
        bool _isEnabled = false;
        public bool IsEnabled
        {
            get => _isEnabled;
            set { SetProperty(ref _isEnabled, value); }
        }

        public int Age { get; set; }

        public decimal Weight { get; set; }

        public int Height { get; set; }

    }
}
