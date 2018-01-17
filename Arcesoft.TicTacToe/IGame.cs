using Arcesoft.TicTacToe.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arcesoft.TicTacToe
{
    public interface IGame
    {
        event EventHandler<EventArgs> GameOver;
        event EventHandler GameReset;
        event EventHandler<GameStateChangedEventArgs> GameStateChanged;

        string GameBoardString { get; }
        Move[] MoveHistory { get; }
        GameState GameState { get; }
        Player CurrentPlayer { get; }
        int TotalMovesMade { get; }
        bool GameIsOver { get; }
        bool IsMoveValid(Move position);       
        IEnumerable<Move> GetLegalMoves();
        void Move(Move position);
        void Reset();
        void UndoLastMove();
    }
}
