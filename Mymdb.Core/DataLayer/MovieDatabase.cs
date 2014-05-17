using System.Collections.Generic;
using System.Linq;
using SQLite.Net;

using Mymdb.Core.Interfaces;
using Mymdb.Core.Models;

namespace Mymdb.Core.DataLayer
{
    public class MovieDatabase
    {
        static object locker = new object();

        SQLiteConnection database;

        public MovieDatabase(SQLiteConnection conn)
        {
            database = conn;
            database.CreateTable<Movie>();
        }

        public IEnumerable<T> GetItems<T>() where T : IBusinessEntity, new()
        {
            lock (locker)
            {
                return database.Table<T>().ToList();
            }
        }

        public T GetItem<T>(int id) where T : IBusinessEntity, new()
        {
            lock (locker)
            {
                return database.Table<T>().FirstOrDefault(x => x.Id == id);
            }
        }

        public int SaveItem<T>(T item) where T : IBusinessEntity, new()
        {
            lock (locker)
            {
                //Id is not an auto-increment
                if (database.Table<T>().FirstOrDefault(x => x.Id == item.Id) != null)
                //if (item.Id != 0)
                {
                    database.Update(item);
                    return item.Id;
                }
                else
                {
                    return database.Insert(item);
                }
            }
        }

        public int DeleteItem<T>(int id) where T : IBusinessEntity, new()
        {
            lock (locker)
            {
                return database.Delete<T>(id);
            }
        }
    }
}
