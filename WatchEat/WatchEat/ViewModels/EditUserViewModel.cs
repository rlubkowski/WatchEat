
using System.Windows.Input;
using WatchEat.Enums;
using WatchEat.Helpers;
using WatchEat.Models;
using Xamarin.Forms;

namespace WatchEat.ViewModels
{
    public class EditUserViewModel : BaseViewModel
    {
        public ICommand Cancel => new AsyncCommand(async (entry) =>
        {
            await Navigation.PopModalAsync();
        });

        public ICommand Save => new AsyncCommand(async (entry) =>
        {
            if (IsValid)
            {
                MessagingCenter.Send(this, CommandNames.UserInfoUpdated, new UserInfoUpdateModel());
                await Navigation.PopModalAsync();
            }
            else
            {
                await DialogService.DisplayAlert("Validation Info", "Some of the values are incorrect!", "Cancel");
            }
        });

        public bool IsValid { get => IsAgeValid && IsWeightValid && IsHeightValid; }

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
        
        bool _isAgeValid = false;
        public bool IsAgeValid
        {
            get => _isAgeValid;
            set { SetProperty(ref _isAgeValid, value); }
        }

        int _age = 0;
        public int Age 
        {
            get => _age;
            set { SetProperty(ref _age, value); } 
        }

        bool _isWeightValid = false;
        public bool IsWeightValid
        {
            get => _isWeightValid;
            set { SetProperty(ref _isWeightValid, value); }
        }

        decimal _weight = 0;
        public decimal Weight 
        {
            get => _weight;
            set { SetProperty(ref _weight, value); }
        }

        bool _isHeightValid = false;
        public bool IsHeightValid
        {
            get => _isHeightValid;
            set { SetProperty(ref _isHeightValid, value); }
        }

        int _heigth = 0;
        public int Height 
        {
            get => _heigth;
            set { SetProperty(ref _heigth, value); }
        }
    }
}
