using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Arcesoft.TicTacToe.ArtificialIntelligence.TicTacToeDataSet;

namespace Arcesoft.TicTacToe.ArtificialIntelligence
{
    internal interface IMovesDataTable
    {
        MovesRow[] Select(string filterExpression);
    }
}
