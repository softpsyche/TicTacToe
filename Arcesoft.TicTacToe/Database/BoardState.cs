using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.IO;
using System.Xml;
using Arcesoft.TicTacToe.Entities;

namespace Arcesoft.TicTacToe.Database
{
    [Serializable]
    internal class BoardState
    {
        public string BoardLayout { get; private set; }
        public MoveResult MoveResult { get; private set; }
        public Player Player { get; private set; }

        public BoardState(MoveResult moveResult, string boardLayout,Player player)
        {
            MoveResult = moveResult;
            BoardLayout = boardLayout;
            Player = player;
        }
    }
}
