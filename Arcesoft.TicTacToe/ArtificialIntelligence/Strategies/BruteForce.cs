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
            //we make a copy because its polite to not inadvertantly mess up 
            //someone elses object they pass in (in case of exceptions)
            var gameCopy = _ticTacToeFactory.NewGame(game.MoveHistory);

            var bestMoves = CalculateBestMoves(_ticTacToeFactory.NewGame(game.MoveHistory));

            var moveResult = randomlySelectIfMoreThanOne ? bestMoves.RandomFromListOrDefault(_random) : bestMoves.First();
            game.Move(moveResult.MoveMade);
        }

        #region Private
        private IEnumerable<MoveResult> CalculateBestMoves(IGame game)
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
                    //recurse to find this moves finale
                    gameMoveResults.Add(new MoveResult(move,
                        CalculateBestMoves(game).FirstOrDefault().BoardStateAfterMove));
                }

                game.UndoLastMove();
            }

            return SelectBestMovesForPlayer(gameMoveResults, game.CurrentPlayer);
        }
        private IEnumerable<MoveResult> SelectBestMovesForPlayer(IEnumerable<MoveResult> gameMoveResults, Player player)
        {
            var winState = (player == Player.X) ? GameState.XWin : GameState.OWin;
            var loseState = (player == Player.X) ? GameState.OWin : GameState.XWin;

            //gotta know when to hold em...
            var bestMoves = gameMoveResults.Where(a => a.BoardStateAfterMove == winState);
            if (bestMoves.Any())
            {
                return bestMoves;
            }

            bestMoves = gameMoveResults.Where(a => a.BoardStateAfterMove == GameState.Tie);
            if (bestMoves.Any())
            {
                return bestMoves;
            }

            return gameMoveResults.Where(a => a.BoardStateAfterMove == loseState);
        }

        #endregion
    }
}
