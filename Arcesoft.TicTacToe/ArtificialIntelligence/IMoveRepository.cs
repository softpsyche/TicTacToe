using Arcesoft.TicTacToe.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arcesoft.TicTacToe.ArtificialIntelligence
{
    internal interface IMoveRepository
    {
        IEnumerable<MoveResponse> FindBoardStates(string currentBoardPosition, Player currentPlayer);
    }
}
