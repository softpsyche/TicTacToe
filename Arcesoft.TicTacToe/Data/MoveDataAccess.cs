using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.IO;
using System.Xml;
using System.Linq;
using Arcesoft.TicTacToe.RandomNumberGeneration;
using Arcesoft.TicTacToe.Entities;
using Arcesoft.TicTacToe.Data;

namespace Arcesoft.TicTacToe.ArtificialIntelligence
{
    [Serializable]
    internal class MoveDataAccess : IMoveDataAccess
    {
        private readonly IMoveRepository _moveRepository;
        private readonly IRandom _random;

        public MoveDataAccess(IMoveRepository moveRepository, IRandom random)
        {
            _moveRepository = moveRepository;
            _random = random;
        }

        public Move FindBestMove(string currentBoardPosition, Player currentPlayer, Boolean random = true)
        {
            var moves = _moveRepository.FindBoardStates(currentBoardPosition, currentPlayer);
            var winningMoves = moves.Where(a => a.IsWin).ToList();
            var tieMoves = moves.Where(a => a.IsTie).ToList();
            var losingMoves = moves.Where(a => a.IsLoss).ToList();
            MoveResponse bestMove;

            if (winningMoves.Any())
            {
                bestMove = random ? winningMoves[_random.Next(winningMoves.Count)] : winningMoves.First();
            }
            else if (tieMoves.Any())
            {
                bestMove = random ? tieMoves[_random.Next(tieMoves.Count)] : tieMoves.First();
            }
            else if (losingMoves.Any())
            {
                bestMove = random ? losingMoves[_random.Next(losingMoves.Count)] : losingMoves.First();
            }
            else
            {
                throw new GameException("There is no response for that board position and player turn.");
            }

            return bestMove.Response;
        }

        public IEnumerable<MoveResponse> FindMoveResponses(string currentBoardPosition, Player currentPlayer)
        {
            return _moveRepository.FindBoardStates(currentBoardPosition, currentPlayer);
        }
    }
}
