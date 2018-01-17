using Arcesoft.TicTacToe.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arcesoft.TicTacToe.ConsoleApplication
{
    public class TicTacToe
    {
        private readonly IGameFactory _gameFactory;
        private static class Commands
        {
            public const string NewGame = "n";
            public const string Quit = "q";
        }

        public TicTacToe(IGameFactory gameFactory)
        {
            _gameFactory = gameFactory;
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
                Console.WriteLine("Please select one of the following options:");
                Console.WriteLine($"{Commands.NewGame} - starts a new game");
                Console.WriteLine($"{Commands.Quit} - exit tic tac toe");
                Console.WriteLine();

                var command = Console.ReadLine();

                switch (command.Trim().ToLowerInvariant())
                {
                    case Commands.NewGame:
                        NewGame();
                        break;
                    case Commands.Quit:
                        exit = true;
                        break;
                    default:
                        Console.WriteLine($"Invalid command '{command}'.");
                        Console.WriteLine();
                        break;
                }
            }
        }

        private void NewGame()
        {
            var game = _gameFactory.NewGame();

            while (game.GameIsOver == false)
            {
                Console.WriteLine($"{game.CurrentPlayer}s turn, make your next move...");

                RenderBoard(game);

                var command = Console.ReadLine();
                Move move;

                switch (command.Trim().ToLowerInvariant())
                {
                    case "1":
                        move = Move.SouthWest;
                        break;
                    case "2":
                        move = Move.Southern;
                        break;
                    case "3":
                        move = Move.SouthEast;
                        break;
                    case "4":
                        move = Move.Western;
                        break;
                    case "5":
                        move = Move.Center;
                        break;
                    case "6":
                        move = Move.Eastern;
                        break;
                    case "7":
                        move = Move.NorthWest;
                        break;
                    case "8":
                        move = Move.Northern;
                        break;
                    case "9":
                        move = Move.NorthEast;
                        break;
                    default:
                        Console.WriteLine($"Invalid move '{command}' . Move must be 1-9.");
                        continue;
                }

                if (game.IsMoveValid(move))
                {
                    game.Move(move);
                }
                else
                {
                    Console.WriteLine("The move you tried to make is not valid, space is already occupied!");
                }
                Console.WriteLine();
            }

            Console.WriteLine();
            Console.WriteLine($"Game is over!");
            Console.WriteLine($"The result is: {game.GameState}");
            Console.WriteLine();
            RenderBoard(game);
            Console.WriteLine();
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
    }
}
