using System;
using System.Collections.Generic;
using System.Text;

namespace NeatTacToe.Game
{
    public interface IPlayer
    {
        Move GetMove(SquareTypes[,] board);
    }
}
