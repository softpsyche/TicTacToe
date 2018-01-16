namespace Arcesoft.TicTacToe.Implementation
{
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
                    if (string.IsNullOrWhiteSpace(Outcome))
                    {
                        return default(GameState);
                    }
                    else
                    {
                        return Outcome.ToEnumeration<GameState>();
                    }
                }
            }
            public Player PlayerEnum
            {
                get
                {
                    if (string.IsNullOrWhiteSpace(Player))
                    {
                        return default(Player);
                    }
                    else
                    {
                        return Player.ToEnumeration<Player>();
                    }
                }
            }

            public bool IsWin
            {
                get
                {
                    return (PlayerEnum == TicTacToe.Player.O && OutcomeEnum == GameState.OWin) ||
                        (PlayerEnum == TicTacToe.Player.X && OutcomeEnum == GameState.XWin);
                }
            }
            public bool IsTie
            {
                get
                {
                    return OutcomeEnum == GameState.Tie;
                }
            }
            public bool IsLoss
            {
                get
                {
                    return (PlayerEnum == TicTacToe.Player.O && OutcomeEnum == GameState.XWin) ||
                        (PlayerEnum == TicTacToe.Player.X && OutcomeEnum == GameState.OWin);
                }
            }
        }
    }
}
