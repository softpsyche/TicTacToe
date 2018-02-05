using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Arcesoft.TicTacToe.GameImplementation;
using Arcesoft.TicTacToe.Entities;
using Arcesoft.TicTacToe.ArtificialIntelligence.Strategies;

namespace Arcesoft.TicTacToe.CommonTestingApproach.GameImplementation
{
    [TestClass]
    [TestCategory("CommonTestingApproach")]
    public class TicTacToeFactoryTests
    {
        private Mock<IServiceProvider> ServiceProviderMock { get; set; }
        private Mock<IGame> GameMock { get; set; }

        private TicTacToeFactory TicTacToeFactory { get; set; }

        [TestInitialize]
        public void Initialize()
        {
            ServiceProviderMock = new Mock<IServiceProvider>();
            GameMock = new Mock<IGame>();

            TicTacToeFactory = new TicTacToeFactory(ServiceProviderMock.Object);
        }

        [TestMethod]
        public void NewGameShouldCreateNewGame()
        {
            //arrange
            ServiceProviderMock
                .Setup(a => a.GetService(typeof(IGame)))
                .Returns(GameMock.Object);

            //act
            var newGame = TicTacToeFactory.NewGame();

            //assert
            newGame.Should().BeSameAs(GameMock.Object);
        }

        [TestMethod]
        public void NewGameShouldCreateNewGameFromMoves()
        {
            //arrange
            ServiceProviderMock
                .Setup(a => a.GetService(typeof(IGame)))
                .Returns(GameMock.Object);

            var movesToMake = new[]{
                Move.Center,
                Move.Western,
                Move.Eastern
                };

            GameMock
                .Setup(a => a.IsMoveValid(It.Is<Move>(b => movesToMake.Contains(b))))
                .Returns(true);

            //act
            var newGame = TicTacToeFactory.NewGame(movesToMake);

            //assert
            newGame.Should().BeSameAs(GameMock.Object);

            GameMock
                .Verify(a => a.Move(Move.Center), Times.Once());
            GameMock
                .Verify(a => a.Move(Move.Western), Times.Once());
            GameMock
                .Verify(a => a.Move(Move.Eastern), Times.Once());
        }

        [TestMethod]
        public void NewGameShouldThrowWhenMoveIsInvalid()
        {
            //arrange
            ServiceProviderMock
                .Setup(a => a.GetService(typeof(IGame)))
                .Returns(GameMock.Object);

            var movesToMake = new[]{
                Move.Center
                };

            GameMock
                .Setup(a => a.IsMoveValid(Move.Center))
                .Returns(false);

            //act
            Action act = () => TicTacToeFactory.NewGame(movesToMake);

            //assert
            act
                .ShouldThrow<GameException>()
                .WithMessage("Invalid move passed in. Cannot create game from moves.");
        }

        [TestMethod]
        public void NewArtificialIntelligenceShouldThrowWhenTypeNotFound()
        {
            //arrange

            //act
            Action act = () => TicTacToeFactory.NewArtificialIntelligence("Giggidy");

            //assert
            act
                .ShouldThrow<ArgumentOutOfRangeException>()
                .WithMessage($"Unable to create AI for type 'Giggidy'. No implementation found for this type.\r\nParameter name: type");
        }

        [TestMethod]
        public void NewArtificialIntelligenceShouldCreateBruteForceAI()
        {
            //arrange

            //act
            var newAi = TicTacToeFactory.NewArtificialIntelligence(ArtificialIntelligenceTypes.BruteForce);

            //assert
            ServiceProviderMock
                .Verify(a => a.GetService(typeof(BruteForce)), Times.Once());
        }

        [TestMethod]
        public void NewArtificialIntelligenceShouldCreateOmniscientGodAI()
        {
            //arrange

            //act
            var newAi = TicTacToeFactory.NewArtificialIntelligence(ArtificialIntelligenceTypes.OmniscientGod);

            //assert
            ServiceProviderMock
                .Verify(a => a.GetService(typeof(OmniscientGod)), Times.Once());
        }
    }
}
