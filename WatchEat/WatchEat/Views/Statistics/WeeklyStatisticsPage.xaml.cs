﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatchEat.ViewModels.Statistics;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WatchEat.Views.Statistics
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WeeklyStatisticsPage : ContentPage
    {
        public WeeklyStatisticsPage()
        {
            InitializeComponent();
            BindingContext = new WeeklyStatisticsViewModel();
        }

        protected override async void OnAppearing()
        {
            await (BindingContext as WeeklyStatisticsViewModel).InitializeAsync(Navigation);
            base.OnAppearing();
        }
    }
}