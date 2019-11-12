using SQLite;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WatchEat.Models.Database.Abstract;
using WatchEat.Services.Interfaces;

namespace WatchEat.Services
{
    public class Repository<T> : IRepository<T> where T : EntityBase, new()
    {
        private readonly SQLiteAsyncConnection _db;

        public event EventHandler<T> EntityInserted;
        public event EventHandler<T> EntityUpdated;
        public event EventHandler<T> EntityDeleted;

        public Repository(SQLiteAsyncConnection db)
        {
            _db = db;
        }

        public AsyncTableQuery<T> AsQueryable() =>
            _db.Table<T>();

        public async Task<List<T>> Get() =>
            await _db.Table<T>().ToListAsync();

        public async Task<List<T>> Get<TValue>(Expression<Func<T, bool>> predicate = null, Expression<Func<T, TValue>> orderBy = null)
        {
            var query = _db.Table<T>();

            if (predicate != null)
                query = query.Where(predicate);

            if (orderBy != null)
                query = query.OrderBy<TValue>(orderBy);

            return await query.ToListAsync();
        }

        public async Task<T> Get(int id) =>
             await _db.FindAsync<T>(id);

        public async Task<T> Get(Expression<Func<T, bool>> predicate) => 
            await _db.FindAsync<T>(predicate);

        public async Task<int> Insert(T entity)
        {
            var result = await _db.InsertAsync(entity);
            OnEntityInserted(entity);
            return result;
        }

        public async Task<int> Update(T entity)
        {
            var result = await _db.UpdateAsync(entity);
            OnEntityUpdated(entity);
            return result;
        }

        public async Task<int> Delete(T entity)
        {
            var result = await _db.DeleteAsync(entity);
            OnEntityDeleted(entity);
            return result;
        }

        private void OnEntityInserted(T entity)
        {
            if (EntityInserted == null)
                return;
            EntityInserted.Invoke(this, entity);
        }

        private void OnEntityUpdated(T entity)
        {
            if (EntityUpdated == null)
                return;
            EntityUpdated.Invoke(this, entity);
        }

        private void OnEntityDeleted(T entity)
        {
            if (EntityDeleted == null)
                return;
            EntityDeleted.Invoke(this, entity);
        }
    }
}
