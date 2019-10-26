using System.Threading.Tasks;
using WatchEat.Models.Database;

namespace WatchEat.Services.Interfaces
{
    public interface IDataStore
    {
        Task InitializeAsync();
        bool IsInitialized { get; }
        IRepository<FoodEntry> FoodEntries { get; }        
        IRepository<Notification> Notifications { get; }
        IRepository<ActivityEntry> ActivityEntries { get; }
        IRepository<JournalEntry> JournalEntries { get; }              
    }
}
