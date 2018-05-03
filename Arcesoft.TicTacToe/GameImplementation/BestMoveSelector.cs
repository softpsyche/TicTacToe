using Arcesoft.TicTacToe.Data;
using Arcesoft.TicTacToe.Entities;
using Arcesoft.TicTacToe.RandomNumberGeneration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arcesoft.TicTacToe.GameImplementation
{
    internal class BestMoveSelector : IBestMoveSelector
    {
        private IRandom _random;

        public BestMoveSelector(IRandom random)
        {
            _random = random;
        }

        public IMoveResult FindRandomBestMoveResultForPlayerOrDefault(IEnumerable<IMoveResult> gameMoveResults, Player player)
        {
            return FindBestMoveResultsForPlayer(gameMoveResults, player)
                .RandomFromListOrDefault(_random);
        }

        public IEnumerable<IMoveResult> FindBestMoveResultsForPlayer(IEnumerable<IMoveResult> gameMoveResults, Player player)
        {
            var bestMoves = gameMoveResults.Where(a => IsWinForPlayer(a, player)).ToList();

            if (bestMoves.Any() == false)
            {
                bestMoves = gameMoveResults.Where(a => IsTie(a)).ToList();
            }
            if (bestMoves.Any() == false)
            {
                bestMoves = gameMoveResults.Where(a => IsLossForPlayer(a, player)).ToList();
            }

            return bestMoves;
        }

        private bool IsWinForPlayer(IMoveResult moveResult, Player player)
        {
            return
                (player == Player.O && moveResult.GameStateAfterMove == GameState.OWin) ||
                (player == Player.X && moveResult.GameStateAfterMove == GameState.XWin);
        }
        private bool IsLossForPlayer(IMoveResult moveResult, Player player)
        {
            return
                (player == Player.O && moveResult.GameStateAfterMove == GameState.XWin) ||
                (player == Player.X && moveResult.GameStateAfterMove == GameState.OWin);
        }

        private bool IsTie(IMoveResult moveResult) => moveResult.GameStateAfterMove == GameState.Tie;
    }
}
