using Arcesoft.TicTacToe.Entities;

namespace Arcesoft.TicTacToe.ArtificialIntelligence
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
