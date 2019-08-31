using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using WatchEat.Helpers;
using WatchEat.Views.TrainingActivity;
using Xamarin.Forms;

namespace WatchEat.ViewModels.TrainingActivity
{
    public class TrainingActivitiesPageViewModel : BaseViewModel
    {
        public TrainingActivitiesPageViewModel()
        {
            Activities = new ObservableCollection<Models.Database.TrainingActivity>();
        }

        public ObservableCollection<Models.Database.TrainingActivity> Activities { get; private set; }

        public ICommand OpenAddActivityPage => new AsyncCommand(async () =>
        {
            await Navigation.PushModalAsync(new NavigationPage(new SingleActivityPage()));
        });

        public ICommand ActivitySelected => new AsyncCommand(async (param) =>
        {
            var activity = (Models.Database.TrainingActivity)param;
            await Navigation.PushModalAsync(new NavigationPage(new SingleActivityPage(new SingleTrainingActivityPageViewModel(activity))));
        });

        public async override Task InitializeAsync(INavigation navigation)
        {
            if (!IsInitialized)
            {
                foreach (var activity in await DataStore.Activities.Get())
                {
                    Activities.Add(activity);
                }

                MessagingCenter.Subscribe<SingleTrainingActivityPageViewModel, Models.Database.TrainingActivity>(this, CommandNames.AddTrainingActivity, async (obj, item) =>
                {
                    Activities.Add(item);
                    await DataStore.Activities.Insert(item);
                });

                MessagingCenter.Subscribe<SingleTrainingActivityPageViewModel, Models.Database.TrainingActivity>(this, CommandNames.EditTrainingActivity, async (obj, item) =>
                {
                    await DataStore.Activities.Update(item);
                });

                MessagingCenter.Subscribe<SingleTrainingActivityPageViewModel, Models.Database.TrainingActivity>(this, CommandNames.RemoveTrainingActivity, async (obj, item) =>
                {
                    Activities.Remove(item);
                    await DataStore.Activities.Delete(item);
                });

                await base.InitializeAsync(navigation);
            }
        }
    }
}
