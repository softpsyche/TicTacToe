using Arcesoft.TicTacToe.Entities;
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
        public IGame NewGame(IEnumerable<Move> moves)
        {
            var game = NewGame();

            foreach (var move in moves)
            {
                if (game.IsMoveValid(move))
                {
                    game.Move(move);
                }
                else
                {
                    throw new GameException("Invalid move passed in. Cannot create game from moves.");
                }
            }

            return game;
        }
    }
}
