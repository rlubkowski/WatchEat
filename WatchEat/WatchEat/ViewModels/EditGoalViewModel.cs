using System;
using System.Collections.Generic;
using System.Text;
using WatchEat.Enums;

namespace WatchEat.ViewModels
{
    public class EditGoalViewModel : BaseViewModel
    {
        GoalType _goalType;
        public GoalType GoalType
        {
            get => _goalType;
            set { SetProperty(ref _goalType, value); }
        }

        GoalTimePeriod _goalTimePeriod;
        public GoalTimePeriod GoalTimePeriod
        {
            get => _goalTimePeriod;
            set { SetProperty(ref _goalTimePeriod, value); }
        }

        int _goalPeriod;
        public int GoalPeriod
        {
            get => _goalPeriod;
            set { SetProperty(ref _goalPeriod, value); }
        }

        decimal _loseGainWeight = 0;
        public decimal LoseGainWeight
        {
            get => _loseGainWeight;
            set { SetProperty(ref _loseGainWeight, value); }
        }
    }
}
