﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Arcesoft.TicTacToe.ArtificialIntelligence;
using Arcesoft.TicTacToe.GameImplementation;
using Arcesoft.TicTacToe.RandomNumberGeneration;
using SimpleInjector;

namespace Arcesoft.TicTacToe.DependencyInjection
{
    public class Binder : IBinder
    {
        public void BindDependencies(Container container)
        {
            try
            {
                //game
                container.Register<ITicTacToeFactory, TicTacToeFactory>();
                container.Register<IGame, Game>();

                //AI
                container.Register<IMoveDatabase, MoveDatabase>();
                container.Register<IMoveDataAccess, MoveDataAccess>();
                container.Register<IMoveRepository, MoveRepository>();
                container.Register<IMoveEvaluator, MoveEvaluator>();

                //random number generation
                container.Register<IRandom, DefaultRandomNumberGenerator>();
            }
            catch (Exception ex)
            {
                var yo = ex;
            }
        }
    }
}
