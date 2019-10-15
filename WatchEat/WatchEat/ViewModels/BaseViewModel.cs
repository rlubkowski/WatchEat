﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using WatchEat.Services.Interfaces;
using Xamarin.Forms;

namespace WatchEat.ViewModels
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        public IDataStore DataStore => DependencyService.Get<IDataStore>();

        bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        string title = string.Empty;
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        protected bool IsInitialized { get; set; }

        public INavigation Navigation { get; private set; }

        public Func<string, string, string, string, Task<bool>> DisplayAlert { get; private set; }

        public virtual async Task InitializeAsync(INavigation navigation)
        {
            Navigation = navigation;
            IsInitialized = true;
            if (!DataStore.IsInitialized)
                await DataStore.InitializeAsync();
        }

        public virtual async Task InitializeAsync(INavigation navigation, Func<string, string, string, string, Task<bool>> displayAlert)
        {            
            DisplayAlert = displayAlert;
            await InitializeAsync(navigation);
        }

        public virtual async Task UnsubscribeAsync()
        {
            
        }

        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName]string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
