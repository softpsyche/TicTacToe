using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Arcesoft.TicTacToe.Entities;
using Arcesoft.TicTacToe.RandomNumberGeneration;
using Arcesoft.TicTacToe.Data;
using Arcesoft.TicTacToe.GameImplementation;

namespace Arcesoft.TicTacToe.ArtificialIntelligence.Strategies
{
    /// <summary>
    /// Its like playing against a god. All knowing, all seeing, never losing. Very boring. This one 
    /// </summary>
    internal class OmniscientGod : IArtificialIntelligence
    {
        private IMoveDataAccess _moveDataAccess;
        private IBestMoveSelector _bestMoveSelector;

        public OmniscientGod(IMoveDataAccess moveDataAccess, IBestMoveSelector bestMoveSelector)
        {
            _moveDataAccess = moveDataAccess;
            _bestMoveSelector = bestMoveSelector;
        }

        public void MakeMove(IGame game, bool randomlySelectIfMoreThanOne = true)
        {
            if (game.GameIsOver)
            {
                throw new GameException($"Unable to make a move because the game is over.");
            }

            var moves = _moveDataAccess.FindMoveResponses(game.GameBoardString, game.CurrentPlayer);
            var moveResult = _bestMoveSelector.FindRandomBestMoveResultForPlayerOrDefault(moves, game.CurrentPlayer);

            if (randomlySelectIfMoreThanOne)
            {
                moveResult = _bestMoveSelector.FindRandomBestMoveResultForPlayerOrDefault(moves, game.CurrentPlayer);
            }
            else
            {
                moveResult = _bestMoveSelector.FindBestMoveResultsForPlayer(moves, game.CurrentPlayer).First();
            }

            if (moveResult == null)
            {
                //this should NEVER happen if the game is not over. This is a true exceptional case
                throw new Exception($"Unable to make a move because there are no available moves for game board {game.GameBoardString}. Possible corrupt move data access or game.");
            }

            game.Move(moveResult.MoveMade);
        }

        public IEnumerable<MoveResult> FindMoveResults(IGame game)
        {
            return _moveDataAccess
                .FindMoveResponses(game.GameBoardString, game.CurrentPlayer)
                .Select(a => new MoveResult(a.Response, a.Outcome))
                .ToList();
        }
    }
}
