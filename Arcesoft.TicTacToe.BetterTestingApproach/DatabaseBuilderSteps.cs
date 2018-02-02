using Arcesoft.TicTacToe.Database;
using Arcesoft.TicTacToe.GameImplementation;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace Arcesoft.TicTacToe.BetterTestingApproach
{
    [Binding]
    class DatabaseBuilderSteps : Steps
    {
        [Given(@"I have a database builder")]
        public void GivenIHaveADatabaseBuilder()
        {
            Invoke(() => DatabaseBuilder = TicTacToeFactory.NewDatabaseBuilder());
        }

        [When(@"I populate move responses for the current game")]
        public void WhenIPopulateMoveResponsesForTheCurrentGame()
        {
            Invoke(() => DatabaseBuilder.PopulateMoveResponses(Game));
        }

        [Then(@"I expect the ILiteDatabase\.DeleteCollection to have been called '(.*)' times")]
        public void ThenIExpectTheILiteDatabase_DeleteAllMovesResponsesToHaveBeenCalledTimes(int times)
        {
            MockLiteDatabase
                .Verify(a => a.DropCollection<MoveResponseRecord>(), Times.Once());
        }

        [Then(@"I expect the ILiteDatabase\.EnsureIndex to have been called with the following '(.*)'")]
        public void ThenIExpectTheILiteDatabase_EnsureIndexToHaveBeenCalledWithTheFollowing(string p0)
        {
            //wow, this one hurt my head a bit...
            Func<Expression<Func<MoveResponseRecord, string>>, bool> validatingFunkiness = (expression) =>
            {
                return p0.Equals(((MemberExpression)expression.Body).Member.Name);
            };

            MockLiteDatabase
                .Verify(a => a.EnsureIndex(It.Is<Expression<Func<MoveResponseRecord, string>>>(b=> validatingFunkiness(b)), false), Times.Once());
        }

        [Then(@"I expect the ILiteDatabase\.InsertBulk to have been called with the following move response records")]
        public void ThenIExpectTheILiteDatabase_InsertBulkToHaveBeenCalledWithTheFollowingMoveResponseRecords(Table table)
        {
            Func<IEnumerable<MoveResponseRecord>,bool> verifyingFunc = (records) =>
            {
                table.CompareToSet(records);

                return true;
            };

            MockLiteDatabase
                .Verify(a => a.InsertBulk(It.Is<IEnumerable<MoveResponseRecord>>(b => verifyingFunc(b))), Times.Once());
        }



    }
}
