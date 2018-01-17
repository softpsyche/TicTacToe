using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arcesoft.TicTacToe
{
    public interface IBinder
    {
        void BindDependencies(Container container);
    }
}
