using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Arcesoft.TicTacToe.GameImplementation;
using SimpleInjector;

namespace Arcesoft.TicTacToe.DependencyInjection
{
    public class Binder : IBinder
    {
        public void BindDependencies(Container container)
        {
            container.Register<IGameFactory, GameFactory>();
        }
    }
}
