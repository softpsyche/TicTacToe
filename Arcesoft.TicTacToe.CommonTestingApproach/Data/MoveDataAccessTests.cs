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
        /// <summary>
        /// The system under test
        /// </summary>
        private MoveDataAccess MoveDataAccess { get; set; }

        private string ABoardPosition = "____X____";

        [TestInitialize]
        public void Initialize()
        {
            MoveRepositoryMock = new Mock<IMoveRepository>();

            MoveDataAccess = new MoveDataAccess(MoveRepositoryMock.Object);
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
