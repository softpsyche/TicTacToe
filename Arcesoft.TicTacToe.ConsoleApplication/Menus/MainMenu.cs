using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arcesoft.TicTacToe.ConsoleApplication.Menus
{
    public static class MainMenu
    {
        public const string NewGameAI = "1";
        public const string NewGameHuman = "2";
        public const string PopulateGameDatabase = "3";

        public static UIMenu Menu => new UIMenu(
            "Please select one of the following options:",
            new[] {
                new UICommand(NewGameAI,"new game vs AI"),
                new UICommand(NewGameHuman,"new game vs human"),
                new UICommand(PopulateGameDatabase, "populate game database"),
                UIMenu.CancelCommand
                }
            );
    }
}
