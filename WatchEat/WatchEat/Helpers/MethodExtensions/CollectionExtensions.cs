using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace WatchEat.Helpers.MethodExtensions
{
    public static class CollectionExtensions
    {
        public static void AddRange<T>(this ObservableCollection<T> collection, IEnumerable<T> elements)
        {
            foreach (var elem in elements)
            {
                collection.Add(elem);
            }
        }
    }
}
