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
    public class MoveRepositoryTests
    {
        private Mock<IMoveDatabase> MoveDatabaseMock { get; set; }
        private Mock<IMovesDataTable> MovesDataTableMock { get; set; }
        private MoveRepository MoveRepository { get; set; }

        [TestInitialize]
        public void Initialize()
        {
            MoveDatabaseMock = new Mock<IMoveDatabase>();
            MovesDataTableMock = new Mock<IMovesDataTable>();
            MoveDatabaseMock.Setup(a => a.MovesDataTable).Returns(MovesDataTableMock.Object);

            MoveRepository = new MoveRepository(MoveDatabaseMock.Object);
        }


        [TestMethod]
        public void ShouldFindBoardStates()
        {
            //arrange
            var currentBoardPosition = "____X____";
            var currentPlayer = Player.O;

            //setup the rows to return
            var searchPattern = String.Format("Board = '{0}' AND Player = '{1}'", currentBoardPosition, currentPlayer.ToString());
            var dataTable = new MovesDataTable();
            dataTable.AddMovesRow(currentBoardPosition, currentPlayer.ToString(), (int)Move.Western, GameState.XWin.ToString());
            dataTable.AddMovesRow(currentBoardPosition, currentPlayer.ToString(), (int)Move.Eastern, GameState.Tie.ToString());
            dataTable.AddMovesRow(currentBoardPosition, currentPlayer.ToString(), (int)Move.Southern, GameState.OWin.ToString());
            var dataRows = new MovesRow[3];
            dataTable.Rows.CopyTo(dataRows, 0);
            MovesDataTableMock
                .Setup(a => a.Select(searchPattern))
                .Returns(dataRows);

            //act
            var result = MoveRepository.FindBoardStates(currentBoardPosition, currentPlayer);

            //assert
            var expectedMoveResponses = new[]
            {
                new MoveResponse()
                {
                    Board = currentBoardPosition,
                    Player = currentPlayer,
                    Outcome = GameState.XWin,
                    Response = Move.Western
                },
                new MoveResponse()
                {
                    Board = currentBoardPosition,
                    Player = currentPlayer,
                    Outcome = GameState.Tie,
                    Response = Move.Eastern
                },
                new MoveResponse()
                {
                    Board = currentBoardPosition,
                    Player = currentPlayer,
                    Outcome = GameState.OWin,
                    Response = Move.Southern
                },
            };

            result.ShouldBeEquivalentTo(expectedMoveResponses);
        }
    }
}
