using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.IO;
using System.Xml;




namespace TicTacToe
{
    [Serializable]
    public class ArtificialIntelligence
    {
        #region Private Variables
        private IGame game = null;
        private IRandom random = null;
        #endregion
        #region Private Methods
        private int FindBestMoveIndex(Collection<GameMoveResult> gameMoveResults, Player side)
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
                return winningMovesIndexes[random.Next( winningMovesIndexes.Count)];
            }
            else if (tieMovesIndexes.Count > 0)
            {
                return tieMovesIndexes[random.Next( tieMovesIndexes.Count)];
            }
            else if (losingMovesIndexes.Count > 0)
            {
                return losingMovesIndexes[random.Next(losingMovesIndexes.Count)];
            }
            else
                throw new ArgumentException("gameMoveResults collection is either empty or corrupt");
        }
        private GameMoveResult GetMinMaxResponseForGame(Collection<BoardLayoutAndGameMoveResult> boardLayoutAndGameMoveResult)
        {
            Collection<GameMoveResult> gameMoveResults = new Collection<GameMoveResult>();
            int bestMoveIndex;
			var legalMoves = game.GetLegalMoves();

			foreach (GameMove gameMove in legalMoves)
            {
                GameMoveResult gameMoveResult;
                Player player = game.PlayerTurn;
                game.MakeMove(gameMove);

                if (game.IsOver)
                {
                    gameMoveResult = new GameMoveResult(gameMove,
                        game.GameState);
                }
                else
                {
                    //recurse to find this moves finale
                    gameMoveResult = new GameMoveResult(gameMove,
                        GetMinMaxResponseForGame( boardLayoutAndGameMoveResult).BoardStateAfterMove);
                }

                //add the result here..
                gameMoveResults.Add(gameMoveResult);

                //undo that last move...
                game.UndoLastMove();

                boardLayoutAndGameMoveResult.Add(new BoardLayoutAndGameMoveResult(
                    gameMoveResult,
                    game.GameBoardString,
                    player));
            }

            bestMoveIndex = FindBestMoveIndex(gameMoveResults, game.PlayerTurn);

            return gameMoveResults[bestMoveIndex];
        }
        #endregion
        #region Public Methods
        public ArtificialIntelligence(IGame game, IRandom random)
        {
            this.game = game;
            this.random = random;
        }

        public GameMoveResult GetBestMove()
        {
            Collection<GameMoveResult> gameMoveResults = new Collection<GameMoveResult>();
			var legalMoves = game.GetLegalMoves();

			foreach (GameMove gameMove in legalMoves)
            {
                game.MakeMove(gameMove);

                if (game.IsOver)
                {
                    gameMoveResults.Add(new GameMoveResult(gameMove, game.GameState));
                }
                else
                {
                    //recurse to find this moves finale
                    gameMoveResults.Add(new GameMoveResult(gameMove,
                        GetBestMove().BoardStateAfterMove));
                }

                game.UndoLastMove();
            }

            return gameMoveResults[FindBestMoveIndex(gameMoveResults, this.game.PlayerTurn)];
        }
        public void GetAllResponsesForGame(Collection<BoardLayoutAndGameMoveResult> boardLayoutAndGameMoveResult)
        {
            GetMinMaxResponseForGame(boardLayoutAndGameMoveResult);
        }
        


        #endregion
    }
    
}


