using Arcesoft.TicTacToe.Database;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arcesoft.TicTacToe.ConsoleApplication
{
    internal class Program
    {
        static readonly Container container;

        static Program()
        {
            container = new Container();

            new DependencyInjection.Binder().BindDependencies(container);
            new Binder().BindDependencies(container);

            container.Verify();
        }

        static void Main()
        {
#if (DEBUG)
            DoIt();
#endif
            container.GetInstance<UI>().Run();
        }

        private static void DoIt()
        {
            //try
            //{
            //    var liteDbFactory = container.GetInstance<Database.ILiteDatabaseFactory>();
            //    var record = new Database.MoveResponseRecord()
            //    {
            //        Board = "____X____",
            //        Player = Entities.Player.X,
            //        Outcome = Entities.GameState.XWin,
            //        Response = Entities.Move.NorthWest
            //    };

            //    using (var db = liteDbFactory.OpenOrCreate("testing.db"))
            //    {
            //        db.Insert(record);
            //    }

            //    using (var db = liteDbFactory.OpenOrCreate("testing.db"))
            //    {
            //        var readRecord = db.FindById<MoveResponseRecord,string>(record.Id);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Console.Write(ex);

            //    var yo = 34;
            //}
        }
    }
}
