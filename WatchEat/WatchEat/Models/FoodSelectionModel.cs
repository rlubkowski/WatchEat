using System;
using WatchEat.Models.Database;

namespace WatchEat.Models
{
    public class FoodSelectionModel
    {
        public FoodSelectionModel(Food food, DateTime time)
        {
            Food = food;
            Time = time;
        }

        public DateTime Time { get; private set; }

        public Food Food { get; private set; }
    }
}
