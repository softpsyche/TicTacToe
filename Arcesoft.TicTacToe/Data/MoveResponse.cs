using Arcesoft.TicTacToe.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arcesoft.TicTacToe.Data
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

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            var boardCopy = Board;

            boardCopy.Remove((int)Response, 1);
            boardCopy.Insert((int)Response, "R");

            sb.AppendLine(boardCopy.Substring(0, 3));
            sb.AppendLine(boardCopy.Substring(0, 3));
            sb.AppendLine(boardCopy.Substring(0, 3));
            sb.AppendLine($"(Outcome: '{Outcome}' )");

            return sb.ToString();
        }
    }
}
