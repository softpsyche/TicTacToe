using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Arcesoft.TicTacToe.RandomNumberGeneration;
using Arcesoft.TicTacToe.ArtificialIntelligence;
using Arcesoft.TicTacToe.Entities;

namespace Arcesoft.TicTacToe.CommonTestingApproach.ArtificialIntelligence
{
    [TestClass]
    internal class MoveEvaluatorTests
    {
        private Mock<ITicTacToeFactory> TicTacToeFactoryMock { get; set; }
        private Mock<IRandom> RandomMock { get; set; }
        private Mock<IGame> GameMock { get; set; }
        private MoveEvaluator MoveEvaluator { get; set; }


        [TestInitialize]
        public void Initialize()
        {
            TicTacToeFactoryMock = new Mock<ITicTacToeFactory>();
            RandomMock = new Mock<IRandom>();
            GameMock = new Mock<IGame>();

            MoveEvaluator = new MoveEvaluator(TicTacToeFactoryMock.Object, RandomMock.Object);
        }

        [TestMethod]
        public void ShouldCalculateBestMove()
        {
            //arrange
            GameMock
                .Setup(a => a.GetLegalMoves())
                .Returns(new[] {
                    Move.Center,
                    Move.Eastern
                    });


            //GameMock
            //    .Setup(a => a.Move(It.IsAny<Move>()))
            //    .Callback<Move>((a) =>
            //    {
            //        switch (a)
            //        {
            //            case Move.Center:
            //                GameMock.set
            //                break;

            //            case Move.Eastern:
            //                break;
            //        }
            //    });

            //        //act


            //        //assert
            //    
        }
    }
}
