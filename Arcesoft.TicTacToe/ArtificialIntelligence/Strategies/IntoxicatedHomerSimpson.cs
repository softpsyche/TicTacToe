using Arcesoft.TicTacToe.Entities;
using Arcesoft.TicTacToe.RandomNumberGeneration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arcesoft.TicTacToe.ArtificialIntelligence.Strategies
{
    /// <summary>
    /// Its like playing against Homer after he has had way too many beers
    /// </summary>
    internal class IntoxicatedHomerSimpson : IArtificialIntelligence
    {
        private IRandom _random;

        public IntoxicatedHomerSimpson(IRandom random)
        {
            _random = random;
        }

        public void MakeMove(IGame game, bool randomlySelectIfMoreThanOne = true)
        {
            if (game.GameIsOver)
            {
                throw new GameException($"Unable to make a move because the game is over.");
            }

            var availableMoves = game.GetLegalMoves();
            Move moveResponse;

            //most of the time Homer will just move to the first free square..
            if (_random.Next(0, 10) < 6)
            {
                moveResponse = availableMoves.FirstOrDefault();
            }
            else//in other cases he will just move randomly
            {
                moveResponse = availableMoves.RandomFromList(_random);
            }

            game.Move(moveResponse);
        }
    }
}
