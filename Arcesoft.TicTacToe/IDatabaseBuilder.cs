﻿using Arcesoft.TicTacToe.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arcesoft.TicTacToe
{
    public interface IDatabaseBuilder
    {
        bool DatabaseIsEmpty();
        void PopulateMoveResponses(IGame game = null);
    }
}
