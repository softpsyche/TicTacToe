using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.IO;
using System.Xml;
using Arcesoft.TicTacToe.RandomNumberGeneration;
using Arcesoft.TicTacToe.Entities;

namespace Arcesoft.TicTacToe.ArtificialIntelligence
{
    internal class MoveEvaluator : IMoveEvaluator
    {
        private ITicTacToeFactory _gameFactory;
        private IRandom _random;

        public MoveEvaluator(ITicTacToeFactory gameFactory, IRandom random)
        {
            _gameFactory = gameFactory;
            _random = random;
        }
        //public MoveResult CalculateBestMove(IGame game, bool randomlySelectIfMoreThanOne)
        //{
        //    return CalculateBestMoveInternal(_gameFactory.NewGame(game.MoveHistory), randomlySelectIfMoreThanOne);
        //}

        public IEnumerable<BoardState> FindAllMoves(IGame game = null)
        {
            game = game ?? _gameFactory.NewGame();
            Collection<BoardState> results = new Collection<BoardState>();

            GetMinMaxResponseForGame(game, results);

            return results;
        }

        #region Private Methods
        //private MoveResult CalculateBestMoveInternal(IGame game, bool randomlySelectIfMoreThanOne)
        //{
        //    Collection<MoveResult> gameMoveResults = new Collection<MoveResult>();
        //    var legalMoves = game.GetLegalMoves();

        //    foreach (var move in legalMoves)
        //    {
        //        game.Move(move);

        //        if (game.GameIsOver)
        //        {
        //            gameMoveResults.Add(new MoveResult(move, game.GameState));
        //        }
        //        else
        //        {
        //            //recurse to find this moves finale
        //            gameMoveResults.Add(new MoveResult(move,
        //                CalculateBestMove(game, randomlySelectIfMoreThanOne).BoardStateAfterMove));
        //        }

        //        game.UndoLastMove();
        //    }

        //    return gameMoveResults[FindBestMoveIndex(gameMoveResults, game.CurrentPlayer)];
        //}   
        private MoveResult GetMinMaxResponseForGame(IGame game, Collection<BoardState> boardLayoutAndGameMoveResult)
        {
            Collection<MoveResult> gameMoveResults = new Collection<MoveResult>();
            int bestMoveIndex;
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
                    gameMoveResult = new MoveResult(move, GetMinMaxResponseForGame(game, boardLayoutAndGameMoveResult).BoardStateAfterMove);
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

            bestMoveIndex = FindBestMoveIndex(gameMoveResults, game.CurrentPlayer);

            return gameMoveResults[bestMoveIndex];
        }
        private int FindBestMoveIndex(Collection<MoveResult> gameMoveResults, Player side)
        {
            Collection<int> winningMovesIndexes = new Collection<int>();
            Collection<int> tieMovesIndexes = new Collection<int>();
            Collection<int> losingMovesIndexes = new Collection<int>();

            if (side == Player.O)
            {
                for (int count = 0; count < gameMoveResults.Count; count++)
                {
                    switch (gameMoveResults[count].BoardStateAfterMove)
                    {
                        case GameState.OWin:
                            winningMovesIndexes.Add(count);
                            break;
                        case GameState.Tie:
                            tieMovesIndexes.Add(count);
                            break;
                        default:
                            losingMovesIndexes.Add(count);
                            break;
                    }
                }
            }
            else
            {
                for (int count = 0; count < gameMoveResults.Count; count++)
                {
                    switch (gameMoveResults[count].BoardStateAfterMove)
                    {
                        case GameState.XWin:
                            winningMovesIndexes.Add(count);
                            break;
                        case GameState.Tie:
                            tieMovesIndexes.Add(count);
                            break;
                        default:
                            losingMovesIndexes.Add(count);
                            break;
                    }
                }
            }

            if (winningMovesIndexes.Count > 0)
            {
                return winningMovesIndexes[_random.Next(winningMovesIndexes.Count)];
            }
            else if (tieMovesIndexes.Count > 0)
            {
                return tieMovesIndexes[_random.Next(tieMovesIndexes.Count)];
            }
            else if (losingMovesIndexes.Count > 0)
            {
                return losingMovesIndexes[_random.Next(losingMovesIndexes.Count)];
            }
            else
                throw new ArgumentException("gameMoveResults collection is either empty or corrupt");
        }
        #endregion
    }
    
}


