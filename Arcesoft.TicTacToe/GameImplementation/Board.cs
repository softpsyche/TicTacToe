using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.IO;
using System.Xml;
using System.Linq;
using Arcesoft.TicTacToe.Entities;

namespace Arcesoft.TicTacToe.GameImplementation
{
	[Serializable]
	internal class Board
	{
		#region Constants
		public const string BoardEmptyString = "_________";
		public const string SquareEmptyString = "_";
		public const string SquareOString = "O";
		public const string SquareXString = "X";
		public const char SquareEmptyChar = '_';
		public const char SquareOChar = 'O';
		public const char SquareXChar = 'X';
		#endregion
		#region Private variables
		private Square[] board = new Square[9];
		private GameState boardState = GameState.InPlay;
		#endregion

		#region Public
        public Square this[Move move]
        {

            get
            {
                return board[(int)move];
            }
            set
            {
                board[(int)move] = value;
                SetBoardState();
            }
        }
        public bool IsFull => board.Any(a => a == Square.Empty) == false;
        public GameState State => boardState;

        public Boolean IsGameOver() => State != GameState.InPlay;

        public bool SquareIsEmpty(Move move) => this[move] == Square.Empty;

		public void Clear()
		{
			for (int count = 0; count < board.Length; count++)
			{
				board[count] = Square.Empty;
			}

			boardState = GameState.InPlay;
		}
        public override string ToString() => BoardLine1 + BoardLine2 + BoardLine3;
        #endregion
        #region Private Methods
        private string BoardLine1 => TranslateBoardSquare(board[0]) + TranslateBoardSquare(board[1]) + TranslateBoardSquare(board[2]);
        private string BoardLine2 => TranslateBoardSquare(board[3]) + TranslateBoardSquare(board[4]) + TranslateBoardSquare(board[5]);
        private string BoardLine3 => TranslateBoardSquare(board[6]) + TranslateBoardSquare(board[7]) + TranslateBoardSquare(board[8]);

        private string TranslateBoardSquare(Square square)
        {
            switch (square)
            {
                case Square.Empty:
                    return SquareEmptyString;
                case Square.O:
                    return SquareOString;
                case Square.X:
                    return SquareXString;
                default:
                    throw new InvalidEnumArgumentException("Invalid/Unknown Square enumeration.");
            }
        }

        private void SetBoardState()
        {
            if (CheckAndSetBoardStateForLine(0, 1, 2))
                return;
            if (CheckAndSetBoardStateForLine(3, 4, 5))
                return;
            if (CheckAndSetBoardStateForLine(6, 7, 8))
                return;
            if (CheckAndSetBoardStateForLine(0, 3, 6))
                return;
            if (CheckAndSetBoardStateForLine(1, 4, 7))
                return;
            if (CheckAndSetBoardStateForLine(2, 5, 8))
                return;
            if (CheckAndSetBoardStateForLine(0, 4, 8))
                return;
            if (CheckAndSetBoardStateForLine(6, 4, 2))
                return;

            boardState = IsFull ? GameState.Tie : GameState.InPlay;
        }

        private bool CheckAndSetBoardStateForLine(int squareIndex0, int squareIndex1, int squareIndex2)
        {
            if ((board[squareIndex0] == board[squareIndex1]) &&
                (board[squareIndex1] == board[squareIndex2]))
            {
                if (board[squareIndex0] == Square.X)
                {
                    boardState = GameState.XWin;
                    return true;
                }
                else if (board[squareIndex0] == Square.O)
                {
                    boardState = GameState.OWin;
                    return true;
                }
            }

            return false;
        }
        #endregion
    }
}
