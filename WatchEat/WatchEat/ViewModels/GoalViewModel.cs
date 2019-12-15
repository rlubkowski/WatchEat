using System;
using System.Threading.Tasks;
using System.Windows.Input;
using WatchEat.Controls;
using WatchEat.Helpers;
using WatchEat.Models;
using WatchEat.Views;
using Xamarin.Forms;

namespace WatchEat.ViewModels
{
    public class GoalViewModel : ViewModelWithChildPages
    {

        public ICommand Update => new AsyncCommand(async () =>
        {
            var page = new StyledNavigationPage(new EditGoalPage());
            HandleChildPageEvents(page);
            await Navigation.PushModalAsync(page);
        });

        protected override void OnChildPageAppearing(object sender, EventArgs e)
        {
            MessagingCenter.Subscribe<EditGoalViewModel, UserGoalModel>(this, CommandNames.UserGoalsUpdated, async (obj, item) =>
            {

            });
            base.OnChildPageAppearing(sender, e);
        }

        protected override void OnChildPageDisappearing(object sender, EventArgs e)
        {
            MessagingCenter.Unsubscribe<EditGoalViewModel, UserGoalModel>(this, CommandNames.UserGoalsUpdated);
            base.OnChildPageDisappearing(sender, e);
        }

        public override Task InitializeAsync(INavigation navigation)
        {
            return base.InitializeAsync(navigation);
        }
    }
}
