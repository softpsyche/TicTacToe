using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    public static class Extensions
    {
        public static GameMove ToGameMove(this MoveDirection moveDirection)
        {
            Int32 row = 0, column = 0;

            switch (moveDirection)
            {
                case MoveDirection.NorthWest:
                    break;
                case MoveDirection.Northern:
                    column = 1;
                    break;
                case MoveDirection.NorthEast:
                    column = 2;
                    break;
                case MoveDirection.Western:
                    row = 1;
                    break;
                case MoveDirection.Center:
                    row = 1;
                    column = 1;
                    break;
                case MoveDirection.Eastern:
                    row = 1;
                    column = 2;
                    break;
                case MoveDirection.SouthWest:
                    row = 2;
                    break;
                case MoveDirection.Southern:
                    row = 2;
                    column = 1;
                    break;
                case MoveDirection.SouthEast:
                    row = 2;
                    column = 2;
                    break;
            }

            return new GameMove(row, column);
        }
		public static GameMove ToGameMove(this Int32 boardInteger)
		{
			return GameMove.FromInteger(boardInteger);
		}
		public static Int32 ToInteger(this GameMove move)
		{
			return GameMove.ToInteger(move);
		}
    }
}
