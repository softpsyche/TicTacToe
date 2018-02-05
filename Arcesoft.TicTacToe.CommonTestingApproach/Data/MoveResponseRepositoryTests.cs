using Arcesoft.TicTacToe.ArtificialIntelligence;
using Arcesoft.TicTacToe.Data;
using Arcesoft.TicTacToe.Database;
using Arcesoft.TicTacToe.Entities;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Arcesoft.TicTacToe.CommonTestingApproach.Data
{
    [TestClass]
    [TestCategory("CommonTestingApproach")]
    public class MoveResponseRepositoryTests
    {
        private Mock<ILiteDatabaseFactory> MockLiteDatabaseFactory { get; set; }
        private Mock<ILiteDatabase> MockLiteDatabase { get; set; }
        private MoveResponseRepository MoveResponseRepository { get; set; }

        [TestInitialize]
        public void Initialize()
        {
            MockLiteDatabaseFactory = new Mock<ILiteDatabaseFactory>();
            MockLiteDatabase = new Mock<ILiteDatabase>();
            MockLiteDatabaseFactory
                .Setup(a => a.OpenOrCreate(MoveResponseRepository.MoveRepositoryName))
                .Returns(MockLiteDatabase.Object);

            MoveResponseRepository = new MoveResponseRepository(MockLiteDatabaseFactory.Object);
        }

        [TestMethod]
        public void ShouldDeleteAllMoveResponses()
        {
            //arrange

            //act
            MoveResponseRepository.DeleteAllMoveResponses();

            //assert
            MockLiteDatabase
                .Verify(a => a.DropCollection<MoveResponseRecord>(), Times.Once());
        }

        [TestMethod]
        public void ShouldInsertMoveResponses()
        {
            //arrange
            var moveResponses = new MoveResponse[]
                {
                    new MoveResponse()
                    {
                        Board = "____X____",
                        Outcome = GameState.XWin,
                        Player = Player.X,
                        Response = Move.Southern
                    },
                    new MoveResponse()
                    {
                        Board = "X__XX__OO",
                        Outcome = GameState.OWin,
                        Player = Player.O,
                        Response = Move.Northern
                    }
                };

            IEnumerable<MoveResponseRecord> responsesReceived = null;
            MockLiteDatabase
                .Setup(a => a.InsertBulk(It.IsAny<IEnumerable<MoveResponseRecord>>()))
                .Callback<IEnumerable<MoveResponseRecord>>(a => responsesReceived = a);

            //act
            MoveResponseRepository.InsertMoveResponses(moveResponses);

            //assert
            var expectedMoveResponses = new MoveResponseRecord[]
                {
                    new MoveResponseRecord()
                    {
                        Board = "____X____",
                        Outcome = GameState.XWin,
                        Player = Player.X,
                        Response = Move.Southern
                    },
                    new MoveResponseRecord()
                    {
                        Board = "X__XX__OO",
                        Outcome = GameState.OWin,
                        Player = Player.O,
                        Response = Move.Northern
                    }
                };

            MockLiteDatabase
                .Verify(a => a.InsertBulk(It.IsAny<IEnumerable<MoveResponseRecord>>()), Times.Once());

            MockLiteDatabase
                .Verify(a => a.EnsureIndex(It.IsAny<Expression<Func<MoveResponseRecord, string>>>(), false), Times.Once());

            expectedMoveResponses.ShouldAllBeEquivalentTo(responsesReceived);
        }

        [TestMethod]
        public void ShouldFindMoveResponses()
        {
            //arrange
            var aBoardPosition = "___XOX___";
            var aPlayer = Player.X;

            var moveResponseRecords = new MoveResponseRecord[]
            {
                new MoveResponseRecord()
                {
                    Board = "____X____",
                    Outcome = GameState.XWin,
                    Player = Player.X,
                    Response = Move.Southern
                },
                new MoveResponseRecord()
                {
                    Board = "X__XX__OO",
                    Outcome = GameState.OWin,
                    Player = Player.O,
                    Response = Move.Northern
                }
            };
            MockLiteDatabase
                .Setup(a => a.FindByIndex(It.IsAny<Expression<Func<MoveResponseRecord, bool>>>()))
                .Returns(moveResponseRecords);

            //act
            var results = MoveResponseRepository.FindMoveResponses(aBoardPosition,aPlayer);

            //assert
            var expectedMoveResponses = new MoveResponse[]
            {
                new MoveResponse()
                {
                    Board = "____X____",
                    Outcome = GameState.XWin,
                    Player = Player.X,
                    Response = Move.Southern
                }
            };

            results.ShouldAllBeEquivalentTo(expectedMoveResponses);
        }
    }
}
