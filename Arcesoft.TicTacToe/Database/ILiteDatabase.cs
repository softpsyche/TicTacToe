using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Arcesoft.TicTacToe.Database
{
    internal interface ILiteDatabase : IDisposable
    {
        int Count<TItem>();

        void Insert<T>(T instance);

        TItem FindById<TItem,TItemId>(TItemId id);

        IEnumerable<TItem> FindByIndex<TItem>(Expression<Func<TItem, bool>> predicate);

        void EnsureIndex<TItem, K>(Expression<Func<TItem, K>> property, bool unique = false);

        bool Delete<TItem, TItemId>(TItemId id);
    }
}
