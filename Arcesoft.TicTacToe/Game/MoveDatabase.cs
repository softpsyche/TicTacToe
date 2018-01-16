using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.IO;
using System.Xml;
using System.Linq;

namespace TicTacToe
{
    [Serializable]
    public class MoveDatabase : IMoveDatabase
    {
        private TicTacToeDataSet.MovesDataTable MovesDataTable
        {
            get;
            set;
        }

		public MoveDatabase(TicTacToeDataSet.MovesDataTable movesDataTable)
        {
            this.MovesDataTable = movesDataTable;
        }
        public GameMove LookupBestMove(string currentBoardPosition, Player currentPlayer, Boolean random = true)
        {
            TicTacToeDataSet.MovesRow bestMove;
            Random randy = random ? new Random() : null;

            var moves = this.LookupMoves(currentBoardPosition, currentPlayer);
            var winningMoves = moves.Where(a => a.IsWin).ToList();
            var tieMoves = moves.Where(a => a.IsTie).ToList();
            var losingMoves = moves.Where(a => a.IsLoss).ToList();

            if (winningMoves.Any())
            {
                bestMove = random ? winningMoves[randy.Next(winningMoves.Count)] : winningMoves.First();
            }
            else if (tieMoves.Any())
            {
                bestMove = random ? tieMoves[randy.Next(tieMoves.Count)] : tieMoves.First();
            }
            else if (losingMoves.Any())
            {
                bestMove = random ? losingMoves[randy.Next(losingMoves.Count)] : losingMoves.First();
            }
            else
            {
                throw new GameException("There is no response for that board position and player turn.");
            }

            return new GameMove(bestMove.ResponseRow, bestMove.ResponseColumn);
        }

		private TicTacToeDataSet.MovesRow[] LookupMoves(string currentBoardPosition, Player currentPlayer)
        {
            var searchPattern = String.Format("Board = '{0}' AND Player = '{1}'", currentBoardPosition, currentPlayer.ToString());

            return (TicTacToeDataSet.MovesRow[])this.MovesDataTable.Select(searchPattern);
        }
    }


    public interface IMoveDatabase
    {
        GameMove LookupBestMove(string currentBoardPosition, Player currentPlayer, Boolean random = true);
    }

    public class MoveDatabaseBuilder
    {
        private ArtificialIntelligence ArtificialIntelligence
        {
            get;
            set;
        }

        private MoveDatabaseBuilder(ArtificialIntelligence artificialIntelligence)
        {
            this.ArtificialIntelligence = artificialIntelligence;
        }

		private TicTacToeDataSet.MovesDataTable BuildMovesDataTable()
        {
            TicTacToeDataSet.MovesDataTable movesDataTable = new TicTacToeDataSet.MovesDataTable();

            Collection<BoardLayoutAndGameMoveResult> gameMoveResultCollection = new Collection<BoardLayoutAndGameMoveResult>();

            this.ArtificialIntelligence.GetAllResponsesForGame(gameMoveResultCollection);

            foreach (BoardLayoutAndGameMoveResult boardLayoutAndGameMoveResult
                in gameMoveResultCollection)
            {

                if (movesDataTable.FindByBoardResponseColumnResponseRowPlayer(
                    boardLayoutAndGameMoveResult.BoardLayout,
                    boardLayoutAndGameMoveResult.GameMoveResult.MoveMade.Column,
                    boardLayoutAndGameMoveResult.GameMoveResult.MoveMade.Row,
                    boardLayoutAndGameMoveResult.Player.ToString()) == null)
                {
                    movesDataTable.AddMovesRow(
                        boardLayoutAndGameMoveResult.BoardLayout,
                        boardLayoutAndGameMoveResult.Player.ToString(),
                        boardLayoutAndGameMoveResult.GameMoveResult.MoveMade.Row,
                        boardLayoutAndGameMoveResult.GameMoveResult.MoveMade.Column,
                        boardLayoutAndGameMoveResult.GameMoveResult.BoardStateAfterMove.ToString());

                    movesDataTable.AcceptChanges();
                }
            }

            return movesDataTable;
        }

        public MoveDatabase Build()
        {
            return new MoveDatabase(BuildMovesDataTable());
        }


        public static readonly string DefaultMoveDataBaseFileName = "MoveDatabase.ttt";

        public static MoveDatabase DefaultMoveDatabase
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


        private static MoveDatabase defaultMoveDatabase = null;
        private static Object synchronizingObject = new object();

        
        private static string DefaultMoveDatabaseFilePath
        {
            get
            {
                return AppDomain.CurrentDomain.BaseDirectory + @"\" + DefaultMoveDataBaseFileName;
            }
        }
        private static MoveDatabase LoadOrCreateDefaultMovesDatabase()
        {
            MoveDatabase moveDatabase = null;

            moveDatabase = TryLoadMovesDatabaseFromDisk();

            if (moveDatabase == null)
            {
                var builder = new MoveDatabaseBuilder(new ArtificialIntelligence(new Game(), new GameRng()));

                moveDatabase = builder.Build();

                SaveMovesDataBaseToDisk(moveDatabase);
            }

            return moveDatabase;
        }
        private static MoveDatabase TryLoadMovesDatabaseFromDisk(String filePath = null)
        {
            filePath = filePath ?? DefaultMoveDatabaseFilePath;

            if (File.Exists(DefaultMoveDatabaseFilePath))
            {
                return Utility.Deserialize<MoveDatabase>(filePath);
            }
            else
            {
                return null;
            }
        }
        private static void SaveMovesDataBaseToDisk(MoveDatabase moveDatabase, String filePath = null)
        {
            filePath = filePath ?? DefaultMoveDatabaseFilePath;

            Utility.Serialize<MoveDatabase>(moveDatabase, filePath);
        }
    }
}
