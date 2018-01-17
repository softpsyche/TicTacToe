using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arcesoft.TicTacToe.Entities
{
    [Serializable()]
    public class GameStateChangedEventArgs : EventArgs
    {
        public GameChange GameChange { get; set; }
        public GameState GameState { get; set; }
        public Player CurrentPlayer { get; set; }


        public GameStateChangedEventArgs(GameState gameState, GameChange change, Player playerTurn)
        {
            GameChange = change;
            GameState = gameState;
            CurrentPlayer = playerTurn;

        }
    }
}
