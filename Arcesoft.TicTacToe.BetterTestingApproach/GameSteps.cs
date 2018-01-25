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

namespace Arcesoft.TicTacToe.BetterTestingApproach
{
    [Binding]
    public class GameSteps : Steps
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


    }
}
