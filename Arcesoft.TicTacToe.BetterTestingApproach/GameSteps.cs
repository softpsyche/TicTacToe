using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using Moq;
using FluentAssertions;
using Arcesoft.TicTacToe.Entities;
using Arcesoft.TicTacToe.Data;
using Arcesoft.TicTacToe.Database;
using System.Linq.Expressions;

namespace Arcesoft.TicTacToe.BetterTestingApproach
{
    [Binding]
    internal class GameSteps : Steps
    {
        [Then(@"The current player should be '(.*)'")]
        public void ThenTheCurrentPlayerShouldBe(Player player)
        {
            Game.CurrentPlayer.Should().Be(player);
        }

        [Then(@"The move history should be empty")]
        public void ThenTheMoveHistoryShouldBeEmpty()
        {
            Game.MoveHistory.Should().BeEmpty();
        }

        [Then(@"The game state should be '(.*)'")]
        public void ThenTheGameStateShouldBe(GameState gameState)
        {
            Game.GameState.Should().Be(gameState);
        }

        [Then(@"The game should not be over")]
        public void ThenTheGameShouldNotBeOver()
        {
            Game.GameIsOver.Should().BeFalse();
        }

        [Then(@"The total moves made should be '(.*)'")]
        public void ThenTheTotalMovesMadeShouldBe(int totalMovesMade)
        {
            Game.TotalMovesMade.Should().Be(totalMovesMade);
        }

        [Then(@"The game board should look like")]
        public void ThenTheGameBoardShouldLookLike(Table table)
        {
            Game.GameBoardString.Should().Be(ToBoardString(table));
        }


        [Then(@"The available legal moves should be")]
        public void ThenTheAvailableLegalMovesShouldBe(Table table)
        {
            var moves = ToMoves(table);

            Game.GetLegalMoves().ShouldBeEquivalentTo(moves);
        }

        [Then(@"The move history should be")]
        public void ThenTheMoveHistoryShouldBe(Table table)
        {
            Game.MoveHistory.ShouldBeEquivalentTo(ToMoves(table));
        }

        [Given(@"I make the move '(.*)'")]
        [When(@"I make the move '(.*)'")]
        public void WhenIMakeTheMove(Move move)
        {
            Game.Move(move);
        }

        [Then(@"The game should be over")]
        public void ThenTheGameShouldBeOver()
        {
            Game.GameIsOver.Should().BeTrue();
        }

        [Then(@"The available legal moves should be empty")]
        public void ThenTheAvailableLegalMovesShouldBeEmpty()
        {
            Game.GetLegalMoves().Should().BeEmpty();
        }

        [Then(@"The move history should be '(.*)'")]
        public void ThenTheMoveHistoryShouldBe(string moveHistory)
        {
            Game.MoveHistory.ShouldBeEquivalentTo(ToMoves(moveHistory));
        }

        [Then(@"The game board should be '(.*)'")]
        public void ThenTheGameBoardShouldBe(string gameBoard)
        {
            Game.GameBoardString.Should().Be(gameBoard);
        }

        [When(@"I reset the game")]
        public void WhenIResetTheGame()
        {
            Game.Reset();
        }

        [Given(@"I start listening to all game events")]
        public void GivenIStartListeningToAllGameEvents()
        {
            GameEventListener = new GameEventListener();
            GameEventListener.Register(Game);
        }

        [Then(@"The following game state changed events are raised")]
        public void ThenTheFollowingGameStateChangedEventsAreRaised(Table table)
        {
            table.CompareToSet(GameEventListener.GameStateChangedEventsReceived);
        }

        [Then(@"The following number of GameOver events are raised: '(.*)'")]
        public void ThenTheFollowingNumberOfGameOverEventsAreRaised(int p0)
        {
            GameEventListener.GameOverEventsReceived.Count().Should().Be(p0);
        }

        [Then(@"The following number of GameReset events are raised: '(.*)'")]
        public void ThenTheFollowingNumberOfGameResetEventsAreRaised(int p0)
        {
            GameEventListener.GameResetEventsReceived.Count().Should().Be(p0);
        }

        [When(@"I undo last move")]
        public void WhenIUndoLastMove()
        {
            Game.UndoLastMove();
        }

        [Given(@"I have the artificial intelligence '(.*)'")]
        public void GivenIHaveTheArtificialIntelligence(string ai)
        {
            ArtificialIntelligence = TicTacToeFactory.NewArtificialIntelligence(ai);
        }

        [When(@"I have the AI find move results for the current game")]
        public void WhenIHaveTheAIFindMoveResultsForTheCurrentGame()
        {
            Invoke(() => MoveResults = ArtificialIntelligence.FindMoveResults(Game));
        }

        [Then(@"The move results should contain the following")]
        public void ThenTheMoveResultsShouldContainTheFollowing(Table table)
        {
            table.CompareToSet(MoveResults);
        }

        [Then(@"The last move made should be one of the following moves '(.*)'")]
        public void ThenTheLastMoveMadeShouldBeOneOfTheFollowingMoves(string moves)
        {
            ToMoves(moves).Should().Contain(Game.MoveHistory.Last());
        }

        [Given(@"I have the AI make the next random best move")]
        [When(@"I have the AI make the next random best move")]
        public void WhenIHaveTheAIMakeTheNextRandomBestMove()
        {
            Invoke(() => ArtificialIntelligence.MakeMove(Game, true));
        }

        [Given(@"I have the AI make the next first best move")]
        [When(@"I have the AI make the next first best move")]
        public void WhenIHaveTheAIMakeTheNextFirstBestMove()
        {
            Invoke(() => ArtificialIntelligence.MakeMove(Game, false));
        }


        [Then(@"The move results should be empty")]
        public void ThenTheMoveResultsShouldBeEmpty()
        {
            MoveResults.Should().BeEmpty();
        }

        [Given(@"I mock the ILiteDatabase")]
        public void GivenIMockTheILiteDatabase()
        {
            MockLiteDatabaseFactory = new Mock<ILiteDatabaseFactory>();
            MockLiteDatabase = new Mock<ILiteDatabase>();

            Container.RegisterSingleton(MockLiteDatabaseFactory.Object);

            MockLiteDatabaseFactory
                .Setup(a => a.OpenOrCreate(MoveResponseRepository.MoveRepositoryName))
                .Returns(MockLiteDatabase.Object);
        }


        [Given(@"I setup the mock ILiteDatabase.FindByIndex method to return the following MoveResponses")]
        public void GivenISetupTheMockILiteDatabaseToFindTheFollowingMoveResponses(Table table)
        {
            var moveResponseRecords = table.CreateSet<MoveResponseRecord>();

            Func<Expression<Func<MoveResponseRecord, bool>>, bool> verifyingFunk = (expression) =>
             {
                 return true;
            };

            //this can be easier make it easire please
            MockLiteDatabase
                .Setup(a => a.FindByIndex(It.Is<Expression<Func<MoveResponseRecord, bool>>>(b => verifyingFunk(b))))
                .Returns(moveResponseRecords);
        }
    }
}
