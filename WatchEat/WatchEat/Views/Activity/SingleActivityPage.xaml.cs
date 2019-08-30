using WatchEat.ViewModels.Activity;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WatchEat.Views.Activity
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SingleActivityPage : ContentPage
    {
        public SingleActivityPage()
        {
            InitializeComponent();
        }

        public SingleActivityPage(SingleActivityPageViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }

        protected override async void OnAppearing()
        {
            var viewModel = BindingContext != null ? BindingContext as SingleActivityPageViewModel : new SingleActivityPageViewModel();
            await viewModel.InitializeAsync(Navigation, DisplayAlert);
            if (viewModel.IsEditView)
            {
                ToolbarItem toolbarItem = new ToolbarItem();
                toolbarItem.Text = "Remove";
                toolbarItem.SetBinding(ToolbarItem.CommandProperty, new Binding(nameof(SingleActivityPageViewModel.Remove)));
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