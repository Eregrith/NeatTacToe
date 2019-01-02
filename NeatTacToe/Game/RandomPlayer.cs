using System;
using System.Collections.Generic;
using System.Text;

namespace NeatTacToe.Game
{
    public class RandomPlayer : IPlayer
    {
        private Random random = new Random();

        public Move GetMove(SquareTypes[,] board)
        {
            int x = random.Next(3);
            int y = random.Next(3);
            while (board[x, y] != SquareTypes.N)
            {
                x = random.Next(3);
                y = random.Next(3);
            }

            return new Move(x, y);
        }
    }
}
