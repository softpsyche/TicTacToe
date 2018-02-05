using Arcesoft.TicTacToe.Entities;
using Arcesoft.TicTacToe.GameImplementation;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arcesoft.TicTacToe.CommonTestingApproach.GameImplementation
{
    [TestClass]
    [TestCategory("CommonTestingApproach")]
    public class GameTests
    {
        private Game Game { get; set; }
        private List<GameStateChangedEventArgs> GameStateChangedEventsReceivedList { get; set; }
        private List<EventArgs> GameResetEventsReceivedList { get; set; }
        private List<EventArgs> GameOverEventsReceivedList { get; set; }

        [TestInitialize]
        public void Initialize()
        {
            Game = new Game();

            Game.GameOver += Game_GameOver;
            Game.GameReset += Game_GameReset;
            Game.GameStateChanged += Game_GameStateChanged;

            GameStateChangedEventsReceivedList = new List<GameStateChangedEventArgs>();
            GameResetEventsReceivedList = new List<EventArgs>();
            GameOverEventsReceivedList = new List<EventArgs>();
        }

        [TestCleanup]
        public void Cleanup()
        {
            Game.GameOver -= Game_GameOver;
            Game.GameReset -= Game_GameReset;
            Game.GameStateChanged -= Game_GameStateChanged;
        }

        private void Game_GameStateChanged(object sender, GameStateChangedEventArgs e)
        {
            GameStateChangedEventsReceivedList.Add(e);
        }
        private void Game_GameReset(object sender, EventArgs e)
        {
            GameResetEventsReceivedList.Add(e);
        }
        private void Game_GameOver(object sender, EventArgs e)
        {
            GameOverEventsReceivedList.Add(e);
        }
        private void ClearAllEventsReceived()
        {
            GameOverEventsReceivedList.Clear();
            GameResetEventsReceivedList.Clear();
            GameStateChangedEventsReceivedList.Clear();
        }

        [TestMethod]
        public void ShouldStartInCorrectState()
        {
            //arrange

            //act

            //assert
            Game.GameBoardString.Should().Be(Board.BoardEmptyString);
            Game.CurrentPlayer.Should().Be(Player.X);
            Game.TotalMovesMade.Should().Be(0);
            Game.MoveHistory.Should().BeEmpty();
            Game.GameState.Should().Be(GameState.InPlay);
            Game.GameIsOver.Should().BeFalse();
        }

        [TestMethod]
        public void MoveShouldThrowExceptionIfGameIsOver()
        {
            //arrange
            Game.Move(Move.Center);
            Game.Move(Move.Eastern);
            Game.Move(Move.Northern);
            Game.Move(Move.Western);
            Game.Move(Move.Southern);

            ClearAllEventsReceived();

            var moveToMake = Move.SouthWest;

            //act
            Action act = () => Game.Move(moveToMake);

            //assert
            act
                .ShouldThrow<GameException>()
                .WithMessage("Invalid move. The game is no longer in play.");
        }

        [TestMethod]
        public void MoveShouldThrowIfGameIfGameSquareIsAlreadyOccupied()
        {
            //arrange
            Game.Move(Move.Center);

            ClearAllEventsReceived();

            var moveToMake = Move.Center;

            //act
            Action act = () => Game.Move(moveToMake);

            //assert
            act
                .ShouldThrow<GameException>()
                .WithMessage("Invalid move. Square already occupied.");
        }

        [TestMethod]
        public void MoveForInPlayShouldWork()
        {
            //arrange
            var moveToMake = Move.Center;

            //act
            Game.Move(moveToMake);

            //assert
            Game.TotalMovesMade.Should().Be(1);
            Game.GameState.Should().Be(GameState.InPlay);
            Game.MoveHistory.Should().BeEquivalentTo(new[] { moveToMake });
            Game.GameIsOver.Should().BeFalse();
            Game.CurrentPlayer.Should().Be(Player.O);
            Game.GameBoardString.Should().Be("____X____");

            GameStateChangedEventsReceivedList.ShouldAllBeEquivalentTo(new[] {
                new GameStateChangedEventArgs(GameState.InPlay, GameChange.Move, Player.O)
                });
        }

        [TestMethod]
        public void MoveForWinShouldWork()
        {
            //arrange
            Game.Move(Move.Center);
            Game.Move(Move.Eastern);
            Game.Move(Move.Northern);
            Game.Move(Move.Western);

            ClearAllEventsReceived();

            var moveToMake = Move.Southern;

            //act
            Game.Move(moveToMake);

            //assert
            Game.TotalMovesMade.Should().Be(5);
            Game.GameState.Should().Be(GameState.XWin);
            Game.MoveHistory.Should().BeEquivalentTo(new[] { Move.Center, Move.Eastern, Move.Northern, Move.Western, Move.Southern });
            Game.GameIsOver.Should().BeTrue();
            Game.GameBoardString.Should().Be("_X_OXO_X_");

            GameStateChangedEventsReceivedList.ShouldAllBeEquivalentTo(new[] {
                new GameStateChangedEventArgs(GameState.XWin, GameChange.Over, Player.O)
                });

            GameOverEventsReceivedList.Count.Should().Be(1);
        }

        [TestMethod]
        public void MoveForTieShouldWork()
        {
            //arrange
            Game.Move(Move.Center);
            Game.Move(Move.SouthWest);
            Game.Move(Move.Western);
            Game.Move(Move.Eastern);
            Game.Move(Move.NorthWest);
            Game.Move(Move.SouthEast);
            Game.Move(Move.NorthEast);
            Game.Move(Move.Northern);


            ClearAllEventsReceived();

            var moveToMake = Move.Southern;

            //act
            Game.Move(moveToMake);

            //assert
            Game.TotalMovesMade.Should().Be(9);
            Game.GameState.Should().Be(GameState.Tie);
            Game.MoveHistory.Should().BeEquivalentTo(new[] {
                Move.Center,
                Move.SouthWest,
                Move.Western,
                Move.Eastern,
                Move.NorthWest,
                Move.SouthEast,
                Move.NorthEast,
                Move.Northern,
                Move.Southern
            });
            Game.GameIsOver.Should().BeTrue();
            Game.GameBoardString.Should().Be("XOXXXOOXO");

            GameStateChangedEventsReceivedList.ShouldAllBeEquivalentTo(new[] {
                new GameStateChangedEventArgs(GameState.Tie, GameChange.Over, Player.O)
                });

            GameOverEventsReceivedList.Count.Should().Be(1);
        }

        [TestMethod]
        public void IsMoveValidShouldReturnTrueForValidMove()
        {
            //arrange
            var moveToMake = Move.Center;

            //act
            var result = Game.IsMoveValid(moveToMake);

            //assert
            result.Should().BeTrue();
        }

        [TestMethod]
        public void IsMoveValidShouldReturnFalseWhenGameIsOver()
        {
            //arrange
            Game.Move(Move.Center);
            Game.Move(Move.Eastern);
            Game.Move(Move.Northern);
            Game.Move(Move.Western);
            Game.Move(Move.Southern);

            var moveToMake = Move.SouthWest;

            //act
            var result = Game.IsMoveValid(moveToMake);

            //assert
            result.Should().BeFalse();
        }

        [TestMethod]
        public void IsMoveValidShouldReturnFalseWhenSquareIsAlreadyOccupied()
        {
            //arrange
            Game.Move(Move.Center);

            var moveToMake = Move.Center;

            //act
            var result = Game.IsMoveValid(moveToMake);

            //assert
            result.Should().BeFalse();
        }

        [TestMethod]
        public void UndoLastMoveShouldThrowExceptionIfNoMovesHaveBeenMadeYet()
        {
            //arrange

            //act
            Action act = () => Game.UndoLastMove();

            //assert
            act
                .ShouldThrow<GameException>()
                .WithMessage("No moves have been made yet.");
        }

        [TestMethod]
        public void UndoLastMoveShouldUndoLastMove()
        {
            //arrange
            Game.Move(Move.Center);
            Game.Move(Move.SouthWest);
            Game.Move(Move.Northern);
            Game.Move(Move.Western);
            Game.Move(Move.Southern);

            ClearAllEventsReceived();

            //act
            Game.UndoLastMove();

            //assert
            Game.GameBoardString.Should().Be("_X_OX_O__");
            Game.CurrentPlayer.Should().Be(Player.X);
            Game.TotalMovesMade.Should().Be(4);
            Game.MoveHistory.ShouldAllBeEquivalentTo(new[] { Move.Center, Move.SouthWest, Move.Northern, Move.Western });
            Game.GameState.Should().Be(GameState.InPlay);
            Game.GameIsOver.Should().BeFalse();

            GameStateChangedEventsReceivedList.ShouldAllBeEquivalentTo(new[] {
                new GameStateChangedEventArgs(GameState.InPlay, GameChange.UndoMove, Player.X)
                });
        }

        [TestMethod]
        public void GetLegalMovesShouldReturnEmptySetWhenGameIsOver()
        {
            //arrange
            Game.Move(Move.Center);
            Game.Move(Move.SouthWest);
            Game.Move(Move.Northern);
            Game.Move(Move.Western);
            Game.Move(Move.Southern);

            //act
            var result = Game.GetLegalMoves();

            //assert
            result.Should().BeEmpty();
        }

        [TestMethod]
        public void GetLegalMovesShouldReturnAvailableEmptySquaresWhenGameIsNotOver()
        {
            //arrange
            Game.Move(Move.Center);
            Game.Move(Move.SouthWest);
            Game.Move(Move.Northern);
            Game.Move(Move.Western);

            //act
            var result = Game.GetLegalMoves();

            //assert
            result.ShouldAllBeEquivalentTo(new[]
            {
                Move.NorthEast,
                Move.NorthWest,
                Move.Eastern,
                Move.Southern,
                Move.SouthEast
            });
        }

        [TestMethod]
        public void ResetShouldPutGameIntoCorrectState()
        {
            //arrange
            Game.Move(Move.Center);
            Game.Move(Move.SouthWest);
            Game.Move(Move.Northern);
            Game.Move(Move.Western);
            Game.Move(Move.Southern);

            ClearAllEventsReceived();

            //act
            Game.Reset();

            //assert
            Game.GameBoardString.Should().Be(Board.BoardEmptyString);
            Game.CurrentPlayer.Should().Be(Player.X);
            Game.TotalMovesMade.Should().Be(0);
            Game.MoveHistory.Should().BeEmpty();
            Game.GameState.Should().Be(GameState.InPlay);
            Game.GameIsOver.Should().BeFalse();

            GameStateChangedEventsReceivedList.ShouldAllBeEquivalentTo(new[] {
                new GameStateChangedEventArgs(GameState.InPlay, GameChange.Reset, Player.X)
                });

            GameResetEventsReceivedList.Count.Should().Be(1);
        }
    }
}
