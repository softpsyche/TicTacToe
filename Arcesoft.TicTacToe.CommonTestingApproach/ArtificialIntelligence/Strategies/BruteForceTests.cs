using Arcesoft.TicTacToe.ArtificialIntelligence.Strategies;
using Arcesoft.TicTacToe.Entities;
using Arcesoft.TicTacToe.GameImplementation;
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
        private Mock<IBestMoveSelector> BestMoveSelectorMock { get; set; }
        private Mock<IGame> GameMock { get; set; }

        //SUT
        private BruteForce BruteForce { get; set; }

        [TestInitialize]
        public void Initialize()
        {
            TicTacToeFactoryMock = new Mock<ITicTacToeFactory>();
            BestMoveSelectorMock = new Mock<IBestMoveSelector>();
            GameMock = new Mock<IGame>();

            BruteForce = new BruteForce(
                TicTacToeFactoryMock.Object,
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
            Action act = () => BruteForce.MakeMove(GameMock.Object);

            //assert
            act
                .ShouldThrow<GameException>()
                .WithMessage($"Unable to make a move because the game is over.");
        }

        [TestMethod]
        public void MakeMoveShouldMakeCopyOfGame()
        {
            //arrange
            //setup a mock for the initial object..
            Move[] initialGameMoveArray = new Move[1];
            Mock<IGame> initialGame = new Mock<IGame>(MockBehavior.Strict);
            initialGame
                .Setup(a => a.MoveHistory)
                .Returns(initialGameMoveArray);

            initialGame
                .Setup(a => a.GameIsOver)
                .Returns(false);

            initialGame
                .Setup(a => a.Move(It.IsAny<Move>()));

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

            BestMoveSelectorMock
                .Setup(a => a.FindRandomBestMoveResultForPlayerOrDefault(It.IsAny<IEnumerable<IMoveResult>>(), Player.O))
                .Returns(new MoveResult(Move.Center, GameState.XWin));

            //act
            BruteForce.MakeMove(initialGame.Object);

            //assert
            TicTacToeFactoryMock.Verify(a => a.NewGame(initialGameMoveArray), Times.Once());
        }

        [TestMethod]
        public void MakeMoveShouldMakeMoveRandomly()
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
                            return GameState.XWin;
                        default:
                            return GameState.Tie;
                    }
                });

            BestMoveSelectorMock
                .Setup(a => a.FindRandomBestMoveResultForPlayerOrDefault(It.IsAny<IEnumerable<IMoveResult>>(), Player.X))
                .Returns(new MoveResult(Move.Center, GameState.XWin));

            //act
            BruteForce.MakeMove(initialGame.Object);

            //assert

            //the center move was made
            GameMock
                .Verify(a => a.Move(Move.Center));
        }

        [TestMethod]
        public void MakeMoveShouldMakeMoveNonRandomly()
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
                            return GameState.XWin;
                        default:
                            return GameState.Tie;
                    }
                });

            BestMoveSelectorMock
                .Setup(a => a.FindBestMoveResultsForPlayer(It.IsAny<IEnumerable<IMoveResult>>(), Player.X))
                .Returns(new[] { new MoveResult(Move.Center, GameState.XWin) });

            //act
            BruteForce.MakeMove(initialGame.Object, false);

            //assert

            //the center move was made
            GameMock
                .Verify(a => a.Move(Move.Center));
        }

        [TestMethod]
        public void FindMoveResultsShouldFindMovesRecursively()
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

            //store the last move made...
            //we need this to test correctly
            Move? lastMoveMade = null;
            GameMock
                .Setup(a => a.Move(It.IsAny<Move>()))
                .Callback<Move>(a => lastMoveMade = a);

            var firstLegalMoves = new Move[]
                {
                    Move.Northern,
                    Move.Southern
                };
            var secondLegalMoves = new Move[]
                {
                    Move.NorthEast,
                    Move.NorthWest,
                    Move.Western,
                    Move.Center
                };
            var thirdLegalMoves = new Move[]
                {
                    Move.Eastern,
                    Move.SouthEast,
                    Move.SouthWest
                };

            //setup the game mock that will drive the rest of our choices
            GameMock
                .Setup(a => a.GetLegalMoves())
                .Returns(() =>
                {
                    if (lastMoveMade.HasValue == false)
                    {
                        return firstLegalMoves;
                    }
                    else if (lastMoveMade.Value == Move.Northern)
                    {
                        return secondLegalMoves;
                    }
                    else if (lastMoveMade.Value == Move.Southern)
                    {
                        return thirdLegalMoves;
                    }

                    throw new Exception("test borked");
                });

            //the game is in play for the first two moves...
            GameMock
                .Setup(a => a.GameIsOver)
                .Returns(() =>
                {
                    switch (lastMoveMade)
                    {
                        case Move.Northern:
                        case Move.Southern:
                            return false;
                        default:
                            return true;
                    }
                });

            GameMock
                .Setup(a => a.CurrentPlayer)
                .Returns(() =>
                {
                    return Player.X;
                });

            GameMock
                .Setup(a => a.GameState)
                .Returns(() =>
                {
                    switch (lastMoveMade)
                    {
                        case Move.NorthEast:
                            return GameState.Tie;
                        case Move.NorthWest:
                            return GameState.OWin;
                        case Move.Western:
                            return GameState.Tie;
                        case Move.Center:
                            return GameState.OWin;
                        case Move.Eastern:
                            return GameState.XWin;
                        case Move.SouthEast:
                            return GameState.OWin;
                        case Move.SouthWest:
                            return GameState.Tie;
                    }

                    throw new Exception();
                });

            BestMoveSelectorMock
                .Setup(a => a.FindBestMoveResultsForPlayer(It.IsAny<IEnumerable<IMoveResult>>(), It.IsAny<Player>()))
                .Returns<IEnumerable<IMoveResult>,Player>((moves, player) =>
                {
                    if (moves.Count() == 4)
                    {
                        return moves.Where(a => a.GameStateAfterMove == GameState.Tie).ToList();
                    }
                    else if (moves.Count() == 3)
                    {
                        return moves.Where(a => a.GameStateAfterMove == GameState.XWin).ToList();
                    }

                    throw new Exception("Test got weird yo");
                });

            //act
            var moveResults = BruteForce.FindMoveResults(initialGame.Object);

            //assert
            var expectedMoveResults = new MoveResult[]
                {
                    new MoveResult(Move.Northern, GameState.Tie),
                    new MoveResult(Move.Southern, GameState.XWin)
                };

            moveResults.ShouldAllBeEquivalentTo(expectedMoveResults);
        }

        [TestMethod]
        public void FindMoveResultsShouldMakeCopyOfGame()
        {
            //arrange
            //setup a mock for the initial object..
            Move[] initialGameMoveArray = new Move[1];
            Mock<IGame> initialGame = new Mock<IGame>(MockBehavior.Strict);
            initialGame
                .Setup(a => a.MoveHistory)
                .Returns(initialGameMoveArray);

            TicTacToeFactoryMock
                .Setup(a => a.NewGame(initialGameMoveArray))
                .Returns(GameMock.Object);

            //store the last move made...
            //we need this to test correctly
            Move? lastMoveMade = null;
            GameMock
                .Setup(a => a.Move(It.IsAny<Move>()))
                .Callback<Move>(a => lastMoveMade = a);

            var firstLegalMoves = new Move[]
                {
                    Move.Northern,
                    Move.Southern
                };
            var secondLegalMoves = new Move[]
                {
                    Move.NorthEast,
                    Move.NorthWest,
                    Move.Western,
                    Move.Center
                };
            var thirdLegalMoves = new Move[]
                {
                    Move.Eastern,
                    Move.SouthEast,
                    Move.SouthWest
                };

            //setup the game mock that will drive the rest of our choices
            GameMock
                .Setup(a => a.GetLegalMoves())
                .Returns(() =>
                {
                    if (lastMoveMade.HasValue == false)
                    {
                        return firstLegalMoves;
                    }
                    else if (lastMoveMade.Value == Move.Northern)
                    {
                        return secondLegalMoves;
                    }
                    else if (lastMoveMade.Value == Move.Southern)
                    {
                        return thirdLegalMoves;
                    }

                    throw new Exception("test borked");
                });

            //the game is in play for the first two moves...
            GameMock
                .Setup(a => a.GameIsOver)
                .Returns(() =>
                {
                    switch (lastMoveMade)
                    {
                        case Move.Northern:
                        case Move.Southern:
                            return false;
                        default:
                            return true;
                    }
                });

            GameMock
                .Setup(a => a.CurrentPlayer)
                .Returns(() =>
                {
                    return Player.X;
                });

            GameMock
                .Setup(a => a.GameState)
                .Returns(() =>
                {
                    switch (lastMoveMade)
                    {
                        case Move.NorthEast:
                            return GameState.Tie;
                        case Move.NorthWest:
                            return GameState.OWin;
                        case Move.Western:
                            return GameState.Tie;
                        case Move.Center:
                            return GameState.OWin;
                        case Move.Eastern:
                            return GameState.XWin;
                        case Move.SouthEast:
                            return GameState.OWin;
                        case Move.SouthWest:
                            return GameState.Tie;
                    }

                    throw new Exception();
                });

            BestMoveSelectorMock
                .Setup(a => a.FindBestMoveResultsForPlayer(It.IsAny<IEnumerable<IMoveResult>>(), It.IsAny<Player>()))
                .Returns<IEnumerable<IMoveResult>, Player>((moves, player) =>
                {
                    if (moves.Count() == 4)
                    {
                        return moves.Where(a => a.GameStateAfterMove == GameState.Tie).ToList();
                    }
                    else if (moves.Count() == 3)
                    {
                        return moves.Where(a => a.GameStateAfterMove == GameState.XWin).ToList();
                    }

                    throw new Exception("Test got weird yo");
                });

            //act
            var moveResults = BruteForce.FindMoveResults(initialGame.Object);

            //assert
            //at this point, this is all we need...
        }
    }
}
