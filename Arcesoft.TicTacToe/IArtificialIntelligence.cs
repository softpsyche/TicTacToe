using Arcesoft.TicTacToe.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arcesoft.TicTacToe
{
    public interface IArtificialIntelligence
    {
        //string CommentOnGameState(IGame game);

        void MakeMove(IGame game, ArtificialIntelligenceBehavior behavior = ArtificialIntelligenceBehavior.PlayToWin, bool randomlySelectIfMoreThanOne = true);
    }
}
