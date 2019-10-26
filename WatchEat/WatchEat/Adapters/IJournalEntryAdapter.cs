using WatchEat.Models.Database;

namespace WatchEat.Adapters
{
    public interface IJournalEntryAdapter
    {
        JournalEntry ToJournalEntry();
    }
}
