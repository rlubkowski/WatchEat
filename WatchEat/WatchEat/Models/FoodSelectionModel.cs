using System;
using WatchEat.Models.Database;

namespace WatchEat.Models
{
    public class FoodSelectionModel
    {
        public FoodSelectionModel(FoodEntry food, DateTime time)
        {
            Food = food;
            Time = time;
        }

        public DateTime Time { get; private set; }

        public FoodEntry Food { get; private set; }
    }
}
