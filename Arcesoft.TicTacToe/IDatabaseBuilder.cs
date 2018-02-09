using Arcesoft.TicTacToe.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arcesoft.TicTacToe
{
    /// <summary>
    /// Populates the database with game moves
    /// </summary>
    public interface IDatabaseBuilder
    {
        /// <summary>
        /// Returns true if the database is empty, false.
        /// </summary>
        /// <returns></returns>
        bool DatabaseIsEmpty();

        /// <summary>
        /// Populate the database with all moves generated from the game state that is passed in. The existing database is cleared first. 
        /// </summary>
        /// <param name="game"></param>
        void PopulateMoveResponses(IGame game = null);
    }
}
