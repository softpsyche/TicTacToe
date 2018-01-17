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
    [Serializable]
    internal class God
    {
        #region Private Variables
        private IGame _game = null;
        private IRandom _random = null;
        #endregion
        #region Private Methods
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
                return winningMovesIndexes[_random.Next( winningMovesIndexes.Count)];
            }
            else if (tieMovesIndexes.Count > 0)
            {
                return tieMovesIndexes[_random.Next( tieMovesIndexes.Count)];
            }
            else if (losingMovesIndexes.Count > 0)
            {
                return losingMovesIndexes[_random.Next(losingMovesIndexes.Count)];
            }
            else
                throw new ArgumentException("gameMoveResults collection is either empty or corrupt");
        }
        private MoveResult GetMinMaxResponseForGame(Collection<BoardLayoutAndGameMoveResult> boardLayoutAndGameMoveResult)
        {
            Collection<MoveResult> gameMoveResults = new Collection<MoveResult>();
            int bestMoveIndex;
			var legalMoves = _game.GetLegalMoves();

			foreach (var move in legalMoves)
            {
                MoveResult gameMoveResult;
                Player player = _game.CurrentPlayer;
                _game.Move(move);

                if (_game.GameIsOver)
                {
                    gameMoveResult = new MoveResult(move,_game.GameState);
                }
                else
                {
                    //recurse to find this moves finale
                    gameMoveResult = new MoveResult(move,GetMinMaxResponseForGame( boardLayoutAndGameMoveResult).BoardStateAfterMove);
                }

                //add the result here..
                gameMoveResults.Add(gameMoveResult);

                //undo that last move...
                _game.UndoLastMove();

                boardLayoutAndGameMoveResult.Add(new BoardLayoutAndGameMoveResult(
                    gameMoveResult,
                    _game.GameBoardString,
                    player));
            }

            bestMoveIndex = FindBestMoveIndex(gameMoveResults, _game.CurrentPlayer);

            return gameMoveResults[bestMoveIndex];
        }
        #endregion
        #region Public Methods
        public God(IGame game, IRandom random)
        {
            _game = game;
            _random = random;
        }

        public MoveResult GetBestMove()
        {
            Collection<MoveResult> gameMoveResults = new Collection<MoveResult>();
			var legalMoves = _game.GetLegalMoves();

			foreach (var move in legalMoves)
            {
                _game.Move(move);

                if (_game.GameIsOver)
                {
                    gameMoveResults.Add(new MoveResult(move, _game.GameState));
                }
                else
                {
                    //recurse to find this moves finale
                    gameMoveResults.Add(new MoveResult(move,
                        GetBestMove().BoardStateAfterMove));
                }

                _game.UndoLastMove();
            }

            return gameMoveResults[FindBestMoveIndex(gameMoveResults, _game.CurrentPlayer)];
        }
        public void GetAllResponsesForGame(Collection<BoardLayoutAndGameMoveResult> boardLayoutAndGameMoveResult)
        {
            GetMinMaxResponseForGame(boardLayoutAndGameMoveResult);
        }
        
        #endregion
    }
    
}


