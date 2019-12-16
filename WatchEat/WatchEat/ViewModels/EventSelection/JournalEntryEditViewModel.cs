using System.Windows.Input;
using WatchEat.Enums;
using WatchEat.Helpers;
using WatchEat.Models.Database;
using WatchEat.Resources;
using Xamarin.Forms;

namespace WatchEat.ViewModels.EventSelection
{
    public class JournalEntryEditViewModel : BaseViewModel
    {
        public JournalEntryEditViewModel(JournalEntry entry)
        {
            Entry = entry;
            Title = AppResource.EditEntry;
        }

        public JournalEntry Entry { get; private set; }

        public bool ActivityVisible => Entry != null && Entry.EntryType == JournalEntryType.Activity;

        public bool WeightVisible => Entry != null && Entry.EntryType == JournalEntryType.Weight;

        public bool FoodVisible => Entry != null && Entry.EntryType == JournalEntryType.Food;

        public bool WaterVisible => Entry != null && Entry.EntryType == JournalEntryType.Water;

        public bool CaloriesVisible => Entry != null && (Entry.EntryType == JournalEntryType.Food || Entry.EntryType == JournalEntryType.Activity);

        public bool NameVisible => CaloriesVisible;
        
        public ICommand Save => new AsyncCommand(async () =>
        {
            await DataStore.JournalEntries.Update(Entry);
            MessagingCenter.Send(this, CommandNames.JournalEntryUpdated, Entry);
            await Navigation.PopModalAsync();
        });

        public ICommand Remove => new AsyncCommand(async () =>
        {
            if (await DialogService.DisplayAlert(AppResource.ConfirmRemove, AppResource.DoYouWantToRemoveSelectedItem, AppResource.Yes, AppResource.No))
            {
                await DataStore.JournalEntries.Delete(Entry);
                MessagingCenter.Send(this, CommandNames.JournalEntryRemoved, Entry);
                await Navigation.PopModalAsync();
            }
        });

        public ICommand Cancel => new AsyncCommand(async () =>
        {
            await Navigation.PopModalAsync();
        });
    }
}
