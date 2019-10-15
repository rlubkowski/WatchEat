using WatchEat.Models;
using Xamarin.Forms;

namespace WatchEat.Views.TemplateSelectors
{
    public class JournalEntryDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate ActivityTemplate { get; set; }

        public DataTemplate WeightTemplate { get; set; }

        public DataTemplate FoodTemplate { get; set; }

        public DataTemplate WaterTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            var journalEntry = (JournalEntryModel)item;
            switch (journalEntry.Type)
            {
                case Enums.JournalEntryType.Activity:
                    return ActivityTemplate;
                case Enums.JournalEntryType.Weight:
                    return WeightTemplate;
                case Enums.JournalEntryType.Food:
                    return FoodTemplate;
                case Enums.JournalEntryType.Water:
                    return WaterTemplate;
            }
            return null;
        }
    }
}
