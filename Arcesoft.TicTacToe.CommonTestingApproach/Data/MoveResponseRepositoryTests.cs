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
    }
}
