using Arcesoft.TicTacToe.Entities;
using Arcesoft.TicTacToe.GameImplementation;
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
        private IBestMoveSelector _bestMoveSelector;

        public BruteForce(ITicTacToeFactory ticTacToeFactory,IBestMoveSelector bestMoveSelector)
        {
            _ticTacToeFactory = ticTacToeFactory;
            _bestMoveSelector = bestMoveSelector;
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

            IMoveResult moveResult;
            if (randomlySelectIfMoreThanOne)
            {
                moveResult = _bestMoveSelector.FindRandomBestMoveResultForPlayerOrDefault(moveResults, gameCopy.CurrentPlayer);
            }
            else
            {
                moveResult = _bestMoveSelector.FindBestMoveResultsForPlayer(moveResults, gameCopy.CurrentPlayer).First();
            }

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
                    var bestMoveResultsForCurrentPlayer = _bestMoveSelector
                        .FindBestMoveResultsForPlayer(FindMoveResultsRecursively(game), game.CurrentPlayer);

                    //recurse to find this moves finale
                    gameMoveResults.Add(new MoveResult(move,
                        bestMoveResultsForCurrentPlayer.FirstOrDefault().GameStateAfterMove));
                }

                game.UndoLastMove();
            }

            return gameMoveResults;
        }
    }
}
