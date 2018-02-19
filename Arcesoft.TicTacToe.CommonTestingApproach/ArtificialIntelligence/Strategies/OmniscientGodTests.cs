using Arcesoft.TicTacToe.RandomNumberGeneration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using FluentAssertions;
using Arcesoft.TicTacToe.ArtificialIntelligence.Strategies;
using Arcesoft.TicTacToe.Data;
using Arcesoft.TicTacToe.Entities;
using Arcesoft.TicTacToe.GameImplementation;

namespace Arcesoft.TicTacToe.CommonTestingApproach.ArtificialIntelligence.Strategies
{
    [TestClass]
    [TestCategory("CommonTestingApproach")]
    public class OmniscientGodTests
    {
        private Mock<IMoveDataAccess> MoveDataAccessMock { get; set; }
        private Mock<IBestMoveSelector> BestMoveSelectorMock { get; set; }
        private Mock<IGame> GameMock { get; set; }

        //SUT
        private OmniscientGod OmniscientGod { get; set; }

        private string AGameBoardString = "Wubalubadubdub";

        [TestInitialize]
        public void Initialize()
        {
            GameMock = new Mock<IGame>();
            MoveDataAccessMock = new Mock<IMoveDataAccess>();
            BestMoveSelectorMock = new Mock<IBestMoveSelector>();

            OmniscientGod = new OmniscientGod(
                MoveDataAccessMock.Object, 
                BestMoveSelectorMock.Object);
        }

        [TestMethod]
        public void MakeMoveShouldThrowExceptionWhenGameIsOver()
        {
            //arrange
            GameMock
                .Setup(a => a.GameIsOver)
                .Returns(true);

            //act
            Action act = () => OmniscientGod.MakeMove(GameMock.Object);

            //assert
            act
                .ShouldThrow<GameException>()
                .WithMessage($"Unable to make a move because the game is over.");
        }

        [TestMethod]
        public void MakeMoveShouldThrowExceptionWhenNoMoveResultsFound()
        {
            //arrange
            GameMock
                .Setup(a => a.GameIsOver)
                .Returns(false);

            var aBoardPosition = "Giggidy";
            GameMock
                .Setup(a => a.GameBoardString)
                .Returns(aBoardPosition);

            var aPlayer = Player.X;
            GameMock
                .Setup(a => a.CurrentPlayer)
                .Returns(aPlayer);

            var moveResponses = new MoveResponse[0];
            MoveDataAccessMock
                .Setup(a => a.FindMoveResponses(aBoardPosition, aPlayer))
                .Returns(moveResponses);

            MoveResult moveResult = null;
            BestMoveSelectorMock
                .Setup(a => a.FindRandomBestMoveResultForPlayerOrDefault(moveResponses, aPlayer))
                .Returns(moveResult);

            //act
            Action act = () => OmniscientGod.MakeMove(GameMock.Object);

            //assert
            act
                .ShouldThrow<Exception>()
                .WithMessage("Unable to make a move because there are no available moves for game board Giggidy. Possible corrupt move data access or game.");
        }

        [TestMethod]
        public void MakeMoveShouldMakeRandomMove()
        {
            //arrange
            GameMock
                .Setup(a => a.GameIsOver)
                .Returns(false);

            var aBoardPosition = "Giggidy";
            GameMock
                .Setup(a => a.GameBoardString)
                .Returns(aBoardPosition);

            var aPlayer = Player.X;
            GameMock
                .Setup(a => a.CurrentPlayer)
                .Returns(aPlayer);

            var moveResponses = new MoveResponse[0];
            MoveDataAccessMock
                .Setup(a => a.FindMoveResponses(aBoardPosition, aPlayer))
                .Returns(moveResponses);

            var moveResult = new MoveResult(Move.Northern, GameState.XWin);
            BestMoveSelectorMock
                .Setup(a => a.FindRandomBestMoveResultForPlayerOrDefault(moveResponses, aPlayer))
                .Returns(moveResult);

            //act
            OmniscientGod.MakeMove(GameMock.Object);

            //assert
            GameMock
                .Verify(a => a.Move(moveResult.MoveMade), Times.Once());
        }

        [TestMethod]
        public void MakeMoveShouldMakeNonRandomMove()
        {
            //arrange
            GameMock
                .Setup(a => a.GameIsOver)
                .Returns(false);

            var aBoardPosition = "Giggidy";
            GameMock
                .Setup(a => a.GameBoardString)
                .Returns(aBoardPosition);

            var aPlayer = Player.X;
            GameMock
                .Setup(a => a.CurrentPlayer)
                .Returns(aPlayer);

            var moveResponses = new MoveResponse[0];
            MoveDataAccessMock
                .Setup(a => a.FindMoveResponses(aBoardPosition, aPlayer))
                .Returns(moveResponses);

            var moveResult = new MoveResult(Move.Northern, GameState.XWin);
            BestMoveSelectorMock
                .Setup(a => a.FindBestMoveResultsForPlayer(moveResponses, aPlayer))
                .Returns(new[] { moveResult });

            //act
            OmniscientGod.MakeMove(GameMock.Object, false);

            //assert
            GameMock
                .Verify(a => a.Move(moveResult.MoveMade), Times.Once());
        }


        [TestMethod]
        public void ShouldFindMoveResults()
        {
            //arrange
            GameMock
                .Setup(a => a.GameBoardString)
                .Returns(AGameBoardString);

            GameMock
                .Setup(a => a.CurrentPlayer)
                .Returns(Player.X);

            var moveResponses = new MoveResponse[]
                {
                    new MoveResponse()
                    {
                        Response = Move.Northern,
                        Outcome = GameState.XWin,
                        Board = "_________",
                        Player = Player.X
                    },
                    new MoveResponse()
                    {
                        Response = Move.Center,
                        Outcome = GameState.OWin,
                        Board = "_________",
                        Player = Player.X
                    },
                    new MoveResponse()
                    {
                        Response = Move.Southern,
                        Outcome = GameState.Tie,
                        Board = "_________",
                        Player = Player.X
                    }
                };
            MoveDataAccessMock
                .Setup(a => a.FindMoveResponses(AGameBoardString, Player.X))
                .Returns(moveResponses);

            //act
            var results = OmniscientGod.FindMoveResults(GameMock.Object);

            //assert
            var expectedMoveResults = new MoveResult[]
                {
                    new MoveResult(Move.Northern, GameState.XWin),
                    new MoveResult(Move.Center, GameState.OWin),
                    new MoveResult(Move.Southern, GameState.Tie)
                };

            results.ShouldAllBeEquivalentTo(expectedMoveResults);

        }
    }
}
