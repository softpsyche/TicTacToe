using Arcesoft.TicTacToe.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using static Arcesoft.TicTacToe.Data.TicTacToeDataSet;

namespace Arcesoft.TicTacToe.Database
{
    /// <summary>
    /// Represents an in-memory database of all the possible tic-tac-toe board configurations. 
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

        public void PopulateMoveResponses()
        {

            var boardStates = _moveEvaluator.FindAllMoves(_ticTacToeFactory.NewGame());

            foreach (var boardState in boardStates)
            {
                if (_moveRepository.TryFindMoveResponse(boardState.BoardLayout, boardState.Player, boardState.MoveResult.MoveMade) == null)
                {
                    var moveResponse = new MoveResponse()
                    {
                        Board = boardState.BoardLayout,
                        Outcome = boardState.MoveResult.GameStateAfterMove,
                        Player = boardState.Player,
                        Response = boardState.MoveResult.MoveMade
                    };

                    _moveRepository.InsertMoveResponse(moveResponse);
                }
            }
        }
    }
}
