using System;
using System.Collections.Generic;
using System.Text;

namespace NeatTacToe.Game
{
    public class Move
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Move(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
