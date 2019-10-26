using WatchEat.Enums;
using WatchEat.Models.Database;
using Xamarin.Forms;

namespace WatchEat.TemplateSelectors
{
    public class JournalEntryDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate ActivityTemplate { get; set; }

        public DataTemplate WeightTemplate { get; set; }

        public DataTemplate FoodTemplate { get; set; }

        public DataTemplate WaterTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            var journalEntry = (JournalEntry)item;
            switch (journalEntry.EntryType)
            {
                case JournalEntryType.Activity:
                    return ActivityTemplate;
                case JournalEntryType.Weight:
                    return WeightTemplate;
                case JournalEntryType.Food:
                    return FoodTemplate;
                case JournalEntryType.Water:
                    return WaterTemplate;
            }
            return null;
        }
    }
}
