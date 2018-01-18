using Arcesoft.TicTacToe.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arcesoft.TicTacToe.ArtificialIntelligence
{
    public interface IMoveRepository
    {
        Move FindBestMove(string currentBoardPosition, Player currentPlayer, Boolean random = true);
    }
}
