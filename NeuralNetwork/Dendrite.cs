using System;

namespace NeuralNetwork
{
    public class Dendrite
    {
        public Tuple<int, int> SourceNeuronId { get; set; }

        public double Weight { get; set; }
        public double SourcePulse { get; set; }
        public double WeightedValue => SourcePulse * Weight;
    }
}