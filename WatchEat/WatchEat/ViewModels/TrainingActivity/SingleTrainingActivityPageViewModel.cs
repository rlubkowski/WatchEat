using System.Windows.Input;
using WatchEat.Helpers;
using Xamarin.Forms;

namespace WatchEat.ViewModels.TrainingActivity
{
    public class SingleTrainingActivityPageViewModel : BaseViewModel
    {
        public SingleTrainingActivityPageViewModel()
        {
            Title = "New Activity";
            IsEditView = false;
            Activity = new Models.Database.TrainingActivity();
        }

        public SingleTrainingActivityPageViewModel(Models.Database.TrainingActivity activity)
        {
            Title = "Edit Activity";
            IsEditView = true;
            Activity = activity;
        }

        Models.Database.TrainingActivity _activity;
        public Models.Database.TrainingActivity Activity
        {
            get => _activity;
            set { SetProperty(ref _activity, value); }
        }

        public bool IsEditView { get; private set; }

        public ICommand Save => new AsyncCommand(async () =>
        {
            if (IsEditView)
            {
                MessagingCenter.Send(this, CommandNames.EditTrainingActivity, Activity);
            }
            else
            {
                MessagingCenter.Send(this, CommandNames.AddTrainingActivity, Activity);
            }
            await Navigation.PopModalAsync();
        });

        public ICommand Cancel => new AsyncCommand(async () =>
        {
            await Navigation.PopModalAsync();
        });

        public ICommand Remove => new AsyncCommand(async () =>
        {
            if (await DisplayAlert("Confirm Remove", "Do you want to remove selected activity?", "Yes", "No"))
            {
                MessagingCenter.Send(this, CommandNames.RemoveTrainingActivity, Activity);
            }
            await Navigation.PopModalAsync();
        });
    }
}
