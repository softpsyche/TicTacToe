using Arcesoft.TicTacToe.Entities;

namespace Arcesoft.TicTacToe.Data
{
    partial class TicTacToeDataSet
    {
        partial class MovesDataTable : IMovesDataTable
        {
            public new MovesRow[] Select(string filterExpression)
            {
                return (MovesRow[])base.Select(filterExpression);
            }
        }
    }
}
