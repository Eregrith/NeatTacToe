using NeatTacToe.Game;
using NeuralNetwork;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NeatTacToe.Players
{
    public class NeuralPlayer : IPlayer
    {
        private readonly NeuralNetwork.NeuralNetwork Brain;
        public SquareTypes SquareType { get; set; }

        public NeuralPlayer(NeuralNetwork.NeuralNetwork brain, SquareTypes mySquareType)
        {
            Brain = brain;
            SquareType = mySquareType;
        }

        public Move GetMove(SquareTypes[,] board)
        {
            List<double> inputs = new List<double>
            {
                getInputForSquareType(board[0, 0]),
                getInputForSquareType(board[0, 1]),
                getInputForSquareType(board[0, 2]),

                getInputForSquareType(board[1, 0]),
                getInputForSquareType(board[1, 1]),
                getInputForSquareType(board[1, 2]),

                getInputForSquareType(board[2, 0]),
                getInputForSquareType(board[2, 1]),
                getInputForSquareType(board[2, 2]),
            };
            Brain.ProcessInput(inputs);
            int i = 0;
            Neuron firedNeuron;
            var neurons = Brain.OutputLayer.Neurons.OrderByDescending(n => n.PulseValue).ToList();
            do
            {
                firedNeuron = neurons[i++];
            }
            while (board[firedNeuron.Id.Item2 % 3, firedNeuron.Id.Item2 / 3] != SquareTypes.N);

            return new Move(firedNeuron.Id.Item2 % 3, firedNeuron.Id.Item2 / 3);
        }

        private double getInputForSquareType(SquareTypes squareTypes)
        {
            if (squareTypes == SquareTypes.N)
                return 0;
            if (squareTypes == SquareType)
                return 1;
            return -1;
        }
    }
}
