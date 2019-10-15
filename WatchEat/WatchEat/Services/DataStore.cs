using SQLite;
using System;
using System.IO;
using System.Threading.Tasks;
using WatchEat.Models.Database;
using WatchEat.Services.Interfaces;

namespace WatchEat.Services
{
    public class DataStore : IDataStore
    {
        public IRepository<Food> FoodProducts { get; private set; }

        public IRepository<Notification> Notifications { get; private set; }

        public IRepository<TrainingActivity> Activities { get; private set; }

        public IRepository<JournalEntry> Entries { get; private set; }

        public IRepository<WeightEntry> WeightRecords { get; private set; }

        public IRepository<WaterEntry> WaterConsumptions { get; private set; }

        public bool IsInitialized { get; private set; }

        public async Task InitializeAsync()
        {
            var connection = new SQLiteAsyncConnection(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "WatchEat.db3"));
            await connection.CreateTableAsync<Food>();            
            await connection.CreateTableAsync<Notification>();
            await connection.CreateTableAsync<TrainingActivity>();
            await connection.CreateTableAsync<JournalEntry>();
            await connection.CreateTableAsync<WeightEntry>();
            await connection.CreateTableAsync<WaterEntry>();
            FoodProducts = new Repository<Food>(connection);            
            Notifications = new Repository<Notification>(connection);
            Activities = new Repository<TrainingActivity>(connection);
            Entries = new Repository<JournalEntry>(connection);
            WeightRecords = new Repository<WeightEntry>(connection);
            WaterConsumptions = new Repository<WaterEntry>(connection);
            IsInitialized = true;
        }
    }
}
