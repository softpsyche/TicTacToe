using Arcesoft.TicTacToe.ConsoleApplication.Menus;
using Arcesoft.TicTacToe.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arcesoft.TicTacToe.ConsoleApplication
{
    public class UI
    {
        private readonly ITicTacToeFactory _gameFactory;
        private readonly IDatabaseBuilder _databaseBuilder;

        public UI(ITicTacToeFactory gameFactory, IDatabaseBuilder databaseBuilder)
        {
            _gameFactory = gameFactory;
            _databaseBuilder = databaseBuilder;
        }

        public void Run()
        {
            Console.WriteLine("Welcome to tic tac toe!");
            Console.WriteLine();

            RunMenu();
        }

        private void RunMenu()
        {
            var exit = false;

            while (exit == false)
            {
                switch (GetUserCommand(MainMenu.Menu))
                {
                    case MainMenu.PopulateGameDatabase:
                        Populate();
                        break;
                    case MainMenu.NewGameHuman:
                        RunGame();
                        break;
                    case MainMenu.NewGameAI:
                        RunGameVsAI();
                        break;
                    case UIMenu.Quit:
                        exit = true;
                        break;
                }
            }
        }

        private void Populate()
        {
            Console.WriteLine("Populating database, this may take a few minutes...");

            _databaseBuilder.PopulateMoveResponses();

            Console.WriteLine("Finished populating database");
        }

        private void RunGame()
        {
            var game = _gameFactory.NewGame();

            while (game.GameIsOver == false)
            {
                MakeHumanMove(game);
            }

            RenderGameOver(game);
        }

        private void RunGameVsAI()
        {
            var game = _gameFactory.NewGame();
            Player player = default(Player);
            IArtificialIntelligence ai = null;

            switch (GetUserCommand(PlayerSelectMenu.Menu))
            {
                case PlayerSelectMenu.PlayAsX:
                    player = Player.X;
                    break;
                case PlayerSelectMenu.PlayAsO:
                    player = Player.O;
                    break;
                case UIMenu.Quit:
                    return;
            }

            switch (GetUserCommand(AIMenu.Menu))
            {
                case AIMenu.BruteForce:
                    ai = _gameFactory.NewArtificialIntelligence(ArtificialIntelligenceTypes.BruteForce);
                    break;
                case AIMenu.OmniscientGod:
                    //because some people were whining about it...
                    if (_databaseBuilder.DatabaseIsEmpty())
                    {
                        Populate();
                    }

                    ai = _gameFactory.NewArtificialIntelligence(ArtificialIntelligenceTypes.OmniscientGod);
                    break;
                case UIMenu.Quit:
                    return;
            }

            while (game.GameIsOver == false)
            {
                if (player == game.CurrentPlayer)
                {
                    MakeHumanMove(game);
                }
                else
                {
                    ai.MakeMove(game);
                }    
            }

            RenderGameOver(game);
        }

        private void MakeHumanMove(IGame game)
        {
            Console.WriteLine($"{game.CurrentPlayer}s turn, make your next move...");

            RenderBoard(game);
            bool moveMade = false;

            //you should feel dirty seeing this. I felt dirty coding it. Alas, leftover from the old C++ days...
            while (!moveMade)
            {
                Move? move = null;

                switch (GetUserCommand(MoveMenu.Menu))
                {
                    case MoveMenu.SouthWest:
                        move = Move.SouthWest;
                        break;
                    case MoveMenu.Southern:
                        move = Move.Southern;
                        break;
                    case MoveMenu.SouthEast:
                        move = Move.SouthEast;
                        break;
                    case MoveMenu.Western:
                        move = Move.Western;
                        break;
                    case MoveMenu.Center:
                        move = Move.Center;
                        break;
                    case MoveMenu.Eastern:
                        move = Move.Eastern;
                        break;
                    case MoveMenu.NorthWest:
                        move = Move.NorthWest;
                        break;
                    case MoveMenu.Northern:
                        move = Move.Northern;
                        break;
                    case MoveMenu.NorthEast:
                        move = Move.NorthEast;
                        break;
                }

                if (game.IsMoveValid(move.Value))
                {
                    game.Move(move.Value);
                    moveMade = true;
                }
                else
                {
                    Console.WriteLine("The move you tried to make is not valid, space is already occupied!");
                }
                Console.WriteLine();
            }
        }

        private string GetUserCommand(UIMenu menu)
        {
            UICommand selectedCommand = null;

            while (selectedCommand == null)
            {
                Console.WriteLine(menu.Instructions);
                menu.Commands.ForEach(a => Console.WriteLine($"{a.CommandCode} - {a.CommandDescription}"));
                Console.WriteLine();

                var commandKey = Console.ReadKey();
                Console.WriteLine();
                var command = commandKey.KeyChar.ToString();

                selectedCommand = menu.Commands.FirstOrDefault(a => a.CommandCode.Equals(command.Trim(), StringComparison.InvariantCultureIgnoreCase));

                if (selectedCommand == null)
                {
                    Console.WriteLine($"Invalid command '{command}'.");
                    Console.WriteLine();
                }
            }

            return selectedCommand.CommandCode;
        }

        private void RenderBoard(IGame game)
        {
            Console.WriteLine();

            var board = game.GameBoardString;
            var padding = "     ";

            Console.WriteLine(padding + board.Substring(0, 3));
            Console.WriteLine(padding + board.Substring(3, 3));
            Console.WriteLine(padding + board.Substring(6, 3));

            Console.WriteLine();
        }

        private void RenderGameOver(IGame game)
        {
            Console.WriteLine();
            Console.WriteLine($"Game is over!");
            Console.WriteLine($"The result is: {game.GameState}");
            Console.WriteLine();
            RenderBoard(game);
            Console.WriteLine();
        }
    }







}
