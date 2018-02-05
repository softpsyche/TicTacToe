using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Arcesoft.TicTacToe.ArtificialIntelligence;
using Arcesoft.TicTacToe.GameImplementation;
using Arcesoft.TicTacToe.RandomNumberGeneration;
using SimpleInjector;
using Arcesoft.TicTacToe.Data;
using Arcesoft.TicTacToe.Database;
using System.Diagnostics.CodeAnalysis;

namespace Arcesoft.TicTacToe.DependencyInjection
{
    public class Binder : IBinder
    {
        public void BindDependencies(Container container)
        {
            //game
            container.Register<ITicTacToeFactory, TicTacToeFactory>();
            container.Register<IGame, Game>();

            //data
            container.Register<IDatabaseBuilder, DatabaseBuilder>();
            container.Register<IMoveDataAccess, MoveDataAccess>();
            container.Register<IMoveResponseRepository, MoveResponseRepository>();

            //random number generation
            container.Register<IRandom, DefaultRandomNumberGenerator>();

            //database
            container.Register<ILiteDatabaseFactory, LiteDatabaseFactory>();
            container.Register<IMoveEvaluator, MoveEvaluator>();
        }
    }
}
