using Arcesoft.TicTacToe.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arcesoft.TicTacToe.GameImplementation
{
    internal interface IBestMoveSelector
    {
        IMoveResult FindRandomBestMoveResultForPlayerOrDefault(IEnumerable<IMoveResult> gameMoveResults, Player player);

        IEnumerable<IMoveResult> FindBestMoveResultsForPlayer(IEnumerable<IMoveResult> gameMoveResults, Player player);
    }
}
