using Arcesoft.TicTacToe.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arcesoft.TicTacToe.Data
{
    internal interface IMoveDataAccess
    {
        IEnumerable<MoveResponse> FindMoveResponses(string currentBoardPosition, Player currentPlayer);
    }
}
