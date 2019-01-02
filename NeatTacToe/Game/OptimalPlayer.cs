using System;
using System.Collections.Generic;
using System.Text;

namespace NeatTacToe.Game
{
    public class OptimalPlayer : IPlayer
    {
        public SquareTypes SquareType { get; set; }

        public OptimalPlayer(SquareTypes type)
        {
            SquareType = type;
        }

        public Move GetMove(SquareTypes[,] board)
        {
            int moveNum = 0;
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    if (board[i, j] != SquareTypes.N)
                        moveNum++;

            //first move is always a corner
            if (moveNum == 0)
                return new Move(0, 0);

            //second move should be the center if free, else a corner
            if (moveNum == 1)
            {
                if (board[1, 1] == SquareTypes.N)
                    return new Move(1, 1);

                return new Move(0, 0);
            }

            //make a winning move if possible
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                {
                    if (board[i, j] != SquareTypes.N)
                        continue;

                    board[i, j] = SquareType;
                    var winner = TicTacToeGame.GetWinner(board);
                    board[i, j] = SquareTypes.N;
                    if (winner == SquareType)
                        return new Move(i, j);
                }

            //if we can't win, check if there are any moves that we have to make
            //to prevent ourselves from losing
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                {
                    if (board[i, j] != SquareTypes.N)
                        continue;

                    //set the move to the opponent's type
                    board[i, j] = SquareType == SquareTypes.X ? SquareTypes.O : SquareTypes.X;
                    var winner = TicTacToeGame.GetWinner(board);
                    board[i, j] = SquareTypes.N;

                    //if the opponent will win by moving here, move here to block them
                    if (winner != SquareTypes.N)
                        return new Move(i, j);
                }

            //if we're here, that means we have made at least 1 move already and can't win
            //nor lose in 1 move, so just make the optimal play which would be to a free
            //corner that isn't blocked
            Move move = null;
            int max = -1;
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                {
                    if (board[i, j] != SquareTypes.N)
                        continue;

                    board[i, j] = SquareType;
                    int count = 0;
                    for (int m = 0; m < 3; m++)
                        for (int n = 0; n < 3; n++)
                        {
                            if (board[m, n] != SquareTypes.N)
                                continue;

                            board[m, n] = SquareType;
                            var winner = TicTacToeGame.GetWinner(board);
                            board[m, n] = SquareTypes.N;
                            if (winner == SquareType)
                                count++;
                        }
                    board[i, j] = SquareTypes.N;
                    if (count > max)
                    {
                        move = new Move(i, j);
                        max = count;
                    }
                }

            return move;
        }
    }
}
