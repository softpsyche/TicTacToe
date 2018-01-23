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
        private IMoveEvaluator _moveEvaluator;
        private IFileAccess _fileAccess;
        private MovesDataTable _movesDataTable;

        private static readonly string DefaultMoveDataBaseFileName = "MoveDatabase.ttt";
        internal string DefaultMoveDatabaseFilePath => AppDomain.CurrentDomain.BaseDirectory + @"\" + DefaultMoveDataBaseFileName;

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
            var boardStates = _moveEvaluator.FindAllMoves();

            foreach (var boardState
                in boardStates)
            {

                if (movesDataTable.FindByBoardResponsePlayer(
                    boardState.BoardLayout,
                    (int)boardState.MoveResult.MoveMade,
                    boardState.Player.ToString()) == null)
                {
                    movesDataTable.AddMovesRow(
                        boardState.BoardLayout,
                        boardState.Player.ToString(),
                        (int)boardState.MoveResult.MoveMade,
                        boardState.MoveResult.BoardStateAfterMove.ToString());

                    movesDataTable.AcceptChanges();
                }
            }

            return movesDataTable;
        }



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
