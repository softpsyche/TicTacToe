using Arcesoft.TicTacToe.ArtificialIntelligence;
using Arcesoft.TicTacToe.Data;
using Arcesoft.TicTacToe.Entities;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Arcesoft.TicTacToe.Data.TicTacToeDataSet;

namespace Arcesoft.TicTacToe.CommonTestingApproach.Data
{
    [TestClass]
    [TestCategory("CommonTestingApproach")]
    public class MoveDatabaseTests
    {
        private Mock<IMoveEvaluator> MoveEvaluatorMock { get; set; }
        private Mock<IFileAccess> FileAccessMock { get; set; }
        private MoveDatabase MoveDatabase { get; set; }

        private MoveResult AMoveResult { get; set; }
        private string ABoardLayout { get; set; }
        private Player APlayer { get; set; }
        private BoardState ABoardState { get; set; }

        [TestInitialize]
        public void Initialize()
        {
            MoveEvaluatorMock = new Mock<IMoveEvaluator>();
            FileAccessMock = new Mock<IFileAccess>();

            MoveDatabase = new MoveDatabase(
                MoveEvaluatorMock.Object, 
                FileAccessMock.Object);

            AMoveResult = new MoveResult(Entities.Move.Center, Entities.GameState.InPlay);
            ABoardLayout = "____X____";
            APlayer = Player.X;
            ABoardState = new BoardState(AMoveResult, ABoardLayout, APlayer);
        }

        [TestMethod]
        public void ShouldCreateMovesDataTable()
        {
            //arrange
            FileAccessMock
                .Setup(a => a.Exists(MoveDatabase.DefaultMoveDatabaseFilePath))
                .Returns(false);

            MoveEvaluatorMock
                .Setup(a => a.FindAllMoves(null))
                .Returns(new[] { ABoardState });

            //act
            var result = MoveDatabase.MovesDataTable as MovesDataTable;

            //assert
            var expectedTable = new MovesDataTable();
            expectedTable.AddMovesRow(ABoardLayout, APlayer.ToString(), (int)AMoveResult.MoveMade, AMoveResult.BoardStateAfterMove.ToString());

            result.Rows.Count.Should().Be(1);
            result.Rows[0].ItemArray.ShouldAllBeEquivalentTo(expectedTable.Rows[0].ItemArray);
            
            //attempt to save database was made
            FileAccessMock.Verify(a => a.SerializeBinary(result, MoveDatabase.DefaultMoveDatabaseFilePath), Times.Once());
        }

        [TestMethod]
        public void ShouldLoadMovesDataTable()
        {
            //arrange
            FileAccessMock
                .Setup(a => a.Exists(MoveDatabase.DefaultMoveDatabaseFilePath))
                .Returns(true);

            var expectedTable = new MovesDataTable();
            expectedTable.AddMovesRow(ABoardLayout, APlayer.ToString(), (int)AMoveResult.MoveMade, AMoveResult.BoardStateAfterMove.ToString());
            FileAccessMock
                .Setup(a => a.DeserializeBinary<MovesDataTable>(MoveDatabase.DefaultMoveDatabaseFilePath))
                .Returns(expectedTable);

            //act
            var result = MoveDatabase.MovesDataTable as MovesDataTable;

            //assert
            result.Rows.Count.Should().Be(1);
            result.Rows[0].ItemArray.ShouldAllBeEquivalentTo(expectedTable.Rows[0].ItemArray);

            //no call to move evaluator was made
            MoveEvaluatorMock
                .Verify(a => a.FindAllMoves(null), Times.Never());
        }

    }
}
