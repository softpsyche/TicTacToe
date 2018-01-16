using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.IO;
using System.Xml;
using System.Linq;

namespace TicTacToe
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
		private string boardString = null;
		#endregion
		#region Constructor(s)

		#endregion
		#region Properties
		//public Square this[int index]
		//{
		//    get
		//    {
		//        return board[index];
		//    }
		//    set
		//    {
		//        board[index] = value;
		//        this.SetBoardState();
		//    }
		//}
		public Square this[int row,int column]
		{
			get
			{
				return board[(row * 3) + column];
			}
			set
			{
				board[(row * 3) + column] = value;
				this.SetBoardState();
			}
		}
		public bool IsEmpty
		{
			get
			{
				foreach (Square square in board)
				{
					if (square != Square.Empty)
						return false;
				}

				return true;
			}
		}
		public bool IsFull
		{
			get
			{
				foreach (Square square in board)
				{
					if (square == Square.Empty)
						return false;
				}

				return true;
			}
		}
		public GameState State
		{
			get
			{
				return boardState;
			}
		}
		public string BoardLine1
		{
			get
			{
				return 
					TranslateBoardSquare(this.board[0]) +
					TranslateBoardSquare(this.board[1]) +
					TranslateBoardSquare(this.board[2]);
			}
		}
		public string BoardLine2
		{
			get
			{
				return 
					TranslateBoardSquare(this.board[3]) +
					TranslateBoardSquare(this.board[4]) +
					TranslateBoardSquare(this.board[5]);
			}
		}
		public string BoardLine3
		{
			get
			{
				return 
					TranslateBoardSquare(this.board[6]) +
					TranslateBoardSquare(this.board[7]) +
					TranslateBoardSquare(this.board[8]);
			}
		}
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
                    throw new GameException("Invalid/Unknown Square enumeration.");
			}
		}
        private Square TranslateBoardString(String str)
        {
            switch (str.ToUpper())
            {
                case "_":
                    return Square.Empty;
                case "O":
                    return Square.O;
                case "X":
                    return Square.X;
                default:
                    throw new GameException(string.Format("Invalid/Unknown board string '{0}'",str));
            }
        }
		#endregion
		#region Private Methods
		private void SetBoardState()
		{
			this.boardString = null;

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
		//private void SetBoardState()
		//{
		//    bool
		//        CheckWinRow0 = true,
		//        CheckWinRow1 = true,
		//        CheckWinRow2 = true,
		//        CheckWinCol0 = true,
		//        CheckWinCol1 = true,
		//        CheckWinCol2 = true,
		//        CheckWinDiagonalFalling = true,
		//        CheckWinDiagonalRising = true;

		//    //check first square first..
		//    if (this[0,0] == Square.Empty)
		//    {
		//        CheckWinCol0 = false;
		//        CheckWinRow0 = false;
		//        CheckWinDiagonalFalling = false;
		//    }

		//    //check middle square next..
		//    if (this[1, 1] == Square.Empty)
		//    {
		//        CheckWinCol1 = false;
		//        CheckWinRow1 = false;
		//        CheckWinDiagonalFalling = false;
		//        CheckWinDiagonalRising = false;
		//    }

		//    //check last square last..
		//    if (this[2,2] == Square.Empty)
		//    {
		//        CheckWinCol2 = false;
		//        CheckWinRow2 = false;
		//        CheckWinDiagonalFalling = false;
		//    }

		//    if (CheckWinRow0 && CheckAndSetBoardStateForLine(0, 1, 2))
		//        return;
		//    if (CheckWinRow1 && CheckAndSetBoardStateForLine(3, 4, 5))
		//        return;
		//    if (CheckWinRow2 && CheckAndSetBoardStateForLine(6, 7, 8))

		//        return;
		//    if (CheckWinCol0 && CheckAndSetBoardStateForLine(0, 3, 6))
		//        return;
		//    if (CheckWinCol1 && CheckAndSetBoardStateForLine(1, 4, 7))
		//        return;
		//    if (CheckWinCol2 && CheckAndSetBoardStateForLine(2, 5, 8))
		//        return;

		//    if (CheckWinDiagonalFalling && CheckAndSetBoardStateForLine(0, 4, 8))
		//        return;
		//    if (CheckWinDiagonalRising && CheckAndSetBoardStateForLine(6, 4, 2))
		//        return;

		//    if (IsFull)
		//    {
		//        boardState = BoardState.Tie;
		//        return;
		//    }
		//    else
		//        boardState = BoardState.InPlay;
		//}
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
        public Boolean IsGameOver()
        {
            return this.State != GameState.InPlay;
        }
		public bool SquareIsEmpty(int row, int column)
		{
			if (this[row, column] == Square.Empty)
				return true;
			else
				return false;
		}
		public void Clear()
		{
			for (int count = 0; count < board.Length; count++)
			{
				board[count] = Square.Empty;
			}

			boardState = GameState.InPlay;
		}
		public override string ToString()
		{
			if (this.boardString == null)
			{
				this.boardString = BoardLine1 + BoardLine2 + BoardLine3;
			}

			return this.boardString;
		}

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

            this.Clear();

            this.board = moves.ToArray();
            this.SetBoardState();
        }
		#endregion
	}
}
