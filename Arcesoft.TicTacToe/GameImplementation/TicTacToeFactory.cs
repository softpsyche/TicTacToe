using Arcesoft.TicTacToe.ArtificialIntelligence.Strategies;
using Arcesoft.TicTacToe.Entities;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arcesoft.TicTacToe.GameImplementation
{
    public class TicTacToeFactory : ITicTacToeFactory
    {
        private readonly Container _container;
        public TicTacToeFactory(Container container)
        {
            _container = container;
        }

        public IGame NewGame()
        {
            return _container.GetInstance<IGame>();
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
        public IArtificialIntelligence NewArtificialIntelligence(string type)
        {
            switch (type)
            {
                case ArtificialIntelligenceTypes.BruteForce:
                    return _container.GetInstance<BruteForce>();
                case ArtificialIntelligenceTypes.OmniscientGod:
                    return _container.GetInstance<OmniscientGod>();
                case ArtificialIntelligenceTypes.IntoxicatedHomerSimpson:
                    return _container.GetInstance<IntoxicatedHomerSimpson>();
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), $"Unable to create AI for type '{type}'. No implementation found for this type.");
            }
        }
    }
}
