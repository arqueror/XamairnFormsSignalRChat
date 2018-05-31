using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using App.Infrastructure;
using SQLite;
using System.Linq;
using Xamarin.Forms;
using App.Database;

namespace RaccoonMsgr.Services
{
    public class DBService
    {
        private SQLiteConnection sqlConnection;
        private SQLiteAsyncConnection sqlConnectionAsync;
        static readonly Lazy<DBService> _instanceHolder = new Lazy<DBService>(() => new DBService());
        public static DBService Instance => _instanceHolder.Value;
        public DBService()
        {
        }
        public async Task<bool> DeleteItem<T>(T bindedItem) where T : class, new()
        {
            sqlConnectionAsync = DependencyService.Get<RuntimeSettingsBase>().CreateSqLiteAsyncConnection();
            var repository = new Repository<T>(sqlConnectionAsync);
            if (TableExists<T>(sqlConnectionAsync))
            {
                var deleted = await repository.Delete(bindedItem);
                if (deleted == 1) return true;
            }
            return false;

        }
        public async Task<List<T>> GetAll<T>() where T : class, new()
        {
            sqlConnectionAsync = DependencyService.Get<RuntimeSettingsBase>().CreateSqLiteAsyncConnection();
            var repository = new Repository<T>(sqlConnectionAsync);
            var result = new List<T>();
            if (TableExists<T>(sqlConnectionAsync))
            {
                result = await repository.Get();
            }
            return result;
        }
        public async Task<List<T>> Get<T>(Func<T, bool> condition) where T : class, new()
        {
            sqlConnectionAsync = DependencyService.Get<RuntimeSettingsBase>().CreateSqLiteAsyncConnection();
            var repository = new Repository<T>(sqlConnectionAsync);
            var result = new List<T>();
            if (TableExists<T>(sqlConnectionAsync))
            {
                result = await repository.Get();
            }
            if (result.Count > 0)
                result = result.Where(condition).ToList();
            return result;
        }
        public async Task<bool> Update<T>(T bindedItem) where T : class, new()
        {
            sqlConnectionAsync = DependencyService.Get<RuntimeSettingsBase>().CreateSqLiteAsyncConnection();
            var rep = new Repository<T>(sqlConnectionAsync);
            var succeeded = false;
            if (!TableExists<T>(sqlConnectionAsync))
            {
                var createTable = await sqlConnectionAsync.CreateTableAsync<T>();
                var inserted = await rep.Insert(bindedItem);
                if (inserted == 1)
                    succeeded = true;
            }
            else
            {
                var all = await rep.Get();
                var filtered = all.Where(x => x == bindedItem).FirstOrDefault();
                if (filtered != null)//item already saved in DB
                {
                    //Update Item
                    var updatedEmp = await rep.Update(bindedItem);
                    if (updatedEmp == 1)
                        succeeded = true;
                }
                else //NEW ITEM
                {
                    var inserted = await rep.Insert(bindedItem);
                    if (inserted == 1)
                        succeeded = true;
                }
            }
            return succeeded;
        }

        private bool TableExists<T>(SQLiteAsyncConnection connection)
        {
            const string cmdText = "SELECT name FROM sqlite_master WHERE type='table' AND name=?";
            var cmd = connection.ExecuteScalarAsync<string>(cmdText, typeof(T).Name).Result;
            return cmd != null;
        }
    }
}
