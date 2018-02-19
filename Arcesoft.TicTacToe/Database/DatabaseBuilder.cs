using Arcesoft.TicTacToe.Data;
using Arcesoft.TicTacToe.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Arcesoft.TicTacToe.Database
{
    /// <summary>
    /// Populates a database
    /// </summary>
    [ExcludeFromCodeCoverage()]//We will exclude this from code coverage for this example because we assume that the 'Database' is already actually built. 
    internal class DatabaseBuilder : IDatabaseBuilder
    {
        private ITicTacToeFactory _ticTacToeFactory;
        private IMoveEvaluator _moveEvaluator;
        private IMoveResponseRepository _moveRepository;

        public DatabaseBuilder(ITicTacToeFactory ticTacToeFactory, IMoveEvaluator moveEvaluator, IMoveResponseRepository moveRepository)
        {
            _ticTacToeFactory = ticTacToeFactory;
            _moveEvaluator = moveEvaluator;
            _moveRepository = moveRepository;
        }

        public bool DatabaseIsEmpty()
        {
            //kind of a hack, but should work...
            return _moveRepository.FindMoveResponses("_________", Player.X).Any() == false;
        }

        public void PopulateMoveResponses(IGame game = null)
        {
            var moveResponses = _moveEvaluator.FindAllMoves(game ?? _ticTacToeFactory.NewGame())
                .Select(a => new MoveResponse()
                {
                    Board = a.BoardLayout,
                    Outcome = a.MoveResult.GameStateAfterMove,
                    Player = a.Player,
                    Response = a.MoveResult.MoveMade
                })
                .ToList();

            _moveRepository.DeleteAllMoveResponses();

            _moveRepository.InsertMoveResponses(moveResponses);
        }
    }
}
