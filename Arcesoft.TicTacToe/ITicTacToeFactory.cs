using Arcesoft.TicTacToe.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arcesoft.TicTacToe
{
    public interface ITicTacToeFactory
    {
        IGame NewGame();

        IGame NewGame(IEnumerable<Move> moves);

        IDatabaseBuilder NewDatabaseBuilder();

        IArtificialIntelligence NewArtificialIntelligence(string type);
    }
}
