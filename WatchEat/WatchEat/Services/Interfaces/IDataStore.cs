using System.Threading.Tasks;
using WatchEat.Models.Database;

namespace WatchEat.Services.Interfaces
{
    public interface IDataStore
    {
        Task InitializeAsync();
        IRepository<Food> FoodProducts { get; }        
        IRepository<Notification> Notifications { get; }
        IRepository<TrainingActivity> Activities { get; }
        IRepository<JournalEntry> Entries { get; }
        IRepository<WeightEntry> WeightRecords { get; }
        IRepository<WaterEntry> WaterConsumptions { get; }
    }
}
