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
    public struct GameMoveResult
    {
        GameMove moveMade;
        GameState boardStateAfterMove;

        public GameMove MoveMade
        {
            get { return moveMade; }
        }
        public GameState BoardStateAfterMove
        {
            get { return boardStateAfterMove; }
        }
        
        public GameMoveResult(GameMove moveMade, GameState boardStateAfterMove)
        {
            this.moveMade = moveMade;
            this.boardStateAfterMove = boardStateAfterMove;
        }
        public override string ToString()
        {
            return "Move Made: (" + moveMade.Row.ToString() + "," + moveMade.Column.ToString() +
                ") Board State: " + boardStateAfterMove.ToString();
        }
    }
}
