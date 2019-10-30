﻿using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;

namespace WatchEat.Helpers
{
    public class SortableObservableCollection<T> : ObservableCollection<T>
    {
        public Func<T, object> SortingSelector { get; set; }

        public bool Descending { get; set; }

        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            base.OnCollectionChanged(e);

            if (SortingSelector == null
                || e.Action == NotifyCollectionChangedAction.Remove
                || e.Action == NotifyCollectionChangedAction.Reset)
                return;

            var query = this.Select((item, index) => (Item: item, Index: index));

            query = Descending
              ? query.OrderBy(tuple => SortingSelector(tuple.Item))
              : query.OrderByDescending(tuple => SortingSelector(tuple.Item));

            var map = query.Select((tuple, index) => (OldIndex: tuple.Index, NewIndex: index)).Where(o => o.OldIndex != o.NewIndex);

            using (var enumerator = map.GetEnumerator())
            {
                if (enumerator.MoveNext())
                    Move(enumerator.Current.OldIndex, enumerator.Current.NewIndex);
            }
        }
    }
}


//USAGE
//class Program
//{
//    static void Main(string[] args)
//    {
//        var xx = new SortableObservableCollection<int>() { SortingSelector = i => i };
//        xx.CollectionChanged += (sender, e) =>
//         WriteLine($"action: {e.Action}, oldIndex:{e.OldStartingIndex},"
//           + " newIndex:{e.NewStartingIndex}, newValue: {xx[e.NewStartingIndex]}");

//        xx.Add(10);
//        xx.Add(8);
//        xx.Add(45);
//        xx.Add(0);
//        xx.Add(100);
//        xx.Add(-800);
//        xx.Add(4857);
//        xx.Add(-1);

//        foreach (var item in xx)
//            Write($"{item}, ");
//    }
//}
