using Xamarin.Forms;

namespace WatchEat.Controls
{
    public class StyledNavigationPage : NavigationPage
    {
        public StyledNavigationPage(Page page) : base(page)
        {
            BarTextColor =  (Color)Application.Current.Resources["CustomWhite"];
            BarBackgroundColor = (Color)Application.Current.Resources["PrimaryColor"];
        }

        public StyledNavigationPage() : base()
        {
            BarTextColor = (Color)Application.Current.Resources["CustomWhite"];
            BarBackgroundColor = (Color)Application.Current.Resources["PrimaryColor"];
        }
    }
}
