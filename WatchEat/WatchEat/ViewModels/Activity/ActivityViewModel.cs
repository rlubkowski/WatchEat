using System.Windows.Input;
using WatchEat.Helpers;
using WatchEat.Models.Database;
using WatchEat.Resources;
using WatchEat.ViewModels.Interfaces;
using Xamarin.Forms;

namespace WatchEat.ViewModels.Activity
{
    public class ActivityViewModel : BaseViewModel, IValid
    {
        public ActivityViewModel()
        {
            Title = AppResource.NewActivity;
            IsEditView = false;
            Activity = new ActivityEntry();
        }

        public ActivityViewModel(ActivityEntry activity)
        {
            Title = AppResource.EditActivity;
            IsEditView = true;
            Activity = activity;
            Name = Activity.Name;
            Calories = Activity.Calories;
        }

        ActivityEntry _activity;
        public ActivityEntry Activity
        {
            get => _activity;
            set { SetProperty(ref _activity, value); }
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

        public bool IsEditView { get; private set; }

        public bool IsValid
        {
            get => Activity != null && !string.IsNullOrWhiteSpace(Activity.Name) && Activity.Calories > 0;
        }

        public ICommand Save => new AsyncCommand(async () =>
        {
            Activity.Name = Name;
            Activity.Calories = Calories;

            if (!IsValid)
            {
                await DialogService.DisplayAlert(AppResource.Validation, AppResource.ValidationNameCalories, AppResource.Cancel);
                return;
            }

            if (IsEditView)
            {
                await DataStore.ActivityEntries.Update(Activity);
            }
            else
            {
                await DataStore.ActivityEntries.Insert(Activity);
                MessagingCenter.Send(this, CommandNames.AddActivity, Activity);
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
                await DataStore.ActivityEntries.Delete(Activity);
                MessagingCenter.Send(this, CommandNames.RemoveActivity, Activity);
            }
            await Navigation.PopModalAsync();
        });
    }
}
