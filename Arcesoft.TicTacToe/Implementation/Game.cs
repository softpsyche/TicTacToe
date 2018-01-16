using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.IO;
using System.Xml;
using System.Linq;

namespace Arcesoft.TicTacToe
{
    [Serializable]
    internal class Game : IGame
    {
        #region Private variables
        private Board _board = new Board();
        private Collection<Move> _moves = new Collection<Move>();
        private ISynchronizeInvoke synchronizingObject = null;
        #endregion
        #region Events
        public event EventHandler<EventArgs> GameOver;
        public event EventHandler GameReset;
        public event EventHandler<GameStateChangedEventArgs> GameStateChanged;

        private void OnGameOver()
        {
            EventHandler<EventArgs> temp = GameOver;

            if (temp != null)
            {
                if (synchronizingObject != null)
                {
                    synchronizingObject.Invoke(temp, new object[] { EventArgs.Empty });
                }
                else
                {
                    temp(this, EventArgs.Empty);
                }
            }
        }
        private void OnGameReset()
        {
            EventHandler temp = GameReset;

            if (temp != null)
            {
                if (synchronizingObject != null)
                {
                    synchronizingObject.Invoke(temp, new object[] { EventArgs.Empty });
                }
                else
                {
                    temp(this, EventArgs.Empty);
                }
            }
        }
        private void OnGameStateChanged(GameChange gameChange)
        {
            EventHandler<GameStateChangedEventArgs> temp = GameStateChanged;

            if (temp != null)
            {
                if (synchronizingObject != null)
                {
                    synchronizingObject.Invoke(temp, new object[] { new GameStateChangedEventArgs(_board.State, gameChange, playerTurn) });
                }
                else
                {
                    temp(this, new GameStateChangedEventArgs(_board.State, gameChange, playerTurn));
                }
            }

            switch (gameChange)
            {
                case GameChange.Reset:
                    OnGameReset();
                    break;
                case GameChange.Over:
                    OnGameOver();
                    break;
            }
        }
        #endregion
        #region Constructor(s)
        public Game()
        {

        }
		public Game(IEnumerable<Move> gameMoves)
		{
			foreach (var move in gameMoves)
			{
				if (IsMoveValid(move))
				{
					Move(move);
				}
				else
				{
					throw new GameException("Invalid move passed in. Cannot create game from moves.");
				}
			}
		}
        #endregion
        #region Properties
        public string GameBoardString => _board.ToString();
        public Player CurrentPlayer => _moves.Count % 2 == 0? Player.X: Player.O;
        public int TotalMovesMade => _moves.Count;
        public Move[] MoveHistory => _moves.ToArray();
        public GameState GameState => _board.State;
        public bool GameIsOver => GameState != GameState.InPlay;

        #endregion
        #region Methods

        public void Reset()
        {
            _board.Clear();

            _moves.Clear();

            OnGameStateChanged(GameChange.Reset);
        }
		public bool IsMoveValid(Move move)
		{
            return (!GameIsOver) && (_board.SquareIsEmpty(move));
        }

        public void UndoLastMove()
        {
            if (_moves.Count == 0)
            {
                throw new InvalidOperationException("No moves have been made yet.");
            }

            //undo the board move...
            _board[_moves.Last()] = Square.Empty;

            //delete move from history..
            _moves.Remove(_moves.Last());

            //raise this event
            OnGameStateChanged(GameChange.UndoMove);
        }

        public void Move(Move move)
        {
            if (_board.IsGameOver())
            {
                throw new GameException("Invalid move. The game is no longer in play.");
            }

            if (!_board.SquareIsEmpty(move))
            {
                throw new GameException("Invalid move. Square already occupied.");
            }

            //make your move man...
            _board[move] = CurrentPlayer == Player.X ? Square.X : Square.O;

            //update our history here...
            _moves.Add(move);

            //raise this event
            OnGameStateChanged(_board.IsGameOver() ? GameChange.Over : GameChange.Move);
        }

		public IEnumerable<Move> GetLegalMoves()
		{
            //if the board is no longer 'in play', we have no legal moves left..
            if (GameState != GameState.InPlay)
                return Enumerable.Empty<Move>();

            //the number of legal moves left can be inferred from 
            //the number of moves already made.
            List<Move> legalMoves = new List<Move>();

            foreach (Move move in Enum.GetValues(typeof(Move)))
            {
                if (_board[move] == Square.Empty)
                {
                    legalMoves.Add(move);
                }
            }

			return legalMoves;
		}
        #endregion
    }



    [Serializable()]
    public class GameStateChangedEventArgs : EventArgs
    {
        public GameChange GameChange { get; set; }
        public GameState GameState { get; set; }
        public Player CurrentPlayer { get; set; }


        public GameStateChangedEventArgs(GameState gameState, GameChange change, Player playerTurn)
        {
            GameChange = change;
            GameState = gameState;
            CurrentPlayer = playerTurn;

        }
    }

}
