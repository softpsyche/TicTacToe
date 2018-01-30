using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.IO;
using System.Xml;
using System.Linq;
using Arcesoft.TicTacToe.RandomNumberGeneration;
using Arcesoft.TicTacToe.Entities;
using Arcesoft.TicTacToe.Data;

namespace Arcesoft.TicTacToe.ArtificialIntelligence
{
    [Serializable]
    internal class MoveDataAccess : IMoveDataAccess
    {
        private readonly IMoveRepository _moveRepository;

        public MoveDataAccess(IMoveRepository moveRepository)
        {
            _moveRepository = moveRepository;
        }

        public IEnumerable<MoveResponse> FindMoveResponses(string currentBoardPosition, Player currentPlayer)
        {
            return _moveRepository.FindBoardStates(currentBoardPosition, currentPlayer);
        }
    }
}
