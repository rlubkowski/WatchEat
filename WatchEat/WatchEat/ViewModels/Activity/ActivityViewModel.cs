using System.Windows.Input;
using WatchEat.Helpers;
using WatchEat.Models.Database;
using WatchEat.Resources;
using Xamarin.Forms;

namespace WatchEat.ViewModels.Activity
{
    public class ActivityViewModel : BaseViewModel
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
        }

        ActivityEntry _activity;
        public ActivityEntry Activity
        {
            get => _activity;
            set { SetProperty(ref _activity, value); }
        }

        public bool IsEditView { get; private set; }

        bool _isNameValid;
        public bool IsNameValid
        {
            get => _isNameValid;
            set { SetProperty(ref _isNameValid, value); }
        }

        bool _isCaloriesValid;
        public bool IsCaloriesValid
        {
            get => _isCaloriesValid;
            set { SetProperty(ref _isCaloriesValid, value); }
        }

        public bool IsValid
        {
            get => IsCaloriesValid && IsNameValid;
        }

        public ICommand Save => new AsyncCommand(async () =>
        {
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
