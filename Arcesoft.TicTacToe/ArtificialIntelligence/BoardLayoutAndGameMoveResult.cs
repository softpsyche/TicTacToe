using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.IO;
using System.Xml;
using Arcesoft.TicTacToe.Entities;

namespace Arcesoft.TicTacToe.ArtificialIntelligence
{
    [Serializable]
    internal class BoardLayoutAndGameMoveResult
    {
        string _boardLayout = null;
        MoveResult _gameMoveResult;
        Player _player;

        public string BoardLayout
        {
            get { return _boardLayout; }
        }
        internal MoveResult GameMoveResult
        {
            get { return _gameMoveResult; }
        }
        public Player Player
        {
            get { return _player; }
        }

        public BoardLayoutAndGameMoveResult(MoveResult gameMoveResult, string boardLayout,Player player)
        {
            _boardLayout = boardLayout;
            _gameMoveResult = gameMoveResult;
            _player = player;
        }
    }
}
