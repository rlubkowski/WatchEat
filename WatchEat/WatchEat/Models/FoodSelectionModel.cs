using System;
using WatchEat.Models.Database;

namespace WatchEat.Models
{
    public class FoodSelectionModel
    {
        public FoodSelectionModel(Food foodProduct, DateTime time)
        {
            FoodProduct = foodProduct;
            Time = time;
        }

        public DateTime Time { get; private set; }

        public Food FoodProduct { get; private set; }
    }
}
