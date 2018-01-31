using Arcesoft.TicTacToe.Data;
using Arcesoft.TicTacToe.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arcesoft.TicTacToe.Database
{
    internal class MoveResponseRecord
    {
        public string Id => $@"{Board}{Player.ToString()}{Response.ToString()}";
        public string Board { get; set; }
        public Player Player { get; set; }
        public Move Response { get; set; }
        public GameState Outcome { get; set; }
    }
}
