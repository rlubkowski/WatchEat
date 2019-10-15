using WatchEat.ViewModels.Activity;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WatchEat.Views.Activity
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ActivityPage : ContentPage
    {
        public ActivityPage()
        {
            InitializeComponent();
        }

        public ActivityPage(ActivityPageViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }

        protected override async void OnAppearing()
        {
            var viewModel = BindingContext != null ? BindingContext as ActivityPageViewModel : new ActivityPageViewModel();
            await viewModel.InitializeAsync(Navigation, DisplayAlert);
            if (viewModel.IsEditView)
            {
                var toolbarItem = new ToolbarItem();
                toolbarItem.Text = "Remove";
                toolbarItem.SetBinding(ToolbarItem.CommandProperty, new Binding(nameof(ActivityPageViewModel.Remove)));
                PageRef.ToolbarItems.Add(toolbarItem);
            }
            else
            {
                BindingContext = viewModel;
            }
            base.OnAppearing();
        }
    }
}