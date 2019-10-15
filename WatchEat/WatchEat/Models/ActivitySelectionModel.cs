using System;
using WatchEat.Models.Database;

namespace WatchEat.Models
{
    public class ActivitySelectionModel
    {
        public ActivitySelectionModel(ActivityEntry activity, DateTime time)
        {
            TrainingActivity = activity;
            Time = time;
        }

        public DateTime Time { get; private set; }
        public ActivityEntry TrainingActivity { get; private set; }
    }
}
