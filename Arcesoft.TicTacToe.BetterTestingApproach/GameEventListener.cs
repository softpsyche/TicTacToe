using Arcesoft.TicTacToe.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arcesoft.TicTacToe.BetterTestingApproach
{
    public class GameEventListener
    {
        private List<KeyValuePair<string, EventArgs>> EventList = new List<KeyValuePair<string, EventArgs>>();

        public IEnumerable<EventArgs> GameResetEventsReceived => EventList.Where(a => a.Key.Equals(nameof(IGame.GameReset))).Select(a => a.Value);
        public IEnumerable<EventArgs> GameOverEventsReceived => EventList.Where(a => a.Key.Equals(nameof(IGame.GameOver))).Select(a => a.Value);
        public IEnumerable<GameStateChangedEventArgs> GameStateChangedEventsReceived => EventList.Where(a => a.Key.Equals(nameof(IGame.GameStateChanged))).Select(a => a.Value as GameStateChangedEventArgs);

        public void Register(IGame game)
        {
            game.GameStateChanged += Game_GameStateChanged;
            game.GameOver += Game_GameOver;
            game.GameReset += Game_GameReset;
        }

        private void Game_GameReset(object sender, EventArgs e)
        {
            EventList.Add(new KeyValuePair<string, EventArgs>(nameof(IGame.GameReset), e));
        }

        private void Game_GameOver(object sender, EventArgs e)
        {
            EventList.Add(new KeyValuePair<string, EventArgs>(nameof(IGame.GameOver), e));
        }

        private void Game_GameStateChanged(object sender, Entities.GameStateChangedEventArgs e)
        {
            EventList.Add(new KeyValuePair<string, EventArgs>(nameof(IGame.GameStateChanged), e));
        }
    }
}
