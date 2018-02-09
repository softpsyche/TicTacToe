using Arcesoft.TicTacToe.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arcesoft.TicTacToe
{
    /// <summary>
    /// Represents a tic tac toe game and its state. 
    /// </summary>
    public interface IGame
    {
        event EventHandler GameOver;
        event EventHandler GameReset;
        event EventHandler<GameStateChangedEventArgs> GameStateChanged;

        /// <summary>
        /// Gets a string representation of the game with 'X', 'O' and '_' as empty space.
        /// The board is represented from the top left being the 0 index of the string
        /// and the bottom right being the last or 8th index of the string, i.e.
        /// 
        ///             012
        ///             345
        ///             678
        /// </summary>
        string GameBoardString { get; }
        /// <summary>
        /// A history of all the moves <see cref="Entities.Move"/> that have been made for this game, in the order
        /// in which they were made.
        /// </summary>
        Move[] MoveHistory { get; }
        /// <summary>
        /// The game state of this game (in play, tie, xwin or owin)
        /// </summary>
        GameState GameState { get; }
        /// <summary>
        /// The player whose turn it currently is. Can be X or O
        /// </summary>
        Player CurrentPlayer { get; }
        /// <summary>
        /// The total number of moves that have been made in this game. Will range from 0-9
        /// </summary>
        int TotalMovesMade { get; }
        /// <summary>
        /// Indicates whether the game is over or not. Specifically, a game is over if the game state is not 'InPlay'
        /// </summary>
        bool GameIsOver { get; }
        /// <summary>
        /// Determines whether the given move is a valid next move for this game
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        bool IsMoveValid(Move position);       
        /// <summary>
        /// Gets a list of all valid moves for a given game. If the game is not over, this should be all empty squares on the board.
        /// </summary>
        /// <returns></returns>
        IEnumerable<Move> GetLegalMoves();
        /// <summary>
        /// Makes a move for the current player.
        /// </summary>
        /// <param name="position"></param>
        /// <exception cref="GameException">Throws a game exception if the game is over or the move is invalid (occupied square).</exception>
        /// <remarks>Will raise a GameStateChangedEvent. Will also raise a GameOver event if the move results in the game being over</remarks>
        void Move(Move position);
        /// <summary>
        /// Resets the game to a 'new' state.
        /// </summary>
        /// <remarks>Will raise a GameReset event. Will also raise a GameStateChangedEvent</remarks>
        void Reset();
        /// <summary>
        /// Will undo the last move made. 
        /// </summary>
        /// <exception cref="GameException">If the game has no moves made, an exception will be thrown</exception>
        /// <remarks>Will raise a GameStateChangedEvent</remarks>
        void UndoLastMove();
    }
}
