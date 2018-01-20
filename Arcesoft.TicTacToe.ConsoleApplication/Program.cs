using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arcesoft.TicTacToe.ConsoleApplication
{
    class Program
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
            container.GetInstance<UI>().Run();
        }
    }
}
