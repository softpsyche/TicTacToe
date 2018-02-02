using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arcesoft.TicTacToe.RandomNumberGeneration
{
    internal class DefaultRandomNumberGenerator : IRandom
    {
        Random random;

        public DefaultRandomNumberGenerator()
        {
            random = new Random();
        }

        public Int32 Next(Int32 maxValue)
        {
            return random.Next(maxValue);
        }
    }
}
