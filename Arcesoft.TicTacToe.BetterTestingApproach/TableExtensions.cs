using Arcesoft.TicTacToe.Data;
using Arcesoft.TicTacToe.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace Arcesoft.TicTacToe.BetterTestingApproach
{
    internal static class TableExtensions
    {
        public static TicTacToeDataSet.MovesDataTable ToMovesDataTable(this Table table)
        {
            TicTacToeDataSet.MovesDataTable movesDataTable = new TicTacToeDataSet.MovesDataTable();

            table.Rows.ForEach(a => movesDataTable.AddMovesRow(
                a[nameof(TicTacToeDataSet.MovesRow.Board)],
                a[nameof(TicTacToeDataSet.MovesRow.Player)],
                (int)(a[nameof(TicTacToeDataSet.MovesRow.Response)]).ToEnumeration<Move>(),
                a[nameof(TicTacToeDataSet.MovesRow.Outcome)]));

            return movesDataTable;
        }
    }
}
