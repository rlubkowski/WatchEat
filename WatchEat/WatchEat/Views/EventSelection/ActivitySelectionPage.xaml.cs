﻿using WatchEat.ViewModels.EventSelection;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WatchEat.Views.EventSelection
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ActivitySelectionPage : ContentPage
    {
        public ActivitySelectionPage(ActivitySelectionViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }

        protected override async void OnAppearing()
        {            
            await (BindingContext as ActivitySelectionViewModel).InitializeAsync(Navigation);
            base.OnAppearing();
        }
    }
}