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
        public IRepository<FoodProduct> FoodProducts { get; private set; }

        public IRepository<Notification> Notifications { get; private set; }

        public IRepository<Activity> Activities { get; private set; }

        public IRepository<JournalEntry> Entries { get; private set; }

        public IRepository<WeightEntry> WeightRecords { get; private set; }

        public IRepository<WaterEntry> WaterConsumptions { get; private set; }\

        public async Task InitializeAsync()
        {
            var connection = new SQLiteAsyncConnection(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "WatchEat.db3"));
            await connection.CreateTableAsync<FoodProduct>();            
            await connection.CreateTableAsync<Notification>();
            await connection.CreateTableAsync<Activity>();
            await connection.CreateTableAsync<JournalEntry>();
            await connection.CreateTableAsync<WeightEntry>();
            await connection.CreateTableAsync<WaterEntry>();
            FoodProducts = new Repository<FoodProduct>(connection);            
            Notifications = new Repository<Notification>(connection);
            Activities = new Repository<Activity>(connection);
            Entries = new Repository<JournalEntry>(connection);
            WeightRecords = new Repository<WeightEntry>(connection);
            WaterConsumptions = new Repository<WaterEntry>(connection);
        }
    }
}
