using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arcesoft.TicTacToe.Entities
{
    /// <summary>
    /// Represents a move result
    /// </summary>
    internal interface IMoveResult
    {
        Move MoveMade { get; }
        GameState GameStateAfterMove { get; }
    }
}
