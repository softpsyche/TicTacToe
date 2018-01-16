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
	public class Board
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
		#region Constructor(s)

		#endregion
		#region Properties
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
        public bool IsEmpty => board.All(a => a == Square.Empty);
        public bool IsFull => board.Any(a => a == Square.Empty) == false;
        public GameState State => boardState;
        public string BoardLine1 => TranslateBoardSquare(board[0]) + TranslateBoardSquare(board[1]) + TranslateBoardSquare(board[2]);
        public string BoardLine2 => TranslateBoardSquare(board[3]) + TranslateBoardSquare(board[4]) + TranslateBoardSquare(board[5]);
		public string BoardLine3 => TranslateBoardSquare(board[6]) + TranslateBoardSquare(board[7]) + TranslateBoardSquare(board[8]);

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
        private Square TranslateBoardString(String boardString)
        {
            switch (boardString.ToUpper())
            {
                case "_":
                    return Square.Empty;
                case "O":
                    return Square.O;
                case "X":
                    return Square.X;
                default:
                    throw new GameException(string.Format("Invalid/Unknown board string '{0}'",boardString));
            }
        }
		#endregion
		#region Private Methods
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

			if (IsFull)
			{
				boardState = GameState.Tie;
				return;
			}
			else
				boardState = GameState.InPlay;
		}

		private bool CheckAndSetBoardStateForLine(int squareIndex0,int squareIndex1,int squareIndex2)
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
		#region Public Methods
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

        public void Load(String boardString)
        {
            if (String.IsNullOrWhiteSpace(boardString))
                throw new GameException("Cannot load empty or null boardstring. Invalid boardstring.");

            if (boardString.Length != 9)
                throw new GameException("Board string must be 9 characters long. Invalid boardstring.");

            var moves = boardString.ToCharArray()
                .Select(a => TranslateBoardString(a.ToString())).ToList();

            var xMoves = moves.Count(a => a == Square.X);
            var oMoves = moves.Count(a => a == Square.O);

            if ((Math.Abs(xMoves - oMoves) > 1) && (oMoves > xMoves))
                throw new GameException("Board contains impossible move configuration. Invalid boardstring.");

            //NOTE: there are other checks for valid board positions, perhaps running this versus our database would be
            //move prudent...

            Clear();

            board = moves.ToArray();
            SetBoardState();
        }
		#endregion
	}
}
