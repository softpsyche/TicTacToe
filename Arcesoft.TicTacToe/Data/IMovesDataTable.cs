using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Arcesoft.TicTacToe.Data.TicTacToeDataSet;

namespace Arcesoft.TicTacToe.Data
{
    internal interface IMovesDataTable
    {
        MovesRow[] Select(string filterExpression);
    }
}
