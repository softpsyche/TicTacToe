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
        private readonly IServiceProvider _serviceProvider;
        public TicTacToeFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IDatabaseBuilder NewDatabaseBuilder()
        {
            return LocateService<IDatabaseBuilder>();
        }

        public IGame NewGame()
        {
            return LocateService<IGame>();
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
                    return LocateService<BruteForce>();
                case ArtificialIntelligenceTypes.OmniscientGod:
                    return LocateService<OmniscientGod>();
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), $"Unable to create AI for type '{type}'. No implementation found for this type.");
            }
        }

        private T LocateService<T>()
            where T : class
        {
            var service = _serviceProvider.GetService(typeof(T));

            return service as T;
        }
    }
}
