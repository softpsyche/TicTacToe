using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arcesoft.TicTacToe.ArtificialIntelligence.Strategies
{


    internal class BruteForce : IArtificialIntelligence
    {
        private readonly IMoveEvaluator _moveEvaluator;

        public BruteForce(IMoveEvaluator moveEvaluator)
        {
            _moveEvaluator = moveEvaluator;
        }

        public void MakeMove(IGame game, bool randomlySelectIfMoreThanOne = true)
        {
            //_moveEvaluator.CalculateBestMove(game, randomlySelectIfMoreThanOne);
        }
    }
}
