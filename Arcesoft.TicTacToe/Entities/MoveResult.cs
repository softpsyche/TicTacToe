using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.IO;
using System.Xml;
using Arcesoft.TicTacToe.Entities;

namespace Arcesoft.TicTacToe.Entities
{
    [Serializable]
    public class MoveResult: IMoveResult
    {
        public Move MoveMade { get; private set; }
        public GameState GameStateAfterMove { get; private set; }
        
        public MoveResult(Move moveMade, GameState gameStateAfterMove)
        {
            MoveMade = moveMade;
            GameStateAfterMove = gameStateAfterMove;
        }
        public override string ToString()
        {
            return $"Move Made: ({MoveMade}) Game State: {GameStateAfterMove}";
        }
    }
}
