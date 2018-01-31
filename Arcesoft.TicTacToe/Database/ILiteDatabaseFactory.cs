using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arcesoft.TicTacToe.Database
{
    internal interface ILiteDatabaseFactory
    {
        ILiteDatabase OpenOrCreate(string name);
    }
}
