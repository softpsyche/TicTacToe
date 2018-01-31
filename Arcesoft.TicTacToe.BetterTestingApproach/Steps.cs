using Arcesoft.TicTacToe.Data;
using Arcesoft.TicTacToe.Database;
using Arcesoft.TicTacToe.Entities;
using Arcesoft.TicTacToe.GameImplementation;
using FluentAssertions;
using Moq;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace Arcesoft.TicTacToe.BetterTestingApproach
{
    internal abstract class Steps
    {
        protected ScenarioContext CurrentContext => ScenarioContext.Current;

        protected Boolean ExpectingException
        {
            get
            {
                return GetScenarioContextItemOrDefault<Boolean>(nameof(ExpectingException));
            }
            set
            {
                CurrentContext.Set(value, nameof(ExpectingException));
            }
        }

        protected Exception Exception
        {
            get
            {
                return GetScenarioContextItemOrDefault<Exception>();
            }
            set
            {
                CurrentContext.Set(value);
            }
        }

        protected Container Container
        {
            get
            {
                return GetScenarioContextItemOrDefault<Container>();
            }
            set
            {
                CurrentContext.Set(value);
            }
        }

        protected ITicTacToeFactory TicTacToeFactory
        {
            get
            {
                return GetScenarioContextItemOrDefault<ITicTacToeFactory>();
            }
            set
            {
                CurrentContext.Set(value);
            }
        }

        protected IGame Game
        {
            get
            {
                return GetScenarioContextItemOrDefault<IGame>();
            }
            set
            {
                CurrentContext.Set(value);
            }
        }

        protected IArtificialIntelligence ArtificialIntelligence
        {
            get
            {
                return GetScenarioContextItemOrDefault<IArtificialIntelligence>(nameof(ArtificialIntelligence));
            }
            set
            {
                CurrentContext.Set(value, nameof(ArtificialIntelligence));
            }
        }

        protected IEnumerable<MoveResult> MoveResults
        {
            get
            {
                return GetScenarioContextItemOrDefault<IEnumerable<MoveResult>>(nameof(MoveResults));
            }
            set
            {
                CurrentContext.Set(value, nameof(MoveResults));
            }
        }

        protected Mock<IDatabaseBuilder> MockMoveDatabase
        {
            get
            {
                return GetScenarioContextItemOrDefault<Mock<IDatabaseBuilder>>(nameof(MockMoveDatabase));
            }
            set
            {
                CurrentContext.Set(value, nameof(MockMoveDatabase));
            }
        }

        protected GameEventListener GameEventListener
        {
            get
            {
                return GetScenarioContextItemOrDefault<GameEventListener>();
            }
            set
            {
                CurrentContext.Set(value);
            }
        }

        private T GetScenarioContextItemOrDefault<T>(string key = null)
        {
            var keyName = key ?? typeof(T).FullName;

            if (CurrentContext.ContainsKey(keyName))
            {
                return CurrentContext.Get<T>(keyName);
            }
            else
            {
                return default(T);
            }
        }

        protected void Invoke(Action action)
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                if (ExpectingException)
                {
                    Exception = ex;
                }
                else
                {
                    throw;
                }
            }
        }

        protected string ToBoardString(Table table)
        {
            StringBuilder sb = new StringBuilder();

            table.Rows.Count.Should().Be(3, "The table should have three rows");
            table.Rows[0].Count.Should().Be(3, "Each row in the table should have 3 entries");

            table.Rows.ForEach(a => sb.Append(string.Concat(ToSquareValues(a.Values))));

            return sb.ToString();
        }
        private IEnumerable<String> ToSquareValues(IEnumerable<string> values)
        {
            return values.Select(a => ToSquareValue(a));
        }
        private string ToSquareValue(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return Board.SquareEmptyString;
            }
            else if (Board.SquareXString.Equals(value, StringComparison.InvariantCultureIgnoreCase))
            {
                return Board.SquareXString;
            }
            else if (Board.SquareOString.Equals(value, StringComparison.InvariantCultureIgnoreCase))
            {
                return Board.SquareOString;
            }
            else
            {
                throw new InvalidOperationException($"Unable to create square value for string because the value '{value}' is not a valid tic tac toe board value");
            }
        }

        protected IEnumerable<Move> ToMoves(Table table)
        {
            return table.CreateSet((a) => a[0].ToEnumeration<Move>());
        }
        protected IEnumerable<Move> ToMoves(string commaDelimitedMoves)
        {
            return commaDelimitedMoves
                .Split(',')
                .Select(a => a.ToEnumeration<Move>());
        }
    }
}
