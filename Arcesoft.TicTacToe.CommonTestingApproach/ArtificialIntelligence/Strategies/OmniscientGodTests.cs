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
                .Returns("MrGiggidy");

            GameMock
                .Setup(a => a.CurrentPlayer)
                .Returns(Entities.Player.X);

            var moveResponses = new MoveResponse[]
                {
                    new MoveResponse()
                    {
                        Response = Entities.Move.Northern,
                        Outcome = Entities.GameState.XWin,
                        Board = "_________",
                        Player = Entities.Player.X
                    },
                    new MoveResponse()
                    {
                        Response = Entities.Move.Center,
                        Outcome = Entities.GameState.OWin,
                        Board = "_________",
                        Player = Entities.Player.X
                    },
                    new MoveResponse()
                    {
                        Response = Entities.Move.Southern,
                        Outcome = Entities.GameState.Tie,
                        Board = "_________",
                        Player = Entities.Player.X
                    }
                };
            MoveDataAccessMock
                .Setup(a => a.FindMoveResponses("MrGiggidy", Entities.Player.X))
                .Returns(moveResponses);

            RandomMock
                .Setup(a => a.Next(It.IsAny<int>()))
                .Returns(0);

            //act
            OmniscientGod.MakeMove(GameMock.Object);

            //assert
            GameMock
                .Verify(a => a.Move(Entities.Move.Northern), Times.Once());
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
                .Returns("MrGiggidy");

            GameMock
                .Setup(a => a.CurrentPlayer)
                .Returns(Entities.Player.X);

            var moveResponses = new MoveResponse[]
                {
                    new MoveResponse()
                    {
                        Response = Entities.Move.Northern,
                        Outcome = Entities.GameState.OWin,
                        Board = "_________",
                        Player = Entities.Player.X
                    },
                    new MoveResponse()
                    {
                        Response = Entities.Move.Center,
                        Outcome = Entities.GameState.OWin,
                        Board = "_________",
                        Player = Entities.Player.X
                    },
                    new MoveResponse()
                    {
                        Response = Entities.Move.Southern,
                        Outcome = Entities.GameState.Tie,
                        Board = "_________",
                        Player = Entities.Player.X
                    }
                };
            MoveDataAccessMock
                .Setup(a => a.FindMoveResponses("MrGiggidy", Entities.Player.X))
                .Returns(moveResponses);

            RandomMock
                .Setup(a => a.Next(It.IsAny<int>()))
                .Returns(0);

            //act
            OmniscientGod.MakeMove(GameMock.Object);

            //assert
            GameMock
                .Verify(a => a.Move(Entities.Move.Southern), Times.Once());
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
                .Returns("MrGiggidy");

            GameMock
                .Setup(a => a.CurrentPlayer)
                .Returns(Entities.Player.X);

            var moveResponses = new MoveResponse[]
                {
                    new MoveResponse()
                    {
                        Response = Entities.Move.Northern,
                        Outcome = Entities.GameState.OWin,
                        Board = "_________",
                        Player = Entities.Player.X
                    },
                    new MoveResponse()
                    {
                        Response = Entities.Move.Center,
                        Outcome = Entities.GameState.OWin,
                        Board = "_________",
                        Player = Entities.Player.X
                    },
                    new MoveResponse()
                    {
                        Response = Entities.Move.Southern,
                        Outcome = Entities.GameState.OWin,
                        Board = "_________",
                        Player = Entities.Player.X
                    }
                };
            MoveDataAccessMock
                .Setup(a => a.FindMoveResponses("MrGiggidy", Entities.Player.X))
                .Returns(moveResponses);

            RandomMock
                .Setup(a => a.Next(It.IsAny<int>()))
                .Returns(0);

            //act
            OmniscientGod.MakeMove(GameMock.Object);

            //assert
            GameMock
                .Verify(a => a.Move(Entities.Move.Northern), Times.Once());
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
                .Returns("MrGiggidy");

            GameMock
                .Setup(a => a.CurrentPlayer)
                .Returns(Entities.Player.O);

            var moveResponses = new MoveResponse[]
                {
                    new MoveResponse()
                    {
                        Response = Entities.Move.Northern,
                        Outcome = Entities.GameState.XWin,
                        Board = "_________",
                        Player = Entities.Player.O
                    },
                    new MoveResponse()
                    {
                        Response = Entities.Move.Center,
                        Outcome = Entities.GameState.OWin,
                        Board = "_________",
                        Player = Entities.Player.O
                    },
                    new MoveResponse()
                    {
                        Response = Entities.Move.Southern,
                        Outcome = Entities.GameState.Tie,
                        Board = "_________",
                        Player = Entities.Player.O
                    }
                };
            MoveDataAccessMock
                .Setup(a => a.FindMoveResponses("MrGiggidy", Entities.Player.O))
                .Returns(moveResponses);

            RandomMock
                .Setup(a => a.Next(It.IsAny<int>()))
                .Returns(0);

            //act
            OmniscientGod.MakeMove(GameMock.Object);

            //assert
            GameMock
                .Verify(a => a.Move(Entities.Move.Center), Times.Once());
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
                .Returns("MrGiggidy");

            GameMock
                .Setup(a => a.CurrentPlayer)
                .Returns(Entities.Player.O);

            var moveResponses = new MoveResponse[]
                {
                    new MoveResponse()
                    {
                        Response = Entities.Move.Northern,
                        Outcome = Entities.GameState.XWin,
                        Board = "_________",
                        Player = Entities.Player.O
                    },
                    new MoveResponse()
                    {
                        Response = Entities.Move.Center,
                        Outcome = Entities.GameState.XWin,
                        Board = "_________",
                        Player = Entities.Player.O
                    },
                    new MoveResponse()
                    {
                        Response = Entities.Move.Southern,
                        Outcome = Entities.GameState.Tie,
                        Board = "_________",
                        Player = Entities.Player.O
                    }
                };
            MoveDataAccessMock
                .Setup(a => a.FindMoveResponses("MrGiggidy", Entities.Player.O))
                .Returns(moveResponses);

            RandomMock
                .Setup(a => a.Next(It.IsAny<int>()))
                .Returns(0);

            //act
            OmniscientGod.MakeMove(GameMock.Object);

            //assert
            GameMock
                .Verify(a => a.Move(Entities.Move.Southern), Times.Once());
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
                .Returns("MrGiggidy");

            GameMock
                .Setup(a => a.CurrentPlayer)
                .Returns(Entities.Player.O);

            var moveResponses = new MoveResponse[]
                {
                    new MoveResponse()
                    {
                        Response = Entities.Move.Northern,
                        Outcome = Entities.GameState.XWin,
                        Board = "_________",
                        Player = Entities.Player.O
                    },
                    new MoveResponse()
                    {
                        Response = Entities.Move.Center,
                        Outcome = Entities.GameState.XWin,
                        Board = "_________",
                        Player = Entities.Player.O
                    },
                    new MoveResponse()
                    {
                        Response = Entities.Move.Southern,
                        Outcome = Entities.GameState.XWin,
                        Board = "_________",
                        Player = Entities.Player.O
                    }
                };
            MoveDataAccessMock
                .Setup(a => a.FindMoveResponses("MrGiggidy", Entities.Player.O))
                .Returns(moveResponses);

            RandomMock
                .Setup(a => a.Next(It.IsAny<int>()))
                .Returns(0);

            //act
            OmniscientGod.MakeMove(GameMock.Object);

            //assert
            GameMock
                .Verify(a => a.Move(Entities.Move.Northern), Times.Once());
        }

        [TestMethod]
        public void ShouldFindMoveResults()
        {
            //arrange
            GameMock
                .Setup(a => a.GameBoardString)
                .Returns("MrGiggidy");

            GameMock
                .Setup(a => a.CurrentPlayer)
                .Returns(Entities.Player.X);

            var moveResponses = new MoveResponse[]
                {
                    new MoveResponse()
                    {
                        Response = Entities.Move.Northern,
                        Outcome = Entities.GameState.XWin,
                        Board = "_________",
                        Player = Entities.Player.X
                    },
                    new MoveResponse()
                    {
                        Response = Entities.Move.Center,
                        Outcome = Entities.GameState.OWin,
                        Board = "_________",
                        Player = Entities.Player.X
                    },
                    new MoveResponse()
                    {
                        Response = Entities.Move.Southern,
                        Outcome = Entities.GameState.Tie,
                        Board = "_________",
                        Player = Entities.Player.X
                    }
                };
            MoveDataAccessMock
                .Setup(a => a.FindMoveResponses("MrGiggidy", Entities.Player.X))
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
