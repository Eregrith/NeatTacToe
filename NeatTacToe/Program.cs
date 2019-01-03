using NeatTacToe.Game;
using System;

namespace NeatTacToe
{
    public static class Program
    {
        static void Main(string[] args)
        {
            RandomPlayer p1 = new RandomPlayer();
            RandomPlayer p2 = new RandomPlayer();
            SquareTypes winner = TicTacToeGame.PlayGameToEnd(p1, p2);

            Console.WriteLine($"{winner} is the winner !");
            Console.ReadLine();
        }
    }
}
