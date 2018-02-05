using Arcesoft.TicTacToe.Entities;
using Arcesoft.TicTacToe.RandomNumberGeneration;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arcesoft.TicTacToe.ArtificialIntelligence.Strategies
{
    internal class BruteForce : IArtificialIntelligence
    {
        private ITicTacToeFactory _ticTacToeFactory;
        private readonly IRandom _random;

        public BruteForce(ITicTacToeFactory ticTacToeFactory, IRandom random)
        {
            _ticTacToeFactory = ticTacToeFactory;
            _random = random;
        }

        public void MakeMove(IGame game, bool randomlySelectIfMoreThanOne = true)
        {
            if (game.GameIsOver)
            {
                throw new GameException($"Unable to make a move because the game is over.");
            }

            //we make a copy because its polite to not inadvertantly mess up 
            //someone elses object they pass in (in case of exceptions)
            var gameCopy = _ticTacToeFactory.NewGame(game.MoveHistory);

            var moveResults = FindMoveResultsRecursively(gameCopy);

            var bestMoves = SelectBestMovesForPlayer(moveResults, gameCopy.CurrentPlayer);

            var moveResult = randomlySelectIfMoreThanOne ? bestMoves.RandomFromListOrDefault(_random) : bestMoves.First();

            game.Move(moveResult.MoveMade);
        }

        public IEnumerable<MoveResult> FindMoveResults(IGame game)
        {
            //we make a copy because its polite to not inadvertantly mess up 
            //someone elses object they pass in (in case of exceptions)
            return FindMoveResultsRecursively(_ticTacToeFactory.NewGame(game.MoveHistory));
        }

        private IEnumerable<MoveResult> FindMoveResultsRecursively(IGame game)
        {
            Collection<MoveResult> gameMoveResults = new Collection<MoveResult>();
            var legalMoves = game.GetLegalMoves();

            foreach (var move in legalMoves)
            {
                game.Move(move);

                if (game.GameIsOver)
                {
                    gameMoveResults.Add(new MoveResult(move, game.GameState));
                }
                else
                {
                    var bestMoveResultsForCurrentPlayer = SelectBestMovesForPlayer(FindMoveResultsRecursively(game), game.CurrentPlayer);

                    //recurse to find this moves finale
                    gameMoveResults.Add(new MoveResult(move,
                        bestMoveResultsForCurrentPlayer.FirstOrDefault().GameStateAfterMove));
                }

                game.UndoLastMove();
            }

            return gameMoveResults;
        }

        #region Private

        private IEnumerable<MoveResult> SelectBestMovesForPlayer(IEnumerable<MoveResult> gameMoveResults, Player player)
        {
            var winState = (player == Player.X) ? GameState.XWin : GameState.OWin;
            var loseState = (player == Player.X) ? GameState.OWin : GameState.XWin;

            //gotta know when to hold em...
            var bestMoves = gameMoveResults.Where(a => a.GameStateAfterMove == winState);
            if (bestMoves.Any())
            {
                return bestMoves;
            }

            bestMoves = gameMoveResults.Where(a => a.GameStateAfterMove == GameState.Tie);
            if (bestMoves.Any())
            {
                return bestMoves;
            }

            return gameMoveResults.Where(a => a.GameStateAfterMove == loseState);
        }

        #endregion
    }
}
