using Arcesoft.TicTacToe.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arcesoft.TicTacToe.ArtificialIntelligence
{
    internal class MoveResponse
    {
        public string Board { get; set; }
        public Player Player { get; set; }
        public Move Response { get; set; }
        public GameState Outcome { get; set; }

        public bool IsWin => 
            (Player == Player.O && Outcome == GameState.OWin) ||
            (Player == Player.X && Outcome == GameState.XWin);

        public bool IsTie => Outcome == GameState.Tie;

        public bool IsLoss => 
            (Player == Player.O && Outcome == GameState.XWin) ||
            (Player == Player.X && Outcome == GameState.OWin);
    }
}
