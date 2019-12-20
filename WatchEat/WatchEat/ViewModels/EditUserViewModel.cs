using System;
using System.Threading.Tasks;
using System.Windows.Input;
using WatchEat.Enums;
using WatchEat.Helpers;
using WatchEat.Models;
using WatchEat.Resources;
using WatchEat.Services.Interfaces;
using WatchEat.ViewModels.Interfaces;
using Xamarin.Forms;

namespace WatchEat.ViewModels
{
    public class EditUserViewModel : BaseViewModel, IValid
    {
        public EditUserViewModel()
        {
            Title = AppResource.EditUserInformation;
        }

        public ICommand Cancel => new AsyncCommand(async (entry) =>
        {
            await Navigation.PopModalAsync();
        });

        public ICommand Save => new AsyncCommand(async (entry) =>
        {
            if (IsValid)
            {
                var userInfo = new UserInfoModel((int)Math.Round(Age), (int)Math.Round(Height), Weight, Gender, ActivityLevel);
                UserSettings.UpdateUserInformation(userInfo);
                MessagingCenter.Send(this, CommandNames.UserInfoUpdated, userInfo);
                await Navigation.PopModalAsync();
            }
            else
            {
                await DialogService.DisplayAlert(AppResource.Validation, AppResource.ValidationValuesIncorrect, AppResource.Cancel);
            }
        });

        protected IUserSettings UserSettings => DependencyService.Get<IUserSettings>();

        public override async Task InitializeAsync(INavigation navigation)
        {
            var userInfo = UserSettings.GetUserInformation();
            ActivityLevel = userInfo.ActivityLevel;
            Gender = userInfo.Gender;
            Age = userInfo.Age;
            Weight = userInfo.Weight;
            Height = userInfo.Height;
            await base.InitializeAsync(navigation);
        }

        public bool IsValid { get => Age > 0  && Weight > 0 && Height > 0; }

        ActivityLevel _activityLevel;
        public ActivityLevel ActivityLevel
        {
            get => _activityLevel;
            set { SetProperty(ref _activityLevel, value); }
        }
        
        Gender _gender;
        public Gender Gender
        {
            get => _gender;
            set { SetProperty(ref _gender, value); }
        }    

        decimal _age = 0;
        public decimal Age 
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

        decimal _heigth = 0;
        public decimal Height 
        {
            get => _heigth;
            set { SetProperty(ref _heigth, value); }
        }
    }
}
