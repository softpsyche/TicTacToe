using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.IO;
using System.Xml;
using Arcesoft.TicTacToe.RandomNumberGeneration;
using Arcesoft.TicTacToe.Entities;
using System.Diagnostics.CodeAnalysis;
using Arcesoft.TicTacToe.ArtificialIntelligence;
using Arcesoft.TicTacToe.GameImplementation;

namespace Arcesoft.TicTacToe.Database
{
    //We will exclude this from code coverage for this example because we assume that the 'Database'
    //is already actually built. 
    [ExcludeFromCodeCoverage()]
    internal class MoveEvaluator : IMoveEvaluator
    {
        private IRandom _random;
        private IBestMoveSelector _bestMoveSelector;

        public MoveEvaluator(IRandom random,IBestMoveSelector bestMoveSelector)
        {
            _random = random;
            _bestMoveSelector = bestMoveSelector;
        }
        public IEnumerable<BoardState> FindAllMoves(IGame game)
        {
            Collection<BoardState> results = new Collection<BoardState>();

            GetMinMaxResponseForGame(game, results);

            return results.Distinct(new BoardStateComparer());
        }

        #region Private Methods
        
        private MoveResult GetMinMaxResponseForGame(IGame game, Collection<BoardState> boardLayoutAndGameMoveResult)
        {
            Collection<MoveResult> gameMoveResults = new Collection<MoveResult>();
            var legalMoves = game.GetLegalMoves();

            foreach (var move in legalMoves)
            {
                MoveResult gameMoveResult;
                Player player = game.CurrentPlayer;
                game.Move(move);

                if (game.GameIsOver)
                {
                    gameMoveResult = new MoveResult(move, game.GameState);
                }
                else
                {
                    //recurse to find this moves finale
                    gameMoveResult = new MoveResult(move, GetMinMaxResponseForGame(game, boardLayoutAndGameMoveResult).GameStateAfterMove);
                }

                //add the result here..
                gameMoveResults.Add(gameMoveResult);

                //undo that last move...
                game.UndoLastMove();

                boardLayoutAndGameMoveResult.Add(new BoardState(
                    gameMoveResult,
                    game.GameBoardString,
                    player));
            }

            return _bestMoveSelector.FindRandomBestMoveResultForPlayerOrDefault(gameMoveResults, game.CurrentPlayer) as MoveResult;
        }
        #endregion
    }

}


