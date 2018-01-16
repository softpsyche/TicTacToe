using System;
namespace TicTacToe {


	partial class TicTacToeDataSet
	{
		partial class MovesDataTable
		{
		}

        public partial class MovesRow
        {
            public GameState OutcomeEnum
            {
                get
                {
                    if (string.IsNullOrWhiteSpace(this.Outcome))
                    {
                        return default(GameState);
                    }
                    else
                    {
                        return this.Outcome.ToEnumeration<GameState>();
                    }
                }
            }
            public Player PlayerEnum
            {
                get
                {
                    if (string.IsNullOrWhiteSpace(this.Player))
                    {
                        return default(Player);
                    }
                    else
                    {
                        return this.Player.ToEnumeration<Player>();
                    }
                }
            }

            public bool IsWin
            {
                get
                {
                    return (this.PlayerEnum == global::TicTacToe.Player.O && this.OutcomeEnum == GameState.OWin) ||
                        (this.PlayerEnum == global::TicTacToe.Player.X && this.OutcomeEnum == GameState.XWin);
                }
            }
            public Boolean IsTie
            {
                get
                {
                    return this.OutcomeEnum == GameState.Tie;
                }
            }
            public Boolean IsLoss
            {
                get
                {
                    return (this.PlayerEnum == global::TicTacToe.Player.O && this.OutcomeEnum == GameState.XWin) ||
                        (this.PlayerEnum == global::TicTacToe.Player.X && this.OutcomeEnum == GameState.OWin);
                }
            }
        }
	}
}
