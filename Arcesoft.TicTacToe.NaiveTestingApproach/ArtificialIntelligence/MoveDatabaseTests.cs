using Arcesoft.TicTacToe.ArtificialIntelligence;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arcesoft.TicTacToe.NaiveTestingApproach.ArtificialIntelligence
{
    [TestClass]
    internal class MoveDatabaseTests
    {
        private Mock<IMoveEvaluator> MoveEvaluatorMock { get; set; }
        //private Movesd


        [TestInitialize]
        public void Initialize()
        {
            MoveEvaluatorMock = new Mock<IMoveEvaluator>();

        }
    }
}
