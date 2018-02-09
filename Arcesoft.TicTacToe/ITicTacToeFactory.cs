using Arcesoft.TicTacToe.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arcesoft.TicTacToe
{
    /// <summary>
    /// Allows for construction of tic tac toe games, AIs and the database builder
    /// </summary>
    public interface ITicTacToeFactory
    {
        /// <summary>
        /// Creates a new instance of a tic tac toe game.
        /// </summary>
        /// <returns></returns>
        IGame NewGame();

        /// <summary>
        /// Creates a new instance of a tic tac toe game. with the supplied moves applied. The moves are made in the
        /// order in which they are read from the set. 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="GameException">Throws an exception if the list of moves are invalid</exception>
        IGame NewGame(IEnumerable<Move> moves);

        /// <summary>
        /// Instantiates a new database builder object
        /// </summary>
        /// <returns></returns>
        IDatabaseBuilder NewDatabaseBuilder();

        /// <summary>
        /// Instantiates a new game AI object
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        IArtificialIntelligence NewArtificialIntelligence(string type);
    }
}
