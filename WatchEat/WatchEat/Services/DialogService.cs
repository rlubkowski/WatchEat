using System.Threading.Tasks;
using WatchEat.Services.Interfaces;

namespace WatchEat.Services
{
    public class DialogService : IDialogService
    {
        public Task DisplayAlert(string title, string message, string cancel)
        {
            return App.Current.MainPage.DisplayAlert(title, message, cancel);
        }

        public Task<bool> DisplayAlert(string title, string message, string accept, string cancel)
        {
            return App.Current.MainPage.DisplayAlert(title, message, accept, cancel);
        }

        public Task<string> DisplayAlert(string title, string cancel, string destruction, params  string[] buttons)
        {
            return App.Current.MainPage.DisplayActionSheet(title, cancel, destruction, buttons);
        }
    }
}
