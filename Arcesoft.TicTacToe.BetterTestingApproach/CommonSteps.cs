﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using Moq;
using FluentAssertions;
using SimpleInjector;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Arcesoft.TicTacToe.BetterTestingApproach
{
    [Binding]
    public class CommonSteps : Steps
    {
        [Given(@"I expect an exception to be thrown")]
        public void GivenIExpectAnExceptionToBeThrown()
        {
            ExpectingException = true;
        }


        [Given(@"I have a container")]
        public void GivenIHaveAContainer()
        {
            var container = new Container();

            new DependencyInjection.Binder().BindDependencies(container);

            container.Verify();

            Container = container;
        }

        [Given(@"I have a tictactoe factory")]
        public void GivenIHaveATictactoeFactory()
        {
            TicTacToeFactory = Container.GetInstance<ITicTacToeFactory>();
        }


        [Given(@"I start a new game")]
        [When(@"I start a new game")]
        public void GivenIStartANewGame()
        {
            Invoke(() => Game = TicTacToeFactory.NewGame());
        }

        [Given(@"I start a new game with the following moves")]
        [When(@"I start a new game with the following moves")]
        public void GivenIStartANewGameWithTheFollowingMoves(Table table)
        {
            Invoke(() =>
            {
                var moves = ToMoves(table);

                Game = TicTacToeFactory.NewGame(moves);
            });
        }

        [Given(@"I start a new game with the following moves '(.*)'")]
        [When(@"I start a new game with the following moves '(.*)'")]
        public void WhenIStartANewGameWithTheFollowingMoves(string movesString)
        {
            Invoke(() =>
            {
                var moves = ToMoves(movesString);

                Game = TicTacToeFactory.NewGame(moves);
            });
        }

        [Then(@"I expect the following GameException to be thrown")]
        public void ThenIExpectTheFollowingGameExceptionToBeThrown(Table table)
        {
            Exception.Should().BeOfType<GameException>();

            table.CompareToInstance(Exception);
        }


        [Given(@"Todo")]
        [When(@"Todo")]
        [Then(@"Todo")]
        public void GivenTodo()
        {
            Assert.Inconclusive();
        }

    }
}