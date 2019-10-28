﻿using System;
using System.Windows.Input;
using WatchEat.Helpers;
using WatchEat.Helpers.MethodExtensions;
using WatchEat.Models;
using Xamarin.Forms;

namespace WatchEat.ViewModels.EventSelection
{
    public class WeightViewModel : BaseViewModel
    {
        public WeightViewModel(DateTime date)
        {
            Title = "New Weight Entry";            
            SelectedDate = date;
            SelectedTime = DateTime.Now.ToTimespan();
        }

        DateTime _date;
        public DateTime SelectedDate
        {
            get => _date;
            set => SetProperty(ref _date, value);
        }

        TimeSpan _time;
        public TimeSpan SelectedTime
        {
            get => _time;
            set
            {
                SetProperty(ref _time, value);
                var date = SelectedDate;
                SelectedDate = new DateTime(date.Year, date.Month, date.Day, value.Hours, value.Minutes, value.Seconds);
            }
        }

        decimal _weight = default(decimal);
        public decimal Weight 
        {
            get => _weight;
            set { SetProperty(ref _weight, value); }
        }

        public ICommand Add => new AsyncCommand(async () =>
        {
            MessagingCenter.Send(this, CommandNames.AddWeightEntry, new WeightEntryModel(Weight, SelectedDate));
            await Navigation.PopModalToRootAsync();
        });

        public ICommand Cancel => new AsyncCommand(async () =>
        {
            await Navigation.PopModalAsync();
        });
    }
}