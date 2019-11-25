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
        
        bool _isValid = false;
        public bool IsValid
        {
            get => _isValid;
            set { SetProperty(ref _isValid, value); }
        }

        int _age = 0;
        public int Age 
        {
            get => _age;
            set { SetProperty(ref _age, value); } 
        }

        decimal _weight = 0;
        public decimal Weight 
        {
            get => _weight;
            set { SetProperty(ref _weight, value); }
        }

        int _heigth = 0;
        public int Height 
        {
            get => _heigth;
            set { SetProperty(ref _heigth, value); }
        }
    }
}
