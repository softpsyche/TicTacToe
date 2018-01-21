using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arcesoft.TicTacToe.ConsoleApplication.Menus
{
    public class AIMenu
    {
        public const string OmniscientGod = "1";
        public const string HomerSimpson = "2";
        
        public static UIMenu Menu => new UIMenu(
            "Which AI would you like to play against?",
            new[] {
                new UICommand(OmniscientGod,"play against an omnisicient god"),
                new UICommand(HomerSimpson,"play against an intoxicated Homer Simpson"),
                UIMenu.CancelCommand
                }
            );
    }
}
