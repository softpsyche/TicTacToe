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
    internal class MoveResult
    {
        public Move MoveMade { get; private set; }
        public GameState BoardStateAfterMove { get; private set; }
        
        public MoveResult(Move moveMade, GameState boardStateAfterMove)
        {
            MoveMade = moveMade;
            BoardStateAfterMove = boardStateAfterMove;
        }
        public override string ToString()
        {
            return $"Move Made: ({MoveMade}) Board State: {BoardStateAfterMove}";
        }
    }
}
