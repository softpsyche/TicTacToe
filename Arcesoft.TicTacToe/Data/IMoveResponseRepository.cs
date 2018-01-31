using Arcesoft.TicTacToe.Entities;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arcesoft.TicTacToe.Data
{
    internal interface IMoveResponseRepository
    {
        void InsertMoveResponse(MoveResponse moveResponse);

        MoveResponse TryFindMoveResponse(string board, Player player, Move response);
        
        IEnumerable<MoveResponse> FindMoveResponses(string currentBoardPosition, Player currentPlayer);

        int FindMoveResponseCount();
    }
}
