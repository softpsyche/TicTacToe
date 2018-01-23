using Arcesoft.TicTacToe.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using static Arcesoft.TicTacToe.Data.TicTacToeDataSet;

namespace Arcesoft.TicTacToe.ArtificialIntelligence
{
    /// <summary>
    /// Represents an in-memory database of all the possible tic-tac-toe board configurations. 
    /// </summary>
    internal class MoveDatabase : IMoveDatabase
    {
        private static readonly string DefaultMoveDataBaseFileName = "MoveDatabase.ttt";
        private IMoveEvaluator _moveEvaluator;
        private IFileAccess _fileAccess;
        private MovesDataTable _movesDataTable;

        public MoveDatabase(IMoveEvaluator moveEvaluator, IFileAccess fileAccess)
        {
            _moveEvaluator = moveEvaluator;
            _fileAccess = fileAccess;
        }

        public IMovesDataTable MovesDataTable
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

            if (_fileAccess.Exists(DefaultMoveDatabaseFilePath))
            {
                try
                {
                    return _fileAccess.DeserializeBinary<MovesDataTable>(filePath);
                }
                catch (SerializationException ex)
                {
                    //this path is bad, delete the file
                    _fileAccess.Delete(filePath);
                }

                return null;
            }
            else
            {
                return null;
            }
        }
        private void SaveMovesDataTableToDisk(MovesDataTable moveDatabase, String filePath = null)
        {
            filePath = filePath ?? DefaultMoveDatabaseFilePath;

            _fileAccess.SerializeBinary(moveDatabase, filePath);
        }
    }
}
