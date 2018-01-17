using System;
using System.Collections.Generic;
using System.Text;

namespace Arcesoft.TicTacToe
{
    public class GameException : Exception
    {
        public GameException(String message)
            : base(message)
        {

        }
    }
}
