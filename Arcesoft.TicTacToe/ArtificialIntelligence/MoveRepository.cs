using Arcesoft.TicTacToe.Entities;
using Arcesoft.TicTacToe.GameImplementation;
using Arcesoft.TicTacToe.RandomNumberGeneration;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Arcesoft.TicTacToe.ArtificialIntelligence.TicTacToeDataSet;

namespace Arcesoft.TicTacToe.ArtificialIntelligence
{
    internal class MoveRepository : IMoveRepository
    {
        private IMoveDatabase _moveDatabase = null;

        private MoveRepository(IMoveDatabase moveDatabase)
        {
            _moveDatabase = moveDatabase;
        }

        public IEnumerable<MoveResponse> FindBoardStates(string currentBoardPosition, Player currentPlayer)
        {
            var searchPattern = String.Format("Board = '{0}' AND Player = '{1}'", currentBoardPosition, currentPlayer.ToString());

            var rows = (MovesRow[])_moveDatabase.MovesDataTable.Select(searchPattern);

            return ToBoardState(rows);
        }

        private IEnumerable<MoveResponse> ToBoardState(IEnumerable<MovesRow> movesRows)
        {
            List<MoveResponse> listy = new List<MoveResponse>();

            movesRows.ForEach(a => listy.Add(ToBoardState(a)));

            return listy;
        }
        private MoveResponse ToBoardState(MovesRow movesRow)
        {
            return new MoveResponse()
            {
                Board = movesRow.Board,
                Outcome = movesRow.Outcome.ToEnumeration<GameState>(),
                Player = movesRow.Player.ToEnumeration<Player>(),
                Response = movesRow.Response.ToEnumeration<Move>()
            };
        }

    }
}
