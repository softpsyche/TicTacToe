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
    public class Game : IGame
    {
        #region Private variables
        private Board gameBoard = new Board();
        private Player playerTurn = Player.X;
        private int movesMade = 0;
        private Collection<GameMove> gameMoves = new Collection<GameMove>();
        private ISynchronizeInvoke synchronizingObject = null;
        #endregion
        #region Events
        public event EventHandler<EventArgs> GameOver;
        public event EventHandler GameReset;
        public event EventHandler<GameStateChangedEventArgs> GameStateChanged;

        private void OnGameOver()
        {
            EventHandler<EventArgs> temp = this.GameOver;

            if (temp != null)
            {
                if (this.synchronizingObject != null)
                {
                    this.synchronizingObject.Invoke(temp, new object[] { EventArgs.Empty });
                }
                else
                {
                    temp(this, EventArgs.Empty);
                }
            }
        }
        private void OnGameReset()
        {
            EventHandler temp = this.GameReset;

            if (temp != null)
            {
                if (this.synchronizingObject != null)
                {
                    this.synchronizingObject.Invoke(temp, new object[] { EventArgs.Empty });
                }
                else
                {
                    temp(this, EventArgs.Empty);
                }
            }
        }
        private void OnGameStateChanged(GameChange gameChange)
        {
            EventHandler<GameStateChangedEventArgs> temp = this.GameStateChanged;

            if (temp != null)
            {
                if (this.synchronizingObject != null)
                {
                    this.synchronizingObject.Invoke(temp, new object[] { new GameStateChangedEventArgs(this.gameBoard.State, gameChange, this.playerTurn) });
                }
                else
                {
                    temp(this, new GameStateChangedEventArgs(this.gameBoard.State, gameChange, this.playerTurn));
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
		[Obsolete("Use the other contructor instead")]
		public Game(String boardState)
		{
			this.LoadFromBoardState(boardState);
		}
		public Game(IEnumerable<GameMove> gameMoves)
		{
			foreach (var move in gameMoves)
			{
				if (IsMoveValid(move))
				{
					MakeMove(move);
				}
				else
				{
					throw new GameException("Invalid move passed in. Cannot create game from moves.");
				}
			}
		}
        #endregion
        #region Properties
        public string GameBoardString
        {
            get
            {
                return this.gameBoard.ToString();
            }
        }
        public ISynchronizeInvoke SynchronizingObject
        {
            set
            {
                this.synchronizingObject = value;
            }
        }
        public Player OtherPlayerTurn
        {
            get
            {
                if (playerTurn == Player.O)
                    return Player.X;
                else
                    return Player.O;
            }
        }
        public Player PlayerTurn
        {
            get
            {
                return playerTurn;
            }
        }
        public int MovesMade
        {
            get
            {
                return this.movesMade;
            }
        }
        public GameMove[] GameHistory
        {
            get
            {
                GameMove[] gameHistoryArray = new GameMove[gameMoves.Count];

                gameMoves.CopyTo(gameHistoryArray, 0);

                return gameHistoryArray;
            }
        }
        public GameState GameState
        {
            get
            {
                return this.gameBoard.State;
            }
        }

        public bool IsOver
        {
            get
            {
                return GameState != GameState.InPlay;
            }
        }
        #endregion
        #region Methods

        public void Reset()
        {
            this.gameBoard.Clear();

            this.gameMoves.Clear();

            this.playerTurn = Player.X;

            this.movesMade = 0;

            this.OnGameStateChanged(GameChange.Reset);
        }
		public bool IsMoveValid(GameMove gameMove)
		{
			return IsMoveValid(gameMove.Row, gameMove.Column);
		}
        public bool IsMoveValid(int row, int column)
        {
            return ((!IsOver) && (gameBoard.SquareIsEmpty(row, column)));
        }

        public void UndoLastMove()
        {
            if (gameMoves.Count == 0)
            {
                throw new InvalidOperationException("No moves have been made yet.");
            }

            //undo the board move...
            this.gameBoard[
                gameMoves[gameMoves.Count - 1].Row,
                gameMoves[gameMoves.Count - 1].Column] = Square.Empty;

            //delete move from history..
            this.gameMoves.RemoveAt(gameMoves.Count - 1);

            //flip the player turn to the previous player...
            this.FlipPlayerTurn();

            //decrement movesmade counter..
            this.movesMade--;

            //raise this event
            this.OnGameStateChanged(GameChange.UndoMove);
        }

        public void MakeMove(MoveDirection moveDirection)
        {
            MakeMove(moveDirection.ToGameMove());
        }
        public void MakeMove(int row, int column)
        {
            this.MakeMove(new GameMove(row, column));
        }
        public void MakeMove(GameMove gameMove)
        {
            if (gameBoard.IsGameOver())
            {
                throw new GameException("Invalid move. The game is no longer in play.");
            }

            if (!gameBoard.SquareIsEmpty(gameMove.Row, gameMove.Column))
            {
                throw new GameException("Invalid move. Square already occupied.");
            }

            if (playerTurn == Player.X)
            {
                gameBoard[gameMove.Row, gameMove.Column] = Square.X;
            }
            else
            {
                gameBoard[gameMove.Row, gameMove.Column] = Square.O;
            }

            //update our history here...
            gameMoves.Add(gameMove);

            //flip the player turn...
            this.FlipPlayerTurn();

            //increment movesmade counter.
            this.movesMade++;

            //raise this event
            this.OnGameStateChanged(this.gameBoard.IsGameOver() ? GameChange.Over : GameChange.Move);
        }

		public GameMove[] GetLegalMoves()
		{

			//if the board is no longer 'in play', we have no legal moves left..
			if (GameState != GameState.InPlay)
				return new GameMove[0];

			//the number of legal moves left can be inferred from 
			//the number of moves already made.
			GameMove[] legalMoves = new GameMove[9 - gameMoves.Count];
			int count = 0;

			for (int row = 0; row < 3; row++)
			{
				for (int column = 0; column < 3; column++)
				{
					if (gameBoard[row, column] == Square.Empty)
					{
						legalMoves[count] = new GameMove(row, column);
						count++;
					}
				}
			}

			return legalMoves;
		}

        private void FlipPlayerTurn()
        {
            if (playerTurn == Player.O)
                playerTurn = Player.X;
            else
                playerTurn = Player.O;
        }
        private void LoadFromBoardState(String boardState)
        {
            this.Reset();

            this.gameBoard.Load(boardState);
        }
        #endregion
    }

    public interface IGame
    {
        string GameBoardString { get; }
        global::TicTacToe.GameMove[] GameHistory { get; }
        event EventHandler<EventArgs> GameOver;
        event EventHandler GameReset;
        global::TicTacToe.GameState GameState { get; }
        event EventHandler<global::TicTacToe.GameStateChangedEventArgs> GameStateChanged;
        bool IsMoveValid(int row, int column);
        bool IsMoveValid(global::TicTacToe.GameMove gameMove);
        bool IsOver { get; }
		GameMove[] GetLegalMoves();
        void MakeMove(int row, int column);
        void MakeMove(global::TicTacToe.GameMove gameMove);
        int MovesMade { get; }
        global::TicTacToe.Player OtherPlayerTurn { get; }
        global::TicTacToe.Player PlayerTurn { get; }
        void Reset();
        global::System.ComponentModel.ISynchronizeInvoke SynchronizingObject { set; }
        void UndoLastMove();
    }

    [Serializable()]
    public class GameStateChangedEventArgs : EventArgs
    {
        public GameChange GameChange { get; set; }
        public GameState GameState { get; set; }
        public Player CurrentPlayer { get; set; }


        public GameStateChangedEventArgs(GameState gameState, GameChange change, Player playerTurn)
        {
            this.GameChange = change;
            this.GameState = gameState;
            this.CurrentPlayer = playerTurn;

        }
    }

}
