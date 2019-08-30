using System.Windows.Input;
using WatchEat.Helpers;
using WatchEat.Models.Database;
using Xamarin.Forms;

namespace WatchEat.ViewModels.Activity
{
    public class SingleActivityPageViewModel : BaseViewModel
    {
        public SingleActivityPageViewModel()
        {
            Title = "New Activity";
            IsEditView = false;
            Activity = new TrainingActivity();
        }

        public SingleActivityPageViewModel(TrainingActivity activity)
        {
            Title = "Edit Activity";
            IsEditView = true;
            Activity = activity;
        }

        TrainingActivity _activity;
        public TrainingActivity Activity
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
