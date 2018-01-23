using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arcesoft.TicTacToe.Data
{
    internal interface IMoveDatabase
    {
        IMovesDataTable MovesDataTable { get; }
    }
}
