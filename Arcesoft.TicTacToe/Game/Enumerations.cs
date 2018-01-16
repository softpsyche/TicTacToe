using System;
using System.Collections.Generic;
using System.Text;

namespace TicTacToe
{
	public enum Square
	{
		Empty = 0,
		X =1,
		O =2
	}
	public enum GameState
	{
		InPlay,
		Tie,
		XWin,
		OWin
	}
	public enum Player
	{
		X,
		O
	}
    public enum MoveDirection
    {
        NorthEast = 0,
        Northern = 1,
        NorthWest = 2,
        Western = 3,
        Center = 4,
        Eastern = 5,
        SouthWest = 6,
        Southern = 7,
        SouthEast = 8
    }

    public enum GameChange
    {
        Move,
        Over,
        Reset,      
        UndoMove
    }
}
