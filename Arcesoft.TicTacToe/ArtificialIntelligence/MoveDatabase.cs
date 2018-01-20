using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Arcesoft.TicTacToe.ArtificialIntelligence.TicTacToeDataSet;

namespace Arcesoft.TicTacToe.ArtificialIntelligence
{
    /// <summary>
    /// Represents an in-memory database of all the possible tic-tac-toe board configurations. 
    /// </summary>
    internal class MoveDatabase:IMoveDatabase
    {
        private static readonly string DefaultMoveDataBaseFileName = "MoveDatabase.ttt";
        private IMoveEvaluator _moveEvaluator;
        private MovesDataTable _movesDataTable;

        public MoveDatabase(IMoveEvaluator moveEvaluator)
        {
            _moveEvaluator = moveEvaluator;
        }

        public MovesDataTable MovesDataTable
        {
            get
            {
                if (_movesDataTable == null)
                {
                    _movesDataTable = LoadOrCreateDefaultMovesDatatable();
                }

                return _movesDataTable;
            }
        }

        private MovesDataTable LoadOrCreateDefaultMovesDatatable()
        {
            MovesDataTable movesDataTable = null;

            movesDataTable = TryLoadMovesDatabaseFromDisk();

            if (movesDataTable == null)
            {
                movesDataTable = BuildMovesDataTable();

                SaveMovesDataTableToDisk(movesDataTable);
            }

            return movesDataTable;
        }

        private MovesDataTable BuildMovesDataTable()
        {
            MovesDataTable movesDataTable = new MovesDataTable();
            Collection<BoardState> gameMoveResultCollection = _moveEvaluator.FindAllMoves();

            foreach (BoardState boardLayoutAndGameMoveResult
                in gameMoveResultCollection)
            {

                if (movesDataTable.FindByBoardResponsePlayer(
                    boardLayoutAndGameMoveResult.BoardLayout,
                    (int)boardLayoutAndGameMoveResult.MoveResult.MoveMade,
                    boardLayoutAndGameMoveResult.Player.ToString()) == null)
                {
                    movesDataTable.AddMovesRow(
                        boardLayoutAndGameMoveResult.BoardLayout,
                        boardLayoutAndGameMoveResult.Player.ToString(),
                        (int)boardLayoutAndGameMoveResult.MoveResult.MoveMade,
                        boardLayoutAndGameMoveResult.MoveResult.BoardStateAfterMove.ToString());

                    movesDataTable.AcceptChanges();
                }
            }

            return movesDataTable;
        }

        private string DefaultMoveDatabaseFilePath => AppDomain.CurrentDomain.BaseDirectory + @"\" + DefaultMoveDataBaseFileName;

        private MovesDataTable TryLoadMovesDatabaseFromDisk(String filePath = null)
        {
            filePath = filePath ?? DefaultMoveDatabaseFilePath;

            if (File.Exists(DefaultMoveDatabaseFilePath))
            {
                return Utility.Deserialize<MovesDataTable>(filePath);
            }
            else
            {
                return null;
            }
        }
        private void SaveMovesDataTableToDisk(MovesDataTable moveDatabase, String filePath = null)
        {
            filePath = filePath ?? DefaultMoveDatabaseFilePath;

            Utility.Serialize(moveDatabase, filePath);
        }
    }
}
