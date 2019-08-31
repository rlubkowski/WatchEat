using System;
using WatchEat.Models.Database;

namespace WatchEat.Models
{
    public class TrainingActivitySelectionModel
    {
        public TrainingActivitySelectionModel(TrainingActivity activity, DateTime time)
        {
            TrainingActivity = activity;
            Time = time;
        }

        public DateTime Time { get; private set; }
        public TrainingActivity TrainingActivity { get; private set; }
    }
}
