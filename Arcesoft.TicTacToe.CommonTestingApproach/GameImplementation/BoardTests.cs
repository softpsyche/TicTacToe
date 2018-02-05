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
    public class BoardTests
    {
        private Board Board { get; set; }

        [TestInitialize]
        public void Initialize()
        {
            Board = new Board();
        }

        [TestMethod]
        public void BoardShouldStartEmptyAndInPlayAndNotOver()
        {
            //arrange

            //act

            //assert
            Board.ToString().Should().Be(Board.BoardEmptyString);
            Board.State.Should().Be(GameState.InPlay);
            Board.IsGameOver().Should().BeFalse();
        }

        [TestMethod]
        public void IndexerShouldSetValues()
        {
            //arrange
            Board[Move.Center] = Square.X;
            Board[Move.Eastern] = Square.O;
            Board[Move.Northern] = Square.Empty;

            //act
            var centerResult = Board[Move.Center];
            var easternResult = Board[Move.Eastern];
            var northernResult = Board[Move.Northern];

            //assert
            centerResult.Should().Be(Square.X);
            easternResult.Should().Be(Square.O);
            northernResult.Should().Be(Square.Empty);
        }

        [TestMethod]
        public void IndexerShouldSetGameStateToTieForFullBoard()
        {
            //arrange
            Board[Move.NorthWest] = Square.X;
            Board[Move.Northern] = Square.O;
            Board[Move.NorthEast] = Square.X;
            Board[Move.Western] = Square.X;
            Board[Move.Center] = Square.X;
            Board[Move.Eastern] = Square.O;
            Board[Move.SouthWest] = Square.O;
            Board[Move.Southern] = Square.X;

            //act
            Board[Move.SouthEast] = Square.O;

            //assert
            Board.State.Should().Be(GameState.Tie);
        }

        [TestMethod]
        public void IndexerShouldSetGameStateToInPlayForNoWinsAndNoFullBoard()
        {
            //arrange
            Board[Move.Center] = Square.X;

            //act
            Board[Move.SouthEast] = Square.O;

            //assert
            Board.State.Should().Be(GameState.InPlay);
        }

        [TestMethod]
        public void IndexerShouldSetGameStateToXWinForTopHorizontal()
        {
            //arrange
            Board[Move.NorthWest] = Square.X;
            Board[Move.Northern] = Square.X;

            //act
            Board[Move.NorthEast] = Square.X;

            //assert
            Board.State.Should().Be(GameState.XWin);
        }

        [TestMethod]
        public void IndexerShouldSetGameStateToXWinForMiddleHorizontal()
        {
            //arrange
            Board[Move.Western] = Square.X;
            Board[Move.Center] = Square.X;

            //act
            Board[Move.Eastern] = Square.X;

            //assert
            Board.State.Should().Be(GameState.XWin);
        }

        [TestMethod]
        public void IndexerShouldSetGameStateToXWinForBottomHorizontal()
        {
            //arrange
            Board[Move.SouthWest] = Square.X;
            Board[Move.Southern] = Square.X;

            //act
            Board[Move.SouthEast] = Square.X;

            //assert
            Board.State.Should().Be(GameState.XWin);
        }

        [TestMethod]
        public void IndexerShouldSetGameStateToXWinForLeftVertical()
        {
            //arrange
            Board[Move.NorthWest] = Square.X;
            Board[Move.Western] = Square.X;

            //act
            Board[Move.SouthWest] = Square.X;

            //assert
            Board.State.Should().Be(GameState.XWin);
        }

        [TestMethod]
        public void IndexerShouldSetGameStateToXWinForMiddleVertical()
        {
            //arrange
            Board[Move.Northern] = Square.X;
            Board[Move.Center] = Square.X;

            //act
            Board[Move.Southern] = Square.X;

            //assert
            Board.State.Should().Be(GameState.XWin);
        }

        [TestMethod]
        public void IndexerShouldSetGameStateToXWinForRightVertical()
        {
            //arrange
            Board[Move.NorthEast] = Square.X;
            Board[Move.Eastern] = Square.X;

            //act
            Board[Move.SouthEast] = Square.X;

            //assert
            Board.State.Should().Be(GameState.XWin);
        }

        [TestMethod]
        public void IndexerShouldSetGameStateToXWinForDiagonalGrade()
        {
            //arrange
            Board[Move.SouthWest] = Square.X;
            Board[Move.Center] = Square.X;

            //act
            Board[Move.NorthEast] = Square.X;

            //assert
            Board.State.Should().Be(GameState.XWin);
        }

        [TestMethod]
        public void IndexerShouldSetGameStateToXWinForDiagonalSlope()
        {
            //arrange
            Board[Move.NorthWest] = Square.X;
            Board[Move.Center] = Square.X;

            //act
            Board[Move.SouthEast] = Square.X;

            //assert
            Board.State.Should().Be(GameState.XWin);
        }

        [TestMethod]
        public void IndexerShouldSetGameStateToOWinForTopHorizontal()
        {
            //arrange
            Board[Move.NorthWest] = Square.O;
            Board[Move.Northern] = Square.O;

            //act
            Board[Move.NorthEast] = Square.O;

            //assert
            Board.State.Should().Be(GameState.OWin);
        }

        [TestMethod]
        public void IndexerShouldSetGameStateToOWinForMiddleHorizontal()
        {
            //arrange
            Board[Move.Western] = Square.O;
            Board[Move.Center] = Square.O;

            //act
            Board[Move.Eastern] = Square.O;

            //assert
            Board.State.Should().Be(GameState.OWin);
        }

        [TestMethod]
        public void IndexerShouldSetGameStateToOWinForBottomHorizontal()
        {
            //arrange
            Board[Move.SouthWest] = Square.O;
            Board[Move.Southern] = Square.O;

            //act
            Board[Move.SouthEast] = Square.O;

            //assert
            Board.State.Should().Be(GameState.OWin);
        }

        [TestMethod]
        public void IndexerShouldSetGameStateToOWinForLeftVertical()
        {
            //arrange
            Board[Move.NorthWest] = Square.O;
            Board[Move.Western] = Square.O;

            //act
            Board[Move.SouthWest] = Square.O;

            //assert
            Board.State.Should().Be(GameState.OWin);
        }

        [TestMethod]
        public void IndexerShouldSetGameStateToOWinForMiddleVertical()
        {
            //arrange
            Board[Move.Northern] = Square.O;
            Board[Move.Center] = Square.O;

            //act
            Board[Move.Southern] = Square.O;

            //assert
            Board.State.Should().Be(GameState.OWin);
        }

        [TestMethod]
        public void IndexerShouldSetGameStateToOWinForRightVertical()
        {
            //arrange
            Board[Move.NorthEast] = Square.O;
            Board[Move.Eastern] = Square.O;

            //act
            Board[Move.SouthEast] = Square.O;

            //assert
            Board.State.Should().Be(GameState.OWin);
        }

        [TestMethod]
        public void IndexerShouldSetGameStateToOWinForDiagonalGrade()
        {
            //arrange
            Board[Move.SouthWest] = Square.O;
            Board[Move.Center] = Square.O;

            //act
            Board[Move.NorthEast] = Square.O;

            //assert
            Board.State.Should().Be(GameState.OWin);
        }

        [TestMethod]
        public void IndexerShouldSetGameStateToOWinForDiagonalSlope()
        {
            //arrange
            Board[Move.NorthWest] = Square.O;
            Board[Move.Center] = Square.O;

            //act
            Board[Move.SouthEast] = Square.O;

            //assert
            Board.State.Should().Be(GameState.OWin);
        }

        [TestMethod]
        public void IsFullShouldReturnTrueWhenFull()
        {
            //arrange
            foreach (Move move in Enum.GetValues(typeof(Move)))
            {
                Board[move] = Square.X;
            }

            //act
            var result = Board.IsFull;

            //assert
            result.Should().BeTrue();
        }

        [TestMethod]
        public void IsFullShouldReturnFalseWhenNotFull()
        {
            //arrange
            foreach (Move move in Enum.GetValues(typeof(Move)))
            {
                Board[move] = Square.X;
            }

            Board[Move.Center] = Square.Empty;

            //act
            var result = Board.IsFull;

            //assert
            result.Should().BeFalse();
        }

        [TestMethod]
        public void IsGameOverShouldReturnTrueWhenGameStateIsNotInPlay()
        {
            //arrange
            foreach (Move move in Enum.GetValues(typeof(Move)))
            {
                Board[move] = Square.X;
            }

            //act
            var result = Board.IsGameOver();

            //assert
            result.Should().BeTrue();
        }

        [TestMethod]
        public void ClearShouldSetAllSquaresToEmptyAndGameStateToInPlay()
        {
            //arrange
            foreach (Move move in Enum.GetValues(typeof(Move)))
            {
                Board[move] = Square.X;
            }

            //act
            Board.Clear();

            foreach (Move move in Enum.GetValues(typeof(Move)))
            {
                Board[move].Should().Be(Square.Empty);
            }

            Board.State.Should().Be(GameState.InPlay);
        }

        [TestMethod]
        public void SquareIsEmptyShouldReturnTrueWhenSquareIsEmpty()
        {
            //arrange
            Board[Move.Center] = Square.Empty;

            //act
            var result = Board.SquareIsEmpty(Move.Center);

            //assert
            result.Should().BeTrue();
        }

        [TestMethod]
        public void SquareIsEmptyShouldReturnFalseWhenSquareIsNotEmpty()
        {
            //arrange
            Board[Move.Center] = Square.O;

            //act
            var result = Board.SquareIsEmpty(Move.Center);

            //assert
            result.Should().BeFalse();
        }

        [TestMethod]
        public void ToStringShouldReturnValidBoardString()
        {
            //arrange
            Board[Move.Northern] = Square.X;
            Board[Move.Center] = Square.O;
            Board[Move.Southern] = Square.O;

            //act
            var result = Board.ToString();

            //assert
            result.Should().Be("_X__O__O_");
        }

    }
}
