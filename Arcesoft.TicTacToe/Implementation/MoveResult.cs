using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.IO;
using System.Xml;

namespace Arcesoft.TicTacToe
{
    [Serializable]
    public struct MoveResult
    {
        Move _moveMade;
        GameState _boardStateAfterMove;

        public Move MoveMade
        {
            get { return _moveMade; }
        }
        public GameState BoardStateAfterMove
        {
            get { return _boardStateAfterMove; }
        }
        
        public MoveResult(Move moveMade, GameState boardStateAfterMove)
        {
            _moveMade = moveMade;
            _boardStateAfterMove = boardStateAfterMove;
        }
        public override string ToString()
        {
            return $"Move Made: ({MoveMade}) Board State: {BoardStateAfterMove}";
        }
    }
}
