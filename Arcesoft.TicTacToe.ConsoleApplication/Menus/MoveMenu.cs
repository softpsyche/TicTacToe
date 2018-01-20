using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arcesoft.TicTacToe.ConsoleApplication.Menus
{
    public static class MoveMenu
    {
        public const string SouthWest = "1";
        public const string Southern = "2";
        public const string SouthEast = "3";
        public const string Western = "4";
        public const string Center = "5";
        public const string Eastern = "6";
        public const string NorthWest = "7";
        public const string Northern = "8";
        public const string NorthEast = "9";

        public static UIMenu Menu => new UIMenu(
            "Please select where it is you want to move:",
            new[] {
                        new UICommand(SouthWest,"bottom left square"),
                        new UICommand(Southern,"bottom center square"),
                        new UICommand(SouthEast,"bottom right square"),
                        new UICommand(Western,"center left square"),
                        new UICommand(Center,"center square"),
                        new UICommand(Eastern,"center right square"),
                        new UICommand(NorthWest,"top left square"),
                        new UICommand(Northern,"top center square"),
                        new UICommand(NorthEast,"top right square"),
                }
            );
    }
}
