using Arcesoft.TicTacToe.ArtificialIntelligence;
using Arcesoft.TicTacToe.Data;
using Arcesoft.TicTacToe.Entities;
using Arcesoft.TicTacToe.RandomNumberGeneration;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arcesoft.TicTacToe.CommonTestingApproach.Data
{
    [TestClass]
    [TestCategory("CommonTestingApproach")]
    public class MoveDataAccessTests
    {
        private Mock<IMoveRepository> MoveRepositoryMock { get; set; }
        private Mock<IRandom> RandomMock { get; set; }
        /// <summary>
        /// The system under test
        /// </summary>
        private MoveDataAccess MoveDataAccess { get; set; }

        private string ABoardPosition = "____X____";

        [TestInitialize]
        public void Initialize()
        {
            MoveRepositoryMock = new Mock<IMoveRepository>();
            RandomMock = new Mock<IRandom>();

            MoveDataAccess = new MoveDataAccess(MoveRepositoryMock.Object, RandomMock.Object);
        }

        [TestMethod]
        public void FindBestMoveShouldThrowGameExceptionWhenNoMovesAreFound()
        {
            //Arrange
            MoveRepositoryMock
                .Setup(a => a.FindBoardStates(ABoardPosition, Player.X))
                .Returns(Enumerable.Empty<MoveResponse>());

            //Act
            Action action = () => MoveDataAccess.FindBestMove(ABoardPosition, Player.X, true);

            //Assert
            action
                .ShouldThrow<GameException>()
                .WithMessage("There is no response for that board position and player turn.");
        }

        [TestMethod]
        public void FindBestMoveShouldFindWinningMove()
        {
            //Arrange
            var moveResponses = GivenIHaveTheFollowingMoveResponses(GameState.XWin, GameState.OWin, GameState.Tie).ToList();
            moveResponses.First().Response = Move.Center;

            MoveRepositoryMock
                .Setup(a => a.FindBoardStates(ABoardPosition, Player.X))
                .Returns(moveResponses);

            //Act
            var result = MoveDataAccess.FindBestMove(ABoardPosition, Player.X, true);

            //Assert
            RandomMock.Verify(a => a.Next(1), Times.Once);

            result.ShouldBeEquivalentTo(Move.Center);
        }

        [TestMethod]
        public void FindBestMoveShouldFindTieMove()
        {
            //Arrange
            var moveResponses = GivenIHaveTheFollowingMoveResponses(GameState.Tie, GameState.OWin, GameState.Tie).ToList();
            moveResponses.First().Response = Move.Center;

            MoveRepositoryMock
                .Setup(a => a.FindBoardStates(ABoardPosition, Player.X))
                .Returns(moveResponses);

            //Act
            var result = MoveDataAccess.FindBestMove(ABoardPosition, Player.X, true);

            //Assert
            RandomMock.Verify(a => a.Next(2), Times.Once);

            result.ShouldBeEquivalentTo(Move.Center);
        }

        [TestMethod]
        public void FindBestMoveShouldFindLosingMove()
        {
            //Arrange
            var moveResponses = GivenIHaveTheFollowingMoveResponses(GameState.OWin, GameState.OWin, GameState.OWin).ToList();
            moveResponses.Last().Response = Move.Center;
            MoveRepositoryMock
                .Setup(a => a.FindBoardStates(ABoardPosition, Player.X))
                .Returns(moveResponses);

            RandomMock
                .Setup(a => a.Next(3))
                .Returns(2);

            //Act
            var result = MoveDataAccess.FindBestMove(ABoardPosition, Player.X, true);

            //Assert
            RandomMock.Verify(a => a.Next(3), Times.Once);

            result.ShouldBeEquivalentTo(Move.Center);
        }

        [TestMethod]
        public void ShouldFindMoveResponses()
        {
            //Arrange
            var moveResponses = GivenIHaveTheFollowingMoveResponses(GameState.XWin, GameState.OWin, GameState.Tie).ToList();

            MoveRepositoryMock
                .Setup(a => a.FindBoardStates(ABoardPosition, Player.X))
                .Returns(moveResponses);

            //Act
            var result = MoveDataAccess.FindMoveResponses(ABoardPosition, Player.X);

            //Assert
            result.ShouldBeEquivalentTo(moveResponses);
        }


        private IEnumerable<MoveResponse> GivenIHaveTheFollowingMoveResponses(params GameState[] gameState)
        {
            return gameState.Select(a => new MoveResponse { Outcome = a });
        }
    }
}
