using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Arcesoft.TicTacToe.Entities;
using Arcesoft.TicTacToe.RandomNumberGeneration;

namespace Arcesoft.TicTacToe.ArtificialIntelligence.Strategies
{
    /// <summary>
    /// Its like playing against a god. All knowing, all seeing, never losing. Very boring
    /// </summary>
    internal class OmniscientGod : IArtificialIntelligence
    {
        private IMoveDataAccess _moveDataAccess;
        private IRandom _random;

        public OmniscientGod(IMoveDataAccess moveDataAccess,IRandom random)
        {
            _moveDataAccess = moveDataAccess;
            _random = random;
        }

        public void MakeMove(IGame game, ArtificialIntelligenceBehavior behavior = ArtificialIntelligenceBehavior.PlayToWin, bool randomlySelectIfMoreThanOne = true)
        {
            if (game.GameIsOver)
            {
                throw new GameException($"Unable to make a move because the game is over.");
            }

            var moves = _moveDataAccess.FindMoveResponses(game.GameBoardString, game.CurrentPlayer);
            MoveResponse moveResponse;

            switch (behavior)
            {
                case ArtificialIntelligenceBehavior.PlayToWin:
                    moveResponse = moves.Where(a => a.IsWin).RandomFromList(_random);
                    break;
                case ArtificialIntelligenceBehavior.PlayToTie:
                    moveResponse = moves.Where(a => a.IsTie).RandomFromList(_random);
                    break;
                case ArtificialIntelligenceBehavior.PlayToLose:
                    moveResponse = moves.Where(a => a.IsLoss).RandomFromList(_random);
                    break;
                default:
                    //there is a more specific exception for invalid enum but it lives in an unfortunate assembly.
                    throw new ArgumentOutOfRangeException(nameof(behavior));
            }

            if (moveResponse == null)
            {
                throw new GameException($"Unable to make a move because there are no available moves for game board {game.GameBoardString}");
            }

            game.Move(moveResponse.Response);
        }
    }
}
