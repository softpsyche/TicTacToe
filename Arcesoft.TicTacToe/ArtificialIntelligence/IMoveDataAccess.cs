using Arcesoft.TicTacToe.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arcesoft.TicTacToe.ArtificialIntelligence
{
    internal interface IMoveDataAccess
    {
        Move FindBestMove(string currentBoardPosition, Player currentPlayer, Boolean random = true);

        IEnumerable<MoveResponse> FindMoveResponses(string currentBoardPosition, Player currentPlayer);
    }
}
