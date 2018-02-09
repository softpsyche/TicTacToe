using Arcesoft.TicTacToe.ArtificialIntelligence;
using Arcesoft.TicTacToe.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arcesoft.TicTacToe
{
    /// <summary>
    /// Represents an artificial intelligence that is capable of making moves for any given tic tac toe game
    /// </summary>
    public interface IArtificialIntelligence
    {
        /// <summary>
        /// Makes a move for the given game randomly (unless specified otherwise)
        /// </summary>
        /// <param name="game"></param>
        /// <param name="randomlySelectIfMoreThanOne"></param>
        /// <exception cref="GameException">If game is over</exception>
        void MakeMove(IGame game, bool randomlySelectIfMoreThanOne = true);

        /// <summary>
        /// Finds all move results for a given game.
        /// </summary>
        /// <param name="game"></param>
        /// <returns></returns>
        IEnumerable<MoveResult> FindMoveResults(IGame game);
    }
}
