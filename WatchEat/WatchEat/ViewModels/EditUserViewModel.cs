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

    }
}
