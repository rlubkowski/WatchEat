using System;
using System.Threading.Tasks;
using System.Windows.Input;
using WatchEat.Controls;
using WatchEat.Enums;
using WatchEat.Helpers;
using WatchEat.Models;
using WatchEat.Resources;
using WatchEat.Services.Interfaces;
using WatchEat.Views;
using Xamarin.Forms;

namespace WatchEat.ViewModels
{
    public class UserViewModel : ViewModelWithChildPages
    {
        public UserViewModel()
        {
            Title = AppResource.UserInformation;
        }

        protected INutritionService NutritionService => DependencyService.Get<INutritionService>();
        protected IUserSettings UserSettings => DependencyService.Get<IUserSettings>();
        protected IBMIService BMIService => DependencyService.Get<IBMIService>();
        protected IIBWService IBWService => DependencyService.Get<IIBWService>();
        protected IBodyFatService BodyFatService => DependencyService.Get<IBodyFatService>();

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

        BMI _bmiFactor;
        public BMI BMIFactor
        {
            get => _bmiFactor;
            set { SetProperty(ref _bmiFactor, value); }
        }

        decimal _bmr = 0;
        public decimal BMR
        {
            get => _bmr;
            set { SetProperty(ref _bmr, value); }
        }

        decimal _ibwBroca = 0;
        public decimal IBWBroca
        {
            get => _ibwBroca;
            set { SetProperty(ref _ibwBroca, value); }
        }

        decimal _ibwDevine = 0;
        public decimal IBWDevine
        {
            get => _ibwDevine;
            set { SetProperty(ref _ibwDevine, value); }
        }

        decimal _ibwHamwi = 0;
        public decimal IBWHamwi
        {
            get => _ibwHamwi;
            set { SetProperty(ref _ibwHamwi, value); }
        }

        decimal _ibwLemmens = 0;
        public decimal IBWLemmens
        {
            get => _ibwLemmens;
            set { SetProperty(ref _ibwLemmens, value); }
        }

        decimal _ibwRobinson = 0;
        public decimal IBWRobinson
        {
            get => _ibwRobinson;
            set { SetProperty(ref _ibwRobinson, value); }
        }

        decimal _ibwMiller = 0;
        public decimal IBWMiller
        {
            get => _ibwMiller;
            set { SetProperty(ref _ibwMiller, value); }
        }

        decimal _bodyFat = 0;
        public decimal BodyFat
        {
            get => _bodyFat;
            set { SetProperty(ref _bodyFat, value); }
        }

        decimal _bmrHarrisBenedict = 0;
        public decimal BMRHarrisBenedict
        {
            get => _bmrHarrisBenedict;
            set { SetProperty(ref _bmrHarrisBenedict, value); }
        }

        decimal _bmrMifflinStJeor = 0;
        public decimal BMRMifflinStJeor
        {
            get => _bmrMifflinStJeor;
            set { SetProperty(ref _bmrMifflinStJeor, value); }
        }

        decimal _caloriesHarrisBenedict = 0;
        public decimal CaloriesHarrisBenedict
        {
            get => _caloriesHarrisBenedict;
            set { SetProperty(ref _caloriesHarrisBenedict, value); }
        }

        decimal _caloriesMifflinStJeor = 0;
        public decimal CaloriesMifflinStJeor
        {
            get => _caloriesMifflinStJeor;
            set { SetProperty(ref _caloriesMifflinStJeor, value); }
        }

        NutritionRecommendation _harrisBenedictNutrition;
        public NutritionRecommendation HarrisBenedictNutrition
        {
            get => _harrisBenedictNutrition;
            set { SetProperty(ref _harrisBenedictNutrition, value); }
        }

        NutritionRecommendation _mifflinStJeorNutrition;
        public NutritionRecommendation MifflinStJeorNutrition
        {
            get => _mifflinStJeorNutrition;
            set { SetProperty(ref _mifflinStJeorNutrition, value); }
        }

        decimal _dailyWater = 0;
        public decimal DailyWater
        {
            get => _dailyWater;
            set { SetProperty(ref _dailyWater, value); }
        }

        public ICommand Update => new AsyncCommand(async () =>
        {
            var page = new StyledNavigationPage(new EditUserPage());
            HandleChildPageEvents(page);
            await Navigation.PushModalAsync(page);
        });

        private void UpdateUserInformation(UserInfoModel userInfoModel)
        {
            ActivityLevel = userInfoModel.ActivityLevel;
            Age = userInfoModel.Age;
            Gender = userInfoModel.Gender;
            Height = userInfoModel.Height;
            Weight = userInfoModel.Weight;
            IBWBroca = Math.Round(IBWService.CalculateBroca(Height, Gender));
            IBWDevine = Math.Round(IBWService.CalculateDevine(Height, Gender));
            IBWHamwi = Math.Round(IBWService.CalculateHamwi(Height, Gender));
            IBWLemmens =  Math.Round(IBWService.CalculateLemmens(Height));
            IBWRobinson = Math.Round(IBWService.CalculateRobinson(Height, Gender));
            IBWMiller = Math.Round(IBWService.CalculateMiller(Height, Gender));
            var bmiResult = BMIService.Calculate(Weight, Height);
            BMI = Math.Round(bmiResult.BMIValue, 1);
            BMIFactor = bmiResult.BMIFactor;
            BodyFat = Math.Round(BodyFatService.EstimateBodyFatPercentage(BMI, Age, Gender), 2);
            BMRHarrisBenedict = Math.Round(NutritionService.BMRHarrisBenedict(Weight, Height, Age, Gender));
            BMRMifflinStJeor = Math.Round(NutritionService.BMRMifflinStJeor(Weight, Height, Age, Gender));
            CaloriesHarrisBenedict = Math.Round(NutritionService.GetDailyCaloriesEstimation(BMRHarrisBenedict, ActivityLevel));
            CaloriesMifflinStJeor = Math.Round(NutritionService.GetDailyCaloriesEstimation(BMRMifflinStJeor, ActivityLevel));
            HarrisBenedictNutrition = NutritionService.CalculateNutritionRecommendation(Convert.ToInt32(CaloriesHarrisBenedict));
            MifflinStJeorNutrition = NutritionService.CalculateNutritionRecommendation(Convert.ToInt32(CaloriesMifflinStJeor));
            DailyWater = Math.Round(UnitConverter.MililitersToLiters(NutritionService.CalculateDailyWaterIntake(Weight)), 1);
        }

        protected override void OnChildPageAppearing(object sender, EventArgs e)
        {
            MessagingCenter.Subscribe<EditUserViewModel, UserInfoModel>(this, CommandNames.UserInfoUpdated, (obj, item) =>
            {
                UpdateUserInformation(item);
            });
            base.OnChildPageAppearing(sender, e);
        }

        protected override void OnChildPageDisappearing(object sender, EventArgs e)
        {
            MessagingCenter.Unsubscribe<EditUserViewModel, UserInfoModel>(this, CommandNames.UserInfoUpdated);
            base.OnChildPageDisappearing(sender, e);
        }

        public override async Task InitializeAsync(INavigation navigation)
        {
            var userInfo = UserSettings.GetUserInformation();
            UpdateUserInformation(userInfo);
            await base.InitializeAsync(navigation);
        }
    }
}
