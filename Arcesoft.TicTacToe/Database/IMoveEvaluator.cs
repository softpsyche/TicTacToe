using Arcesoft.TicTacToe.ArtificialIntelligence;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arcesoft.TicTacToe.Database
{
    internal interface IMoveEvaluator
    {
        IEnumerable<BoardState> FindAllMoves(IGame game);
    }
}
