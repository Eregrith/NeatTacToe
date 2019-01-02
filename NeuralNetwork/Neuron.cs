using System;
using System.Collections.Generic;
using System.Linq;

namespace NeuralNetwork
{
    public class Neuron
    {
        public Neuron(int layerId, int neuronId)
        {
            Id = new Tuple<int, int>(layerId, neuronId);
        }

        public Tuple<int, int> Id { get; set; }

        public double PulseValue => Math.Tanh(Dendrites.Sum(d => d.WeightedValue));

        public List<Dendrite> Dendrites { get; set; }
    }
}