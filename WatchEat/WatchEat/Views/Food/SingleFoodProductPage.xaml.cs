using WatchEat.ViewModels.Food;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WatchEat.Views.Food
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SingleFoodProductPage : ContentPage
    {
        public SingleFoodProductPage()
        {
            InitializeComponent();
        }

        public SingleFoodProductPage(SingleFoodProductPageViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }

        protected override async void OnAppearing()
        {
            var viewModel = BindingContext != null ? BindingContext as SingleFoodProductPageViewModel : new SingleFoodProductPageViewModel();
            await viewModel.InitializeAsync(Navigation, DisplayAlert);
            if (viewModel.IsEditView)
            {
                ToolbarItem toolbarItem = new ToolbarItem();
                toolbarItem.Text = "Remove";
                toolbarItem.SetBinding(ToolbarItem.CommandProperty, new Binding(nameof(SingleFoodProductPageViewModel.Remove)));
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