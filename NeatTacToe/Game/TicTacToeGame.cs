using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace NeatTacToe.Game
{
    public class TicTacToeGame
    {
        public SquareTypes[,] Board { get; set; }
        public TicTacToeGame()
        {
            Board = new SquareTypes[3, 3];
        }

        public static SquareTypes PlayGameToEnd(IPlayer xPlayer, IPlayer oPlayer)
        {
            TicTacToeGame game = new TicTacToeGame();
            SquareTypes winner = SquareTypes.N;

            for (int moveNum = 0; moveNum < 9 && winner == SquareTypes.N; moveNum++)
            {
                Console.WriteLine($"+- Turn {moveNum+1} -+");

                IPlayer curPlayer;
                SquareTypes curSquareType;

                if (moveNum % 2 == 0)
                {
                    curPlayer = xPlayer;
                    curSquareType = SquareTypes.X;
                }
                else
                {
                    curPlayer = oPlayer;
                    curSquareType = SquareTypes.O;
                }

                var move = curPlayer.GetMove(game.Board);

                Debug.Assert(game.IsEmpty(move.X, move.Y), "Player tried to make an illegal move!");

                game.Board[move.X, move.Y] = curSquareType;

                if (moveNum > 3)
                    winner = game.GetWinner();

                DisplayBoard(game.Board);
            }

            return winner;
        }

        private static void DisplayBoard(SquareTypes[,] board)
        {
            string a = board[0, 0] == SquareTypes.N ? " " : board[0, 0].ToString();
            string b = board[0, 1] == SquareTypes.N ? " " : board[0, 1].ToString();
            string c = board[0, 2] == SquareTypes.N ? " " : board[0, 2].ToString();
            string d = board[1, 0] == SquareTypes.N ? " " : board[1, 0].ToString();
            string e = board[1, 1] == SquareTypes.N ? " " : board[1, 1].ToString();
            string f = board[1, 2] == SquareTypes.N ? " " : board[1, 2].ToString();
            string g = board[2, 0] == SquareTypes.N ? " " : board[2, 0].ToString();
            string h = board[2, 1] == SquareTypes.N ? " " : board[2, 1].ToString();
            string i = board[2, 2] == SquareTypes.N ? " " : board[2, 2].ToString();
            Console.WriteLine("+---+---+---+");
            Console.WriteLine($"| {a} | {b} | {c} |");
            Console.WriteLine("+---+---+---+");
            Console.WriteLine($"| {d} | {e} | {f} |");
            Console.WriteLine("+---+---+---+");
            Console.WriteLine($"| {g} | {h} | {i} |");
            Console.WriteLine("+---+---+---+");
        }

        public void ResetGame()
        {
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    Board[i, j] = SquareTypes.N;
        }

        public bool IsEmpty(int x, int y)
        {
            Debug.Assert(x < 3 && x >= 0);
            Debug.Assert(y < 3 && y >= 0);
            return Board[x, y] == SquareTypes.N;
        }

        public SquareTypes GetWinner()
        {
            return GetWinner(Board);
        }

        internal static SquareTypes GetWinner(SquareTypes[,] Board)
        {
            if (Board[0, 0] != SquareTypes.N)
            {
                var type = Board[0, 0];
                if (type == Board[0, 1] && type == Board[0, 2])
                    return type;

                if (type == Board[1, 0] && type == Board[2, 0])
                    return type;

                if (type == Board[1, 1] && type == Board[2, 2])
                    return type;
            }

            if (Board[0, 2] != SquareTypes.N)
            {
                var type = Board[0, 2];

                if (type == Board[1, 1] && type == Board[2, 0])
                    return type;

                if (type == Board[1, 2] && type == Board[2, 2])
                    return type;
            }

            if (Board[0, 1] != SquareTypes.N)
            {
                var type = Board[0, 1];

                if (type == Board[1, 1] && type == Board[2, 1])
                    return type;
            }

            if (Board[1, 0] != SquareTypes.N)
            {
                var type = Board[1, 0];

                if (type == Board[1, 1] && type == Board[1, 2])
                    return type;
            }

            if (Board[2, 0] != SquareTypes.N)
            {
                var type = Board[2, 0];

                if (type == Board[2, 1] && type == Board[2, 2])
                    return type;
            }

            return SquareTypes.N;
        }

        public static SquareTypes[,] GetBoardFromString(string boardString)
        {
            var lineTokens = boardString.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            Debug.Assert(lineTokens.Length == 3, "Invalid board string:\n" + boardString + "\n");

            var tokens = new string[9];

            var line = lineTokens[0].Trim().Split('|');
            Debug.Assert(line.Length == 3);
            line.CopyTo(tokens, 0);

            line = lineTokens[1].Trim().Split('|');
            Debug.Assert(line.Length == 3);
            line.CopyTo(tokens, 3);

            line = lineTokens[2].Trim().Split('|');
            Debug.Assert(line.Length == 3);
            line.CopyTo(tokens, 6);

            Debug.Assert(tokens.Length == 9, "Invalid board string:\n" + boardString + "\n");

            SquareTypes[,] board = new SquareTypes[3, 3];

            board[0, 0] = getSquareType(tokens[0]);
            board[1, 0] = getSquareType(tokens[1]);
            board[2, 0] = getSquareType(tokens[2]);
            board[0, 1] = getSquareType(tokens[3]);
            board[1, 1] = getSquareType(tokens[4]);
            board[2, 1] = getSquareType(tokens[5]);
            board[0, 2] = getSquareType(tokens[6]);
            board[1, 2] = getSquareType(tokens[7]);
            board[2, 2] = getSquareType(tokens[8]);
            return board;
        }

        private static SquareTypes getSquareType(string squareString)
        {
            switch (squareString.Trim())
            {
                case "X": return SquareTypes.X;
                case "O": return SquareTypes.O;
                case "N":
                case "": return SquareTypes.N;
                default: throw new Exception("Unknown square string: " + squareString);
            }
        }
    }
}
