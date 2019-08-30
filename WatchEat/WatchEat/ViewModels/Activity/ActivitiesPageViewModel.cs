using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using WatchEat.Helpers;
using WatchEat.Models.Database;
using WatchEat.Views.Activity;
using Xamarin.Forms;

namespace WatchEat.ViewModels.Activity
{
    public class ActivitiesPageViewModel : BaseViewModel
    {
        public ActivitiesPageViewModel()
        {
            Activities = new ObservableCollection<TrainingActivity>();
        }

        public ObservableCollection<TrainingActivity> Activities { get; private set; }

        public ICommand OpenAddActivityPage => new AsyncCommand(async () =>
        {
            await Navigation.PushModalAsync(new NavigationPage(new SingleActivityPage()));
        });

        public ICommand ActivitySelected => new AsyncCommand(async (param) =>
        {
            var activity = (TrainingActivity)param;
            await Navigation.PushModalAsync(new NavigationPage(new SingleActivityPage(new SingleActivityPageViewModel(activity))));
        });

        public async override Task InitializeAsync(INavigation navigation)
        {
            foreach (var activity in await DataStore.Activities.Get())
            {
                Activities.Add(activity);
            }

            MessagingCenter.Subscribe<SingleActivityPageViewModel, TrainingActivity>(this, CommandNames.AddTrainingActivity, async (obj, item) =>
            {
                Activities.Add(item);
                await DataStore.Activities.Insert(item);
            });

            MessagingCenter.Subscribe<SingleActivityPageViewModel, TrainingActivity>(this, CommandNames.EditTrainingActivity, async (obj, item) =>
            {
                await DataStore.Activities.Update(item);
            });

            MessagingCenter.Subscribe<SingleActivityPageViewModel, TrainingActivity>(this, CommandNames.RemoveTrainingActivity, async (obj, item) =>
            {
                Activities.Remove(item);
                await DataStore.Activities.Delete(item);
            });

            await base.InitializeAsync(navigation);
        }
    }
}
