using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arcesoft.TicTacToe.GameImplementation
{
    public class GameFactory : IGameFactory
    {
        public IGame NewGame()
        {
            return new Game();
        }
    }
}
