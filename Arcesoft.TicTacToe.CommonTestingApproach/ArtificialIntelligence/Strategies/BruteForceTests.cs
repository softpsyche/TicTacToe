using Arcesoft.TicTacToe.ArtificialIntelligence.Strategies;
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

namespace Arcesoft.TicTacToe.CommonTestingApproach.ArtificialIntelligence.Strategies
{
    [TestClass]
    [TestCategory("CommonTestingApproach")]
    public class BruteForceTests
    {
        private Mock<ITicTacToeFactory> TicTacToeFactoryMock { get; set; }
        private Mock<IRandom> RandomMock { get; set; }
        private Mock<IGame> GameMock { get; set; }

        //SUT
        private BruteForce BruteForce { get; set; }

        [TestInitialize]
        public void Initialize()
        {
            TicTacToeFactoryMock = new Mock<ITicTacToeFactory>();
            RandomMock = new Mock<IRandom>();
            GameMock = new Mock<IGame>();

            BruteForce = new BruteForce(TicTacToeFactoryMock.Object, RandomMock.Object);
        }

        [TestMethod]
        public void MakeMoveShouldThrowExceptionWhenGameIsOver()
        {
            //arrange
            GameMock
                .Setup(a => a.GameIsOver)
                .Returns(true);

            //act
            Action act = () => BruteForce.MakeMove(GameMock.Object);

            //assert
            act
                .ShouldThrow<GameException>()
                .WithMessage($"Unable to make a move because the game is over.");
        }

        [TestMethod]
        public void MakeMoveShouldMakeWinningMoveForXRandomly()
        {
            //arrange
            //setup a mock for the initial object..
            Move[] initialGameMoveArray = new Move[1];
            Mock<IGame> initialGame = new Mock<IGame>();
            initialGame
                .Setup(a => a.MoveHistory)
                .Returns(initialGameMoveArray);

            TicTacToeFactoryMock
                .Setup(a => a.NewGame(initialGameMoveArray))
                .Returns(GameMock.Object);

            //setup the game mock that will drive the rest of our choices
            List<Move> legalMoves = new List<Move>()
            {
                Move.Northern,
                Move.Center,
                Move.Southern
            };
            GameMock
                .Setup(a => a.GetLegalMoves())
                .Returns(legalMoves);

            GameMock
                .Setup(a => a.GameIsOver)
                .Returns(true);

            GameMock
                .Setup(a => a.CurrentPlayer)
                .Returns(Player.X);

            var callCount = 0;
            GameMock
                .Setup(a => a.GameState)
                .Returns(() =>
                {
                    callCount++;
                    switch (callCount-1)
                    {
                        case 0:
                            return GameState.OWin;
                        case 1:
                            return GameState.XWin;
                        default:
                            return GameState.Tie;
                    }
                });

            //setup up our random mock
            RandomMock
                .Setup(a => a.Next(1))
                .Returns(0);

            //act
            BruteForce.MakeMove(initialGame.Object);

            //assert

            //random was called
            RandomMock.Verify(a => a.Next(1), Times.Once());

            //the center move was made
            GameMock
                .Verify(a => a.Move(Move.Center));
        }

        [TestMethod]
        public void MakeMoveShouldMakeTieMoveForXRandomly()
        {
            //arrange
            //setup a mock for the initial object..
            Move[] initialGameMoveArray = new Move[1];
            Mock<IGame> initialGame = new Mock<IGame>();
            initialGame
                .Setup(a => a.MoveHistory)
                .Returns(initialGameMoveArray);

            TicTacToeFactoryMock
                .Setup(a => a.NewGame(initialGameMoveArray))
                .Returns(GameMock.Object);

            //setup the game mock that will drive the rest of our choices
            List<Move> legalMoves = new List<Move>()
            {
                Move.Northern,
                Move.Center,
                Move.Southern
            };
            GameMock
                .Setup(a => a.GetLegalMoves())
                .Returns(legalMoves);

            GameMock
                .Setup(a => a.GameIsOver)
                .Returns(true);

            GameMock
                .Setup(a => a.CurrentPlayer)
                .Returns(Player.X);

            var callCount = 0;
            GameMock
                .Setup(a => a.GameState)
                .Returns(() =>
                {
                    callCount++;
                    switch (callCount - 1)
                    {
                        case 0:
                            return GameState.Tie;
                        case 1:
                            return GameState.OWin;
                        default:
                            return GameState.Tie;
                    }
                });

            //setup up our random mock
            RandomMock
                .Setup(a => a.Next(2))
                .Returns(1);

            //act
            BruteForce.MakeMove(initialGame.Object);

            //assert

            //random was called
            RandomMock.Verify(a => a.Next(2), Times.Once());

            //the center move was made
            GameMock
                .Verify(a => a.Move(Move.Southern));
        }

        [TestMethod]
        public void MakeMoveShouldMakeLosingMoveForXRandomly()
        {
            //arrange
            //setup a mock for the initial object..
            Move[] initialGameMoveArray = new Move[1];
            Mock<IGame> initialGame = new Mock<IGame>();
            initialGame
                .Setup(a => a.MoveHistory)
                .Returns(initialGameMoveArray);

            TicTacToeFactoryMock
                .Setup(a => a.NewGame(initialGameMoveArray))
                .Returns(GameMock.Object);

            //setup the game mock that will drive the rest of our choices
            List<Move> legalMoves = new List<Move>()
            {
                Move.Northern,
                Move.Center,
                Move.Southern
            };
            GameMock
                .Setup(a => a.GetLegalMoves())
                .Returns(legalMoves);

            GameMock
                .Setup(a => a.GameIsOver)
                .Returns(true);

            GameMock
                .Setup(a => a.CurrentPlayer)
                .Returns(Player.X);

            var callCount = 0;
            GameMock
                .Setup(a => a.GameState)
                .Returns(() =>
                {
                    callCount++;
                    switch (callCount - 1)
                    {
                        case 0:
                            return GameState.OWin;
                        case 1:
                            return GameState.OWin;
                        default:
                            return GameState.OWin;
                    }
                });

            //setup up our random mock
            RandomMock
                .Setup(a => a.Next(3))
                .Returns(0);

            //act
            BruteForce.MakeMove(initialGame.Object);

            //assert

            //random was called
            RandomMock.Verify(a => a.Next(3), Times.Once());

            //the center move was made
            GameMock
                .Verify(a => a.Move(Move.Northern));
        }

        [TestMethod]
        public void MakeMoveShouldMakeWinningMoveForORandomly()
        {
            //arrange
            //setup a mock for the initial object..
            Move[] initialGameMoveArray = new Move[1];
            Mock<IGame> initialGame = new Mock<IGame>();
            initialGame
                .Setup(a => a.MoveHistory)
                .Returns(initialGameMoveArray);

            TicTacToeFactoryMock
                .Setup(a => a.NewGame(initialGameMoveArray))
                .Returns(GameMock.Object);

            //setup the game mock that will drive the rest of our choices
            List<Move> legalMoves = new List<Move>()
            {
                Move.Northern,
                Move.Center,
                Move.Southern
            };
            GameMock
                .Setup(a => a.GetLegalMoves())
                .Returns(legalMoves);

            GameMock
                .Setup(a => a.GameIsOver)
                .Returns(true);

            GameMock
                .Setup(a => a.CurrentPlayer)
                .Returns(Player.O);

            var callCount = 0;
            GameMock
                .Setup(a => a.GameState)
                .Returns(() =>
                {
                    callCount++;
                    switch (callCount - 1)
                    {
                        case 0:
                            return GameState.OWin;
                        case 1:
                            return GameState.XWin;
                        default:
                            return GameState.Tie;
                    }
                });

            //setup up our random mock
            RandomMock
                .Setup(a => a.Next(1))
                .Returns(0);

            //act
            BruteForce.MakeMove(initialGame.Object);

            //assert

            //random was called
            RandomMock.Verify(a => a.Next(1), Times.Once());

            //the center move was made
            GameMock
                .Verify(a => a.Move(Move.Northern));
        }

        [TestMethod]
        public void MakeMoveShouldMakeTieMoveForORandomly()
        {
            //arrange
            //setup a mock for the initial object..
            Move[] initialGameMoveArray = new Move[1];
            Mock<IGame> initialGame = new Mock<IGame>();
            initialGame
                .Setup(a => a.MoveHistory)
                .Returns(initialGameMoveArray);

            TicTacToeFactoryMock
                .Setup(a => a.NewGame(initialGameMoveArray))
                .Returns(GameMock.Object);

            //setup the game mock that will drive the rest of our choices
            List<Move> legalMoves = new List<Move>()
            {
                Move.Northern,
                Move.Center,
                Move.Southern
            };
            GameMock
                .Setup(a => a.GetLegalMoves())
                .Returns(legalMoves);

            GameMock
                .Setup(a => a.GameIsOver)
                .Returns(true);

            GameMock
                .Setup(a => a.CurrentPlayer)
                .Returns(Player.O);

            var callCount = 0;
            GameMock
                .Setup(a => a.GameState)
                .Returns(() =>
                {
                    callCount++;
                    switch (callCount - 1)
                    {
                        case 0:
                            return GameState.Tie;
                        case 1:
                            return GameState.XWin;
                        default:
                            return GameState.Tie;
                    }
                });

            //setup up our random mock
            RandomMock
                .Setup(a => a.Next(2))
                .Returns(1);

            //act
            BruteForce.MakeMove(initialGame.Object);

            //assert

            //random was called
            RandomMock.Verify(a => a.Next(2), Times.Once());

            //the center move was made
            GameMock
                .Verify(a => a.Move(Move.Southern));
        }

        [TestMethod]
        public void MakeMoveShouldMakeLosingMoveForORandomly()
        {
            //arrange
            //setup a mock for the initial object..
            Move[] initialGameMoveArray = new Move[1];
            Mock<IGame> initialGame = new Mock<IGame>();
            initialGame
                .Setup(a => a.MoveHistory)
                .Returns(initialGameMoveArray);

            TicTacToeFactoryMock
                .Setup(a => a.NewGame(initialGameMoveArray))
                .Returns(GameMock.Object);

            //setup the game mock that will drive the rest of our choices
            List<Move> legalMoves = new List<Move>()
            {
                Move.Northern,
                Move.Center,
                Move.Southern
            };
            GameMock
                .Setup(a => a.GetLegalMoves())
                .Returns(legalMoves);

            GameMock
                .Setup(a => a.GameIsOver)
                .Returns(true);

            GameMock
                .Setup(a => a.CurrentPlayer)
                .Returns(Player.O);

            var callCount = 0;
            GameMock
                .Setup(a => a.GameState)
                .Returns(() =>
                {
                    callCount++;
                    switch (callCount - 1)
                    {
                        case 0:
                            return GameState.XWin;
                        case 1:
                            return GameState.XWin;
                        default:
                            return GameState.XWin;
                    }
                });

            //setup up our random mock
            RandomMock
                .Setup(a => a.Next(3))
                .Returns(0);

            //act
            BruteForce.MakeMove(initialGame.Object);

            //assert

            //random was called
            RandomMock.Verify(a => a.Next(3), Times.Once());

            //the center move was made
            GameMock
                .Verify(a => a.Move(Move.Northern));
        }
    }
}
