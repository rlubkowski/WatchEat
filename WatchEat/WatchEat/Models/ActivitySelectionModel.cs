using System;
using WatchEat.Models.Database;

namespace WatchEat.Models
{
    public class ActivitySelectionModel
    {
        public ActivitySelectionModel(TrainingActivity activity, DateTime time)
        {
            Activity = activity;
            Time = time;
        }

        public DateTime Time { get; private set; }
        public TrainingActivity Activity { get; private set; }
    }
}
