using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arcesoft.TicTacToe.ArtificialIntelligence
{
    internal interface IMoveEvaluator
    {
        //MoveResult CalculateBestMove(IGame game, bool randomlySelectIfMoreThanOne);

        IEnumerable<BoardState> FindAllMoves(IGame game);
    }
}
