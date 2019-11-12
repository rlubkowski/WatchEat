using System.Windows.Input;
using WatchEat.Controls;
using WatchEat.Helpers;
using WatchEat.Views;

namespace WatchEat.ViewModels
{
    public class UserViewModel : BaseViewModel
    {
        public ICommand Edit => new AsyncCommand(async () =>
        {   
            await Navigation.PushModalAsync(new StyledNavigationPage(new EditUserPage()));
        });
    }
}
