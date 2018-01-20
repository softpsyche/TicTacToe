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
    public class UIMenu
    {
        public const string Quit = "q";

        public string Instructions { get; private set; }

        //public string HelpDescription { get; private set; }

        public IEnumerable<UICommand> Commands { get; private set; }

        public UIMenu(string description, IEnumerable<UICommand> uICommands)
        {
            Instructions = description;
            Commands = uICommands.ToArray();
        }

        public static UICommand CancelCommand = new UICommand(Quit, "Quit/Cancel");
    }
}
