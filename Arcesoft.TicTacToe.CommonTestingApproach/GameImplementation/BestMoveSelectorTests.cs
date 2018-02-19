using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using FluentAssertions;
using Arcesoft.TicTacToe.RandomNumberGeneration;
using Arcesoft.TicTacToe.GameImplementation;
using Arcesoft.TicTacToe.Entities;

namespace Arcesoft.TicTacToe.CommonTestingApproach.GameImplementation
{
    [TestClass]
    public class BestMoveSelectorTests
    {
        private Mock<IRandom> RandomMock { get; set; }

        private BestMoveSelector BestMoveSelector { get; set; }

        [TestInitialize]
        public void Initialize()
        {
            RandomMock = new Mock<IRandom>();

            BestMoveSelector = new BestMoveSelector(RandomMock.Object);
        }

        [TestMethod]
        public void ShouldFindBestMoveResultsForPlayerXWinning()
        {
            //arrange
            var moveResults = new[]
            {
                new MoveResult(Move.Center, GameState.XWin),
                new MoveResult(Move.Northern, GameState.OWin),
                new MoveResult(Move.Southern, GameState.Tie),
                new MoveResult(Move.Eastern, GameState.XWin),
            };

            //act
            var results = BestMoveSelector.FindBestMoveResultsForPlayer(moveResults, Player.X);
            
            //assert
            var expectedMoveResults = new[]
            {
                new MoveResult(Move.Center, GameState.XWin),
                new MoveResult(Move.Eastern, GameState.XWin)
            };

            results.ShouldAllBeEquivalentTo(expectedMoveResults);
        }

        [TestMethod]
        public void ShouldFindBestMoveResultsForPlayerXTieing()
        {
            //arrange
            var moveResults = new[]
            {
                new MoveResult(Move.Center, GameState.OWin),
                new MoveResult(Move.Northern, GameState.OWin),
                new MoveResult(Move.Southern, GameState.Tie),
                new MoveResult(Move.Eastern, GameState.OWin),
            };

            //act
            var results = BestMoveSelector.FindBestMoveResultsForPlayer(moveResults, Player.X);

            //assert
            var expectedMoveResults = new[]
            {
                new MoveResult(Move.Southern, GameState.Tie)
            };

            results.ShouldAllBeEquivalentTo(expectedMoveResults);
        }

        [TestMethod]
        public void ShouldFindBestMoveResultsForPlayerXLosing()
        {
            //arrange
            var moveResults = new[]
            {
                new MoveResult(Move.Center, GameState.OWin),
                new MoveResult(Move.Northern, GameState.OWin),
                new MoveResult(Move.Southern, GameState.OWin),
                new MoveResult(Move.Eastern, GameState.OWin),
            };

            //act
            var results = BestMoveSelector.FindBestMoveResultsForPlayer(moveResults, Player.X);

            //assert
            var expectedMoveResults = new[]
            {
                new MoveResult(Move.Center, GameState.OWin),
                new MoveResult(Move.Northern, GameState.OWin),
                new MoveResult(Move.Southern, GameState.OWin),
                new MoveResult(Move.Eastern, GameState.OWin),
            };

            results.ShouldAllBeEquivalentTo(expectedMoveResults);
        }

        [TestMethod]
        public void ShouldFindBestMoveResultsForPlayerOWinning()
        {
            //arrange
            var moveResults = new[]
            {
                new MoveResult(Move.Center, GameState.XWin),
                new MoveResult(Move.Northern, GameState.OWin),
                new MoveResult(Move.Southern, GameState.Tie),
                new MoveResult(Move.Eastern, GameState.OWin),
            };

            //act
            var results = BestMoveSelector.FindBestMoveResultsForPlayer(moveResults, Player.O);

            //assert
            var expectedMoveResults = new[]
            {
                new MoveResult(Move.Northern, GameState.OWin),
                new MoveResult(Move.Eastern, GameState.OWin)
            };

            results.ShouldAllBeEquivalentTo(expectedMoveResults);
        }

        [TestMethod]
        public void ShouldFindBestMoveResultsForPlayerOTieing()
        {
            //arrange
            var moveResults = new[]
            {
                new MoveResult(Move.Center, GameState.Tie),
                new MoveResult(Move.Southern, GameState.Tie),
                new MoveResult(Move.Eastern, GameState.XWin),
            };

            //act
            var results = BestMoveSelector.FindBestMoveResultsForPlayer(moveResults, Player.O);

            //assert
            var expectedMoveResults = new[]
            {
                new MoveResult(Move.Center, GameState.Tie),
                new MoveResult(Move.Southern, GameState.Tie)
            };

            results.ShouldAllBeEquivalentTo(expectedMoveResults);
        }

        [TestMethod]
        public void ShouldFindBestMoveResultsForPlayerOLosing()
        {
            //arrange
            var moveResults = new[]
            {
                new MoveResult(Move.Center, GameState.XWin),
                new MoveResult(Move.Northern, GameState.XWin),
                new MoveResult(Move.Southern, GameState.XWin),
                new MoveResult(Move.Eastern, GameState.XWin),
            };

            //act
            var results = BestMoveSelector.FindBestMoveResultsForPlayer(moveResults, Player.O);

            //assert
            var expectedMoveResults = new[]
            {
                new MoveResult(Move.Center, GameState.XWin),
                new MoveResult(Move.Northern, GameState.XWin),
                new MoveResult(Move.Southern, GameState.XWin),
                new MoveResult(Move.Eastern, GameState.XWin)
            };

            results.ShouldAllBeEquivalentTo(expectedMoveResults);
        }

        [TestMethod]
        public void ShouldFindRandomBestMoveResultForPlayerOrDefault()
        {
            //arrange
            var moveResults = new[]
            {
                new MoveResult(Move.Center, GameState.XWin),
                new MoveResult(Move.Northern, GameState.OWin),
                new MoveResult(Move.Southern, GameState.Tie),
                new MoveResult(Move.Eastern, GameState.XWin),
            };

            RandomMock
                .Setup(a => a.Next(2))
                .Returns(1);

            //act
            var result = BestMoveSelector.FindRandomBestMoveResultForPlayerOrDefault(moveResults, Player.X);

            //assert
            var expectedMoveResult = new MoveResult(Move.Eastern, GameState.XWin);

            result.ShouldBeEquivalentTo(expectedMoveResult);
        }
    }
}
