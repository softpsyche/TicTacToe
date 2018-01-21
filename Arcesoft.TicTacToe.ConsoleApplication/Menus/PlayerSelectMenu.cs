using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arcesoft.TicTacToe.ConsoleApplication.Menus
{
    public class PlayerSelectMenu
    {
        public const string PlayAsX = "1";
        public const string PlayAsO = "2";

        public static UIMenu Menu => new UIMenu(
            "Please select what side you want to play",
            new[] {
                new UICommand(PlayAsX,"play as 'X' (moves first)"),
                new UICommand(PlayAsO,"play as 'O'"),
                UIMenu.CancelCommand
                }
            );
    }
}
