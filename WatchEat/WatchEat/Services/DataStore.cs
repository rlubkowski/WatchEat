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
        public IRepository<FoodEntry> FoodEntries { get; private set; }

        public IRepository<Notification> Notifications { get; private set; }

        public IRepository<ActivityEntry> ActivityEntries { get; private set; }

        public IRepository<JournalEntry> JournalEntries { get; private set; }

        public bool IsInitialized { get; private set; }

        public async Task InitializeAsync()
        {
            var connection = new SQLiteAsyncConnection(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "WatchEat.db3"));
            await connection.CreateTableAsync<FoodEntry>();            
            await connection.CreateTableAsync<Notification>();
            await connection.CreateTableAsync<ActivityEntry>();
            await connection.CreateTableAsync<JournalEntry>();            
            FoodEntries = new Repository<FoodEntry>(connection);            
            Notifications = new Repository<Notification>(connection);
            ActivityEntries = new Repository<ActivityEntry>(connection);
            JournalEntries = new Repository<JournalEntry>(connection);            
            IsInitialized = true;
        }
    }
}
