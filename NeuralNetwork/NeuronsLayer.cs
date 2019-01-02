using System;
using System.Collections.Generic;
using System.Linq;

namespace NeuralNetwork
{
    public class NeuronsLayer
    {
        public List<Neuron> Neurons { get; set; }
        public int Id { get; }

        internal NeuronsLayer(int id, int neuronsCount)
        {
            Id = id;
            Neurons = Enumerable.Range(0, neuronsCount).Select(i => new Neuron(Id, i)).ToList();
        }

        internal void BuildDendrites(int dendritesPerNeuron, double defaultWeight)
        {
            Neurons.ForEach(n => n.Dendrites = Enumerable.Range(0, dendritesPerNeuron).Select(i => new Dendrite { SourceNeuronId = new Tuple<int, int>(Id - 1, i), Weight = defaultWeight }).ToList());
        }
    }
}