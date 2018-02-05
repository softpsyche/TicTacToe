using Arcesoft.TicTacToe.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Arcesoft.TicTacToe.Database
{
    //But what to do with poor Hugo? Too crazy for Boys Town, too much of a boy for Crazy Town.
    //This guy talks to a direct process boundary (i.e. a database). A suggestion which makes sense
    //is to create a very lean integration test that verifies that this guy works as expected. 
    //in this case, we essentially wrappered the calls to the underlying store so there is very
    //little logic in this class. It is mostly a pass-thru. 
    internal class LiteDatabase : ILiteDatabase
    {
        private LiteDB.LiteDatabase _liteDatabase;

        public LiteDatabase(LiteDB.LiteDatabase liteDatabase)
        {
            _liteDatabase = liteDatabase;
        }

        public void DropCollection<T>()
        {
            _liteDatabase.DropCollection(typeof(T).Name);
        }

        public void InsertBulk<T>(IEnumerable<T> instances)
        {
            Collection<T>().InsertBulk(instances);
        }

        public int Count<TItem>()
        {
            return Collection<TItem>().Count();
        }

        public void EnsureIndex<TItem, K>(Expression<Func<TItem, K>> property, bool unique = false)
        {
            var collection = Collection<TItem>();

            collection.EnsureIndex(property, unique);
        }

        public IEnumerable<TItem> FindByIndex<TItem>(Expression<Func<TItem,bool>> predicate)
        {
            return Collection<TItem>().Find(predicate);
        }

        public TItem FindById<TItem, TItemId>(TItemId id)
        {
            return Collection<TItem>().FindById(new LiteDB.BsonValue(id));
        }

        public bool Delete<TItem, TItemId>(TItemId id)
        {
            return Collection<TItem>().Delete(new LiteDB.BsonValue(id));
        }

        private LiteDB.LiteCollection<T> Collection<T>() => _liteDatabase.GetCollection<T>();

        public void Dispose()
        {
            _liteDatabase?.Dispose();
        }
    }
}
