using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.IO;
using System.Xml;
using System.Linq;
using Arcesoft.TicTacToe.ArtificialIntelligence;
using Arcesoft.TicTacToe.RandomNumberGeneration;
using Arcesoft.TicTacToe.Entities;
using Arcesoft.TicTacToe.GameImplementation;

namespace Arcesoft.TicTacToe.ArtificialIntelligence
{
    [Serializable]
    internal class MoveRepository : IMoveRepository
    {
        private TicTacToeDataSet.MovesDataTable MovesDataTable
        {
            get;
            set;
        }

		public MoveRepository(TicTacToeDataSet.MovesDataTable movesDataTable)
        {
            MovesDataTable = movesDataTable;
        }

        public Move FindBestMove(string currentBoardPosition, Player currentPlayer, Boolean random = true)
        {
            TicTacToeDataSet.MovesRow bestMove;
            Random randy = random ? new Random() : null;

            var moves = LookupMoves(currentBoardPosition, currentPlayer);
            var winningMoves = moves.Where(a => a.IsWin).ToList();
            var tieMoves = moves.Where(a => a.IsTie).ToList();
            var losingMoves = moves.Where(a => a.IsLoss).ToList();

            if (winningMoves.Any())
            {
                bestMove = random ? winningMoves[randy.Next(winningMoves.Count)] : winningMoves.First();
            }
            else if (tieMoves.Any())
            {
                bestMove = random ? tieMoves[randy.Next(tieMoves.Count)] : tieMoves.First();
            }
            else if (losingMoves.Any())
            {
                bestMove = random ? losingMoves[randy.Next(losingMoves.Count)] : losingMoves.First();
            }
            else
            {
                throw new GameException("There is no response for that board position and player turn.");
            }

            return (Move)bestMove.Response;
        }

		private TicTacToeDataSet.MovesRow[] LookupMoves(string currentBoardPosition, Player currentPlayer)
        {
            var searchPattern = String.Format("Board = '{0}' AND Player = '{1}'", currentBoardPosition, currentPlayer.ToString());

            return (TicTacToeDataSet.MovesRow[])MovesDataTable.Select(searchPattern);
        }
    }

   
}
