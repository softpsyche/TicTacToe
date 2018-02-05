using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arcesoft.TicTacToe.Database
{
    internal class LiteDatabaseFactory : ILiteDatabaseFactory
    {
        //See ilitedatabase comment...same thing here
        public ILiteDatabase OpenOrCreate(string name)
        {
            return new LiteDatabase(new LiteDB.LiteDatabase(name));
        }
    }
}
