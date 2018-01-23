using Arcesoft.TicTacToe.ArtificialIntelligence;
using Arcesoft.TicTacToe.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arcesoft.TicTacToe.NaiveTestingApproach.Data
{
    [TestClass]
    internal class MoveDatabaseTests
    {
        private Mock<IMoveEvaluator> MoveEvaluatorMock { get; set; }
        private Mock<IFileAccess> FileAccessMock { get; set; }


        [TestInitialize]
        public void Initialize()
        {
            MoveEvaluatorMock = new Mock<IMoveEvaluator>();
        }

        [TestMethod]
        public void ShouldCreateMovesDataTable()
        {
            //arrange


            //act


            //assert
        }

        private void GivenNoDatabaseFileExists()
        {

        }
    }
}
