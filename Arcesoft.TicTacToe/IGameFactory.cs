using Arcesoft.TicTacToe.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arcesoft.TicTacToe
{
    public interface IGameFactory
    {
        IGame NewGame();

        IGame NewGame(IEnumerable<Move> moves);
    }
}
