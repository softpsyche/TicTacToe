using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arcesoft.TicTacToe.ConsoleApplication.Menus
{
    public static class MainMenu
    {
        public const string NewGameAI = "a";
        public const string NewGameHuman = "h";

        public static UIMenu Menu => new UIMenu(
            "Please select one of the following options:",
            new[] {
                new UICommand(NewGameAI,"new game vs AI"),
                new UICommand(NewGameHuman,"new game vs human"),
                UIMenu.CancelCommand
                }
            );
    }
}
