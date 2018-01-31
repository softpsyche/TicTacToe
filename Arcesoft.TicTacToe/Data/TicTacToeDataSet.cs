using Arcesoft.TicTacToe.Entities;
using System.Diagnostics.CodeAnalysis;

namespace Arcesoft.TicTacToe.Data
{
    [ExcludeFromCodeCoverage]
    partial class TicTacToeDataSet
    {
        [ExcludeFromCodeCoverage]
        partial class MovesDataTable : IMovesDataTable
        {
            public new MovesRow[] Select(string filterExpression)
            {
                return (MovesRow[])base.Select(filterExpression);
            }
        }
    }
}
