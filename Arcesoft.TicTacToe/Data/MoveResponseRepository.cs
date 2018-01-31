using Arcesoft.TicTacToe.Database;
using Arcesoft.TicTacToe.Entities;
using Arcesoft.TicTacToe.GameImplementation;
using Arcesoft.TicTacToe.RandomNumberGeneration;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Arcesoft.TicTacToe.Data.TicTacToeDataSet;

namespace Arcesoft.TicTacToe.Data
{
    internal class MoveResponseRepository : IMoveResponseRepository
    {
        private const string MoveRepositoryName = "TicTacToe.db";
        private ILiteDatabaseFactory _liteDatabaseFactory;

        public MoveResponseRepository(ILiteDatabaseFactory liteDatabaseFactory)
        {
            _liteDatabaseFactory = liteDatabaseFactory;
        }

        private ILiteDatabase Database() => _liteDatabaseFactory.OpenOrCreate(MoveRepositoryName);

        public void InsertMoveResponse(MoveResponse moveResponse)
        {
            using (var db = Database())
            {
                db.EnsureIndex<MoveResponseRecord, string>(a => a.Id);

                db.Insert(ToMoveResponseRecord(moveResponse));
            }
        }

        public MoveResponse TryFindMoveResponse(string board, Player player, Move response)
        {
            using (var db = Database())
            {
                var record = new MoveResponseRecord()
                {
                    Board = board,
                    Player = player,
                    Response = response
                };

                return ToMoveResponse(db.FindById<MoveResponseRecord, string>(record.Id));
            }
        }

        public IEnumerable<MoveResponse> FindMoveResponses(string currentBoardPosition, Player currentPlayer)
        {
            using (var db = Database())
            {
                var responses = db
                    .FindByIndex<MoveResponseRecord>(a => a.Board == currentBoardPosition)
                    .Where(a => a.Player == currentPlayer);

                return ToMoveResponses(responses);
            }
        }

        public int FindMoveResponseCount()
        {
            using (var db = Database())
            {
                return db.Count<MoveResponse>();
            }
        }

        private MoveResponseRecord ToMoveResponseRecord(MoveResponse moveResponse)
        {
            return new MoveResponseRecord()
            {
                Board = moveResponse.Board,
                Outcome = moveResponse.Outcome,
                Player = moveResponse.Player,
                Response = moveResponse.Response
            };
        }

        private IEnumerable<MoveResponse> ToMoveResponses(IEnumerable<MoveResponseRecord> movesRows)
        {
            List<MoveResponse> listy = new List<MoveResponse>();

            movesRows.ForEach(a => listy.Add(ToMoveResponse(a)));

            return listy;
        }
        private MoveResponse ToMoveResponse(MoveResponseRecord moveResponseRecord)
        {
            if (moveResponseRecord == null) return null;

            return new MoveResponse()
            {
                Board = moveResponseRecord.Board,
                Outcome = moveResponseRecord.Outcome,
                Player = moveResponseRecord.Player,
                Response = moveResponseRecord.Response
            };
        }

    }
}
