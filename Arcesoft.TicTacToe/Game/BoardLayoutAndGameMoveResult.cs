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
    public class BoardLayoutAndGameMoveResult
    {
        string boardLayout = null;
        GameMoveResult gameMoveResult;
        Player player;

        public string BoardLayout
        {
            get { return boardLayout; }
        }
        internal GameMoveResult GameMoveResult
        {
            get { return gameMoveResult; }
        }
        public Player Player
        {
            get { return player; }
        }

        public BoardLayoutAndGameMoveResult(GameMoveResult gameMoveResult, string boardLayout,Player player)
        {
            this.boardLayout = boardLayout;
            this.gameMoveResult = gameMoveResult;
            this.player = player;
        }
    }
}
