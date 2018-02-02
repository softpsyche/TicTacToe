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
        void DeleteAllMoveResponses();

        void InsertMoveResponses(IEnumerable<MoveResponse> moveResponses);
        
        IEnumerable<MoveResponse> FindMoveResponses(string currentBoardPosition, Player currentPlayer);
    }
}
