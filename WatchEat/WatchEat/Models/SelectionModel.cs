using System;
using WatchEat.Models.Database;

namespace WatchEat.Models
{
    public class SelectionModel<T> where T : EntityBase
    {
        public SelectionModel(T selectedEntity, DateTime time)
        {
            SelectedEntity = selectedEntity;
            Time = time;
        }

        public T SelectedEntity { get; private set; }

        public DateTime Time { get; private set; }
    }
}
