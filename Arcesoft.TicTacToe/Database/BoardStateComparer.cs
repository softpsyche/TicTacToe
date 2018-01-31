using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arcesoft.TicTacToe.Database
{
    internal class BoardStateComparer : IEqualityComparer<BoardState>
    {
        public bool Equals(BoardState x, BoardState y)
        {
            return x.BoardLayout == y.BoardLayout &&
                x.MoveResult.MoveMade == y.MoveResult.MoveMade &&
                x.Player == y.Player;
        }

        public int GetHashCode(BoardState obj)
        {
            return obj.BoardLayout.GetHashCode() ^
                obj.MoveResult.MoveMade.GetHashCode() ^
                obj.Player.GetHashCode();
        }
    }
}
