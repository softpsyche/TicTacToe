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
    public class GameMove
    {
        public GameMove()
        {

        }
        public GameMove(int row, int column)
        {
			//if (row < 0 || row > 2 || column < 0 || column > 2)
			//{
			//	throw new ArgumentException("both row and column must be values between 0 and 2 (inclusive).");
			//}

            this.Row = row;
            this.Column = column;
        }
        public int Row
        {
            get;
            set;
        }
        public int Column
        {
            get;
            set;
        }

		public Int32 ToInteger()
		{
			return ToInteger(this);
		}

		public static Int32 ToInteger(GameMove gameMove)
		{
			return (gameMove.Row * 3) + gameMove.Column;
		}

		public static GameMove FromInteger(Int32 value)
		{
			return new GameMove(value / 3, value % 3);
		}
    }
}
