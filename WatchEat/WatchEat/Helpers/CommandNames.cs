namespace WatchEat.Helpers
{
    public static class CommandNames
    {
        public const string AddFood = nameof(AddFood);        
        public const string RemoveFood = nameof(RemoveFood);

        public const string AddActivity = nameof(AddActivity);        
        public const string RemoveActivity = nameof(RemoveActivity);

        public const string AddWaterEntry = nameof(AddWaterEntry);
        public const string AddWeightEntry = nameof(AddWeightEntry);        

        public const string FoodSelected = nameof(FoodSelected);
        public const string ActivitySelected = nameof(ActivitySelected);

        public const string UserInfoUpdated = nameof(UserInfoUpdated);

        public const string UserGoalUpdated = nameof(UserGoalUpdated);

        public const string JournalEntryRemoved = nameof(JournalEntryRemoved);

        public const string JournalEntryUpdated = nameof(JournalEntryUpdated);
    }
}
