using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NeuralNetwork
{
    public class NeuralNetwork
    {
        public NeuronsLayer InputLayer { get; set; }
        public List<NeuronsLayer> HiddenLayers { get; set; }
        public NeuronsLayer OutputLayer { get; set; }
        public List<NeuronsLayer> AllLayers { get; set; }

        public void BuildFromDNA(string dna)
        {
            string[] parts = dna.Split(new[] { "||" }, StringSplitOptions.None);

            int inputCount = int.Parse(parts[0]);
            int outputCount = int.Parse(parts[1]);
            int depth = 0;
            int hiddenNeuronsPerLayer = 0;
            if (parts.Length > 2)
                depth = int.Parse(parts[2]);
            if (parts.Length > 3)
                hiddenNeuronsPerLayer = int.Parse(parts[3]);

            InputLayer = new NeuronsLayer(0, inputCount);

            InputLayer.BuildDendrites(1, 1);

            HiddenLayers = Enumerable.Range(1, depth).Select(i => new NeuronsLayer(i, hiddenNeuronsPerLayer)).ToList();

            OutputLayer = new NeuronsLayer(1 + depth, outputCount);

            HiddenLayers.ForEach(h => h.BuildDendrites(h == HiddenLayers.First() ? inputCount : hiddenNeuronsPerLayer, 0));

            OutputLayer.BuildDendrites(hiddenNeuronsPerLayer, 0);

            if (parts.Length > 4)
            {
                int weightCounter = 4;

                foreach (NeuronsLayer nl in HiddenLayers)
                {
                    foreach (Neuron n in nl.Neurons)
                    {
                        foreach (Dendrite d in n.Dendrites)
                        {
                            d.Weight = double.Parse(parts[weightCounter++]);
                        }
                    }
                }
                foreach (Neuron n in OutputLayer.Neurons)
                {
                    foreach (Dendrite d in n.Dendrites)
                    {
                        d.Weight = double.Parse(parts[weightCounter++]);
                    }
                }
            }
            AllLayers = new List<NeuronsLayer>();
            AllLayers.Add(InputLayer);
            AllLayers.AddRange(HiddenLayers);
            AllLayers.Add(OutputLayer);
        }

        public void ProcessInput(List<double> inputs)
        {
            int i = 0;
            foreach (NeuronsLayer layer in AllLayers)
            {
                foreach (Neuron n in layer.Neurons)
                {
                    foreach (Dendrite d in n.Dendrites)
                    {
                        if (layer == AllLayers.First())
                            d.SourcePulse = inputs[i++];
                        else
                            d.SourcePulse = AllLayers[d.SourceNeuronId.Item1].Neurons[d.SourceNeuronId.Item2].PulseValue;
                    }
                }
            }
        }
    }
}
