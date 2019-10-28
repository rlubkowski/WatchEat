using System.Threading.Tasks;

namespace WatchEat.Services.Interfaces
{
    public interface IDialogService
    {
        Task DisplayAlert(string title, string message, string cancel);
        Task<bool> DisplayAlert(string title, string message, string accept, string cancel);
        Task<string> DisplayAlert(string title, string cancel, string destruction, params string[] buttons);
    }
}
