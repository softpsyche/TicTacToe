using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arcesoft.TicTacToe.ConsoleApplication
{
    /// <summary>
    /// This class is threadsafe
    /// </summary>
    public class UICommand
    {
        public string CommandCode { get; private set; }
        public string CommandDescription { get; private set; }

        public UICommand(string commandCode, string commandDescription)
        {
            CommandCode = commandCode;
            CommandDescription = commandDescription;
        }
    }
}
