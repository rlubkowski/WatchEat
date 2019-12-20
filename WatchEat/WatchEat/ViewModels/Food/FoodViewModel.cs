using System.Windows.Input;
using WatchEat.Helpers;
using WatchEat.Models.Database;
using WatchEat.Resources;
using Xamarin.Forms;

namespace WatchEat.ViewModels.Food
{
    public class FoodViewModel : BaseViewModel
    {
        public FoodViewModel()
        {
            Title = AppResource.NewFood;
            IsEditView = false;
            Food = new FoodEntry();
        }

        public FoodViewModel(FoodEntry food)
        {
            Title = AppResource.EditFood;
            IsEditView = true;
            Food = food;
            Name = Food.Name;
            Calories = Food.Calories;
            Carbs = Food.Carbs;
            Protein = Food.Protein;
            Fat = Food.Fat;
            Fiber = Food.Fiber;
            Salt = Food.Salt;
        }

        FoodEntry _food;
        public FoodEntry Food
        {
            get => _food;
            set { SetProperty(ref _food, value); }
        }

        string _name = string.Empty;
        public string Name
        {
            get => _name;
            set { SetProperty(ref _name, value); }
        }

        decimal _calories = 0;
        public decimal Calories
        {
            get => _calories;
            set { SetProperty(ref _calories, value); }
        }

        decimal _carbs = 0;
        public decimal Carbs
        {
            get => _carbs;
            set { SetProperty(ref _carbs, value); }
        }

        decimal _protein = 0;
        public decimal Protein
        {
            get => _protein;
            set { SetProperty(ref _protein, value); }
        }

        decimal _fat = 0;
        public decimal Fat
        {
            get => _fat;
            set { SetProperty(ref _fat, value); }
        }

        decimal _fiber = 0;
        public decimal Fiber
        {
            get => _fiber;
            set { SetProperty(ref _fiber, value); }
        }

        decimal _salt = 0;
        public decimal Salt
        {
            get => _salt;
            set { SetProperty(ref _salt, value); }
        }

        public bool IsEditView { get; private set; }

        public bool IsValid
        {
            get => Food != null && !string.IsNullOrWhiteSpace(Food.Name) && Food.Calories > 0;
        }

        public ICommand Save => new AsyncCommand(async () =>
        {
            Food.Name = Name;
            Food.Calories = Calories;
            Food.Carbs = Carbs;
            Food.Protein = Protein;
            Food.Fat = Fat;
            Food.Fiber = Fiber;
            Food.Salt = Salt;

            if (!IsValid)
            {
                await DialogService.DisplayAlert(AppResource.Validation, AppResource.ValidationNameCalories, AppResource.Cancel);
                return;
            }

            if (IsEditView)
            {   
                await DataStore.FoodEntries.Update(Food);                
            }
            else
            {
                await DataStore.FoodEntries.Insert(Food);
                MessagingCenter.Send(this, CommandNames.AddFood, Food);
            }
            
            await Navigation.PopModalAsync();
        });

        public ICommand Cancel => new AsyncCommand(async () =>
        {
            await Navigation.PopModalAsync();
        });

        public ICommand Remove => new AsyncCommand(async () =>
        {
            if (await DialogService.DisplayAlert(AppResource.ConfirmRemove, AppResource.DoYouWantToRemoveSelectedItem, AppResource.Yes, AppResource.No))
            {
                await DataStore.FoodEntries.Delete(Food);
                MessagingCenter.Send(this, CommandNames.RemoveFood, Food);
            }
            await Navigation.PopModalAsync();
        });
    }
}
