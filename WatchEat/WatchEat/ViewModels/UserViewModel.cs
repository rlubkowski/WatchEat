using System;
using System.Threading.Tasks;
using System.Windows.Input;
using WatchEat.Controls;
using WatchEat.Enums;
using WatchEat.Helpers;
using WatchEat.Models;
using WatchEat.Views;
using Xamarin.Forms;

namespace WatchEat.ViewModels
{
    public class UserViewModel : MessagesSubscribedViewModel
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

        decimal _bmi = 0;
        public decimal BMI
        {
            get => _bmi;
            set { SetProperty(ref _bmi, value); }
        }

        decimal _bmr = 0;
        public decimal BMR
        {
            get => _bmr;
            set { SetProperty(ref _bmr, value); }
        }

        decimal _ibw = 0;
        public decimal IBW
        {
            get => _ibw;
            set { SetProperty(ref _ibw, value); }
        }

        decimal _fat = 0;
        public decimal Fat
        {
            get => _fat;
            set { SetProperty(ref _fat, value); }
        }

        decimal _calories = 0;
        public decimal Calories
        {
            get => _calories;
            set { SetProperty(ref _calories, value); }
        }
        
        public ICommand Update => new AsyncCommand(async () =>
        {
            var page = new StyledNavigationPage(new EditUserPage());
            HandlePageEvents(page);
            await Navigation.PushModalAsync(page);
        });
        
        protected override void OnPageAppearing(object sender, EventArgs e)
        {
            MessagingCenter.Subscribe<EditUserViewModel, UserInfoUpdateModel>(this, CommandNames.UserInfoUpdated, async (obj, item) =>
            {

            });
            base.OnPageAppearing(sender, e);
        }

        protected override void OnPageDisappearing(object sender, EventArgs e)
        {
            MessagingCenter.Unsubscribe<EditUserViewModel, UserInfoUpdateModel>(this, CommandNames.UserInfoUpdated);
            base.OnPageDisappearing(sender, e);
        }

        public override Task InitializeAsync(INavigation navigation)
        {            
            return base.InitializeAsync(navigation);
        }
    }
}
