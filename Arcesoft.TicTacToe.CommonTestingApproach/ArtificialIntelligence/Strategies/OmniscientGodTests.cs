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

namespace Arcesoft.TicTacToe.CommonTestingApproach.ArtificialIntelligence.Strategies
{
    [TestClass]
    [TestCategory("CommonTestingApproach")]
    public class OmniscientGodTests
    {
        private Mock<IMoveDataAccess> MoveDataAccessMock { get; set; }
        private Mock<IRandom> RandomMock { get; set; }
        private Mock<IGame> GameMock { get; set; }

        //SUT
        private OmniscientGod OmniscientGod { get; set; }

        private string AGameBoardString = "Wubalubadubdub";

        [TestInitialize]
        public void Initialize()
        {
            GameMock = new Mock<IGame>();
            RandomMock = new Mock<IRandom>();
            MoveDataAccessMock = new Mock<IMoveDataAccess>();

            OmniscientGod = new OmniscientGod(MoveDataAccessMock.Object, RandomMock.Object);
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
        public void MakeMoveShouldMakeWinningMoveForX()
        {
            //arrange
            GameMock
                .Setup(a => a.GameIsOver)
                .Returns(false);

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

            RandomMock
                .Setup(a => a.Next(It.IsAny<int>()))
                .Returns(0);

            //act
            OmniscientGod.MakeMove(GameMock.Object);

            //assert
            GameMock
                .Verify(a => a.Move(Move.Northern), Times.Once());
        }

        [TestMethod]
        public void MakeMoveShouldMakeTieMoveForX()
        {
            //arrange
            GameMock
                .Setup(a => a.GameIsOver)
                .Returns(false);

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
                        Outcome = GameState.OWin,
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

            RandomMock
                .Setup(a => a.Next(It.IsAny<int>()))
                .Returns(0);

            //act
            OmniscientGod.MakeMove(GameMock.Object);

            //assert
            GameMock
                .Verify(a => a.Move(Move.Southern), Times.Once());
        }

        [TestMethod]
        public void MakeMoveShouldMakeLosingMoveForX()
        {
            //arrange
            GameMock
                .Setup(a => a.GameIsOver)
                .Returns(false);

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
                        Outcome = GameState.OWin,
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
                        Outcome = GameState.OWin,
                        Board = "_________",
                        Player = Player.X
                    }
                };
            MoveDataAccessMock
                .Setup(a => a.FindMoveResponses(AGameBoardString, Player.X))
                .Returns(moveResponses);

            RandomMock
                .Setup(a => a.Next(It.IsAny<int>()))
                .Returns(0);

            //act
            OmniscientGod.MakeMove(GameMock.Object);

            //assert
            GameMock
                .Verify(a => a.Move(Move.Northern), Times.Once());
        }

        [TestMethod]
        public void MakeMoveShouldMakeWinningMoveForO()
        {
            //arrange
            GameMock
                .Setup(a => a.GameIsOver)
                .Returns(false);

            GameMock
                .Setup(a => a.GameBoardString)
                .Returns(AGameBoardString);

            GameMock
                .Setup(a => a.CurrentPlayer)
                .Returns(Player.O);

            var moveResponses = new MoveResponse[]
                {
                    new MoveResponse()
                    {
                        Response = Move.Northern,
                        Outcome = GameState.XWin,
                        Board = "_________",
                        Player = Player.O
                    },
                    new MoveResponse()
                    {
                        Response = Move.Center,
                        Outcome = GameState.OWin,
                        Board = "_________",
                        Player = Player.O
                    },
                    new MoveResponse()
                    {
                        Response = Move.Southern,
                        Outcome = GameState.Tie,
                        Board = "_________",
                        Player = Player.O
                    }
                };
            MoveDataAccessMock
                .Setup(a => a.FindMoveResponses(AGameBoardString, Player.O))
                .Returns(moveResponses);

            RandomMock
                .Setup(a => a.Next(It.IsAny<int>()))
                .Returns(0);

            //act
            OmniscientGod.MakeMove(GameMock.Object);

            //assert
            GameMock
                .Verify(a => a.Move(Move.Center), Times.Once());
        }

        [TestMethod]
        public void MakeMoveShouldMakeTieMoveForO()
        {
            //arrange
            GameMock
                .Setup(a => a.GameIsOver)
                .Returns(false);

            GameMock
                .Setup(a => a.GameBoardString)
                .Returns(AGameBoardString);

            GameMock
                .Setup(a => a.CurrentPlayer)
                .Returns(Player.O);

            var moveResponses = new MoveResponse[]
                {
                    new MoveResponse()
                    {
                        Response = Move.Northern,
                        Outcome = GameState.XWin,
                        Board = "_________",
                        Player = Player.O
                    },
                    new MoveResponse()
                    {
                        Response = Move.Center,
                        Outcome = GameState.XWin,
                        Board = "_________",
                        Player = Player.O
                    },
                    new MoveResponse()
                    {
                        Response = Move.Southern,
                        Outcome = GameState.Tie,
                        Board = "_________",
                        Player = Player.O
                    }
                };
            MoveDataAccessMock
                .Setup(a => a.FindMoveResponses(AGameBoardString, Player.O))
                .Returns(moveResponses);

            RandomMock
                .Setup(a => a.Next(It.IsAny<int>()))
                .Returns(0);

            //act
            OmniscientGod.MakeMove(GameMock.Object);

            //assert
            GameMock
                .Verify(a => a.Move(Move.Southern), Times.Once());
        }

        [TestMethod]
        public void MakeMoveShouldMakeLosingMoveForO()
        {
            //arrange
            GameMock
                .Setup(a => a.GameIsOver)
                .Returns(false);

            GameMock
                .Setup(a => a.GameBoardString)
                .Returns(AGameBoardString);

            GameMock
                .Setup(a => a.CurrentPlayer)
                .Returns(Player.O);

            var moveResponses = new MoveResponse[]
                {
                    new MoveResponse()
                    {
                        Response = Move.Northern,
                        Outcome = GameState.XWin,
                        Board = "_________",
                        Player = Player.O
                    },
                    new MoveResponse()
                    {
                        Response = Move.Center,
                        Outcome = GameState.XWin,
                        Board = "_________",
                        Player = Player.O
                    },
                    new MoveResponse()
                    {
                        Response = Move.Southern,
                        Outcome = GameState.XWin,
                        Board = "_________",
                        Player = Player.O
                    }
                };
            MoveDataAccessMock
                .Setup(a => a.FindMoveResponses(AGameBoardString, Player.O))
                .Returns(moveResponses);

            RandomMock
                .Setup(a => a.Next(It.IsAny<int>()))
                .Returns(0);

            //act
            OmniscientGod.MakeMove(GameMock.Object);

            //assert
            GameMock
                .Verify(a => a.Move(Move.Northern), Times.Once());
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
