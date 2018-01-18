using Arcesoft.TicTacToe.GameImplementation;
using Arcesoft.TicTacToe.RandomNumberGeneration;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arcesoft.TicTacToe.ArtificialIntelligence
{
    internal class MoveDatabaseBuilder
    {
        public static readonly string DefaultMoveDataBaseFileName = "MoveDatabase.ttt";
        private static MoveRepository defaultMoveDatabase = null;
        private static Object synchronizingObject = new object();

        private MoveEvaluator BestMoveFinder
        {
            get;
            set;
        }

        private MoveDatabaseBuilder(MoveEvaluator artificialIntelligence)
        {
            BestMoveFinder = artificialIntelligence;
        }

        private TicTacToeDataSet.MovesDataTable BuildMovesDataTable()
        {
            TicTacToeDataSet.MovesDataTable movesDataTable = new TicTacToeDataSet.MovesDataTable();
            Collection<BoardLayoutAndGameMoveResult> gameMoveResultCollection = BestMoveFinder.FindAllMoves();

            foreach (BoardLayoutAndGameMoveResult boardLayoutAndGameMoveResult
                in gameMoveResultCollection)
            {

                if (movesDataTable.FindByBoardResponsePlayer(
                    boardLayoutAndGameMoveResult.BoardLayout,
                    (int)boardLayoutAndGameMoveResult.GameMoveResult.MoveMade,
                    boardLayoutAndGameMoveResult.Player.ToString()) == null)
                {
                    movesDataTable.AddMovesRow(
                        boardLayoutAndGameMoveResult.BoardLayout,
                        boardLayoutAndGameMoveResult.Player.ToString(),
                        (int)boardLayoutAndGameMoveResult.GameMoveResult.MoveMade,
                        boardLayoutAndGameMoveResult.GameMoveResult.BoardStateAfterMove.ToString());

                    movesDataTable.AcceptChanges();
                }
            }

            return movesDataTable;
        }

        public MoveRepository Build()
        {
            return new MoveRepository(BuildMovesDataTable());
        }

        public static MoveRepository DefaultMoveDatabase
        {
            get
            {
                if (defaultMoveDatabase == null)
                {
                    lock (synchronizingObject)
                    {
                        if (defaultMoveDatabase == null)
                        {
                            defaultMoveDatabase = LoadOrCreateDefaultMovesDatabase();
                        }
                    }
                }

                return defaultMoveDatabase;
            }
        }

        private static string DefaultMoveDatabaseFilePath
        {
            get
            {
                return AppDomain.CurrentDomain.BaseDirectory + @"\" + DefaultMoveDataBaseFileName;
            }
        }
        private static MoveRepository LoadOrCreateDefaultMovesDatabase()
        {
            MoveRepository moveDatabase = null;

            moveDatabase = TryLoadMovesDatabaseFromDisk();

            if (moveDatabase == null)
            {
                var builder = new MoveDatabaseBuilder(new MoveEvaluator(new GameFactory(), new DefaultRandomNumberGenerator()));

                moveDatabase = builder.Build();

                SaveMovesDataBaseToDisk(moveDatabase);
            }

            return moveDatabase;
        }
        private static MoveRepository TryLoadMovesDatabaseFromDisk(String filePath = null)
        {
            filePath = filePath ?? DefaultMoveDatabaseFilePath;

            if (File.Exists(DefaultMoveDatabaseFilePath))
            {
                return Utility.Deserialize<MoveRepository>(filePath);
            }
            else
            {
                return null;
            }
        }
        private static void SaveMovesDataBaseToDisk(MoveRepository moveDatabase, String filePath = null)
        {
            filePath = filePath ?? DefaultMoveDatabaseFilePath;

            Utility.Serialize<MoveRepository>(moveDatabase, filePath);
        }
    }
}
