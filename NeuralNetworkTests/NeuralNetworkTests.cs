using FluentAssertions;
using NeuralNetwork;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NeuralNetworkTests
{
    [TestFixture]
    public class NeuralNetworkTests
    {
        [Test]
        public void BuildFromDNA_Should_Return_InputLayer_And_OutputLayer_With_Given_Sizes_In_DNA()
        {
            NeuralNetwork.NeuralNetwork nn = new NeuralNetwork.NeuralNetwork();
            int inputCount = 5;
            int outputCount = 10;
            string DNA = $"{inputCount}||{outputCount}";

            nn.BuildFromDNA(DNA);
            
            nn.InputLayer.Neurons.Should().HaveCount(inputCount);
            nn.OutputLayer.Neurons.Should().HaveCount(outputCount);
        }

        [Test]
        public void BuildFromDNA_Should_Set_All_Neurons_Id_According_To_Its_Place_In_Layers()
        {
            NeuralNetwork.NeuralNetwork nn = new NeuralNetwork.NeuralNetwork();
            int inputCount = 5;
            int outputCount = 10;
            string DNA = $"{inputCount}||{outputCount}";

            nn.BuildFromDNA(DNA);

            nn.InputLayer.Id.Should().NotBe(nn.OutputLayer.Id);
            foreach (Neuron n in nn.InputLayer.Neurons)
            {
                n.Id.Item1.Should().Be(nn.InputLayer.Id);
                nn.InputLayer.Neurons.Should().ContainSingle(neuron => neuron.Id.Item2 == n.Id.Item2);
            }
            foreach (Neuron n in nn.OutputLayer.Neurons)
            {
                n.Id.Item1.Should().Be(nn.OutputLayer.Id);
                nn.OutputLayer.Neurons.Should().ContainSingle(neuron => neuron.Id.Item2 == n.Id.Item2);
            }
        }

        [Test]
        public void BuildFromDNA_Should_Create_HiddenLayers_From_Depth_And_HiddenNeuronsPerLayer_In_DNA()
        {
            NeuralNetwork.NeuralNetwork nn = new NeuralNetwork.NeuralNetwork();
            int inputCount = 5;
            int outputCount = 8;
            int depth = 5;
            int hiddenNeuronsPerLayer = 10;
            string DNA = $"{inputCount}||{outputCount}||{depth}||{hiddenNeuronsPerLayer}";

            nn.BuildFromDNA(DNA);

            nn.HiddenLayers.Should().HaveCount(depth);
        }

        [Test]
        public void BuildFromDNA_Should_Create_InputLayer_Neurons_With_One_Dendrite_Each()
        {
            NeuralNetwork.NeuralNetwork nn = new NeuralNetwork.NeuralNetwork();
            int inputCount = 2;
            int outputCount = 2;
            int depth = 1;
            int hiddenNeuronsPerLayer = 2;
            List<double> weights = new List<double>
            {
                1, 2,
                3, 4,

                5, 6,
                7, 8,
            };
            string DNA = $"{inputCount}||{outputCount}||{depth}||{hiddenNeuronsPerLayer}||{string.Join("||", weights)}";

            nn.BuildFromDNA(DNA);

            foreach (Neuron n in nn.InputLayer.Neurons)
            {
                n.Dendrites.Should().HaveCount(1);
            }
        }

        /*     0         1         2
         *    [0] -1-   [0] - 5 - [0]
         *         / \        / \ 
         *        2   3      6   7
         *       /     \    /     \
         *    [1] - 4 - [1] - 8 - [1]
         *    
         *    Dendrites :
         *    0 0 weight 1
         *    0 1 weight 2
         *    0 0 weight 3
         *    0 0 weight 4
         *    1 0 weight 5
         *    1 1 weight 6
         *    1 0 weight 7
         *    1 1 weight 8
         */
        [Test]
        public void BuildFromDNA_Should_Create_Next_Layers_Dendrites_With_Correct_SourceIds()
        {
            NeuralNetwork.NeuralNetwork nn = new NeuralNetwork.NeuralNetwork();
            int inputCount = 2;
            int outputCount = 2;
            int depth = 1;
            int hiddenNeuronsPerLayer = 2;
            string DNA = $"{inputCount}||{outputCount}||{depth}||{hiddenNeuronsPerLayer}";

            nn.BuildFromDNA(DNA);

            nn.HiddenLayers[0].Neurons[0].Dendrites.Should().HaveCount(2);
            nn.HiddenLayers[0].Neurons[0].Dendrites[0].SourceNeuronId.Item1.Should().Be(0);
            nn.HiddenLayers[0].Neurons[0].Dendrites[0].SourceNeuronId.Item2.Should().Be(0);

            nn.HiddenLayers[0].Neurons[0].Dendrites[1].SourceNeuronId.Item1.Should().Be(0);
            nn.HiddenLayers[0].Neurons[0].Dendrites[1].SourceNeuronId.Item2.Should().Be(1);


            nn.HiddenLayers[0].Neurons[1].Dendrites.Should().HaveCount(2);
            nn.HiddenLayers[0].Neurons[1].Dendrites[0].SourceNeuronId.Item1.Should().Be(0);
            nn.HiddenLayers[0].Neurons[1].Dendrites[0].SourceNeuronId.Item2.Should().Be(0);

            nn.HiddenLayers[0].Neurons[1].Dendrites[1].SourceNeuronId.Item1.Should().Be(0);
            nn.HiddenLayers[0].Neurons[1].Dendrites[1].SourceNeuronId.Item2.Should().Be(1);

            nn.OutputLayer.Neurons[0].Dendrites.Should().HaveCount(2);
            nn.OutputLayer.Neurons[0].Dendrites[0].SourceNeuronId.Item1.Should().Be(1);
            nn.OutputLayer.Neurons[0].Dendrites[0].SourceNeuronId.Item2.Should().Be(0);

            nn.OutputLayer.Neurons[0].Dendrites[1].SourceNeuronId.Item1.Should().Be(1);
            nn.OutputLayer.Neurons[0].Dendrites[1].SourceNeuronId.Item2.Should().Be(1);

            nn.OutputLayer.Neurons[1].Dendrites.Should().HaveCount(2);
            nn.OutputLayer.Neurons[1].Dendrites[0].SourceNeuronId.Item1.Should().Be(1);
            nn.OutputLayer.Neurons[1].Dendrites[0].SourceNeuronId.Item2.Should().Be(0);

            nn.OutputLayer.Neurons[1].Dendrites[1].SourceNeuronId.Item1.Should().Be(1);
            nn.OutputLayer.Neurons[1].Dendrites[1].SourceNeuronId.Item2.Should().Be(1);
        }

        [Test]
        public void BuildFromDNA_Should_Set_Input_Dendrites_Weights_To_One()
        {
            NeuralNetwork.NeuralNetwork nn = new NeuralNetwork.NeuralNetwork();
            int inputCount = 2;
            int outputCount = 2;
            int depth = 1;
            int hiddenNeuronsPerLayer = 2;
            List<double> weights = new List<double>
            {
                1, 2,
                3, 4,

                5, 6,
                7, 8,
            };
            string DNA = $"{inputCount}||{outputCount}||{depth}||{hiddenNeuronsPerLayer}||{string.Join("||", weights)}";

            nn.BuildFromDNA(DNA);
            
            foreach (Dendrite d in nn.InputLayer.Neurons.SelectMany(n => n.Dendrites))
            {
                d.Weight.Should().Be(1);
            }
        }

        [Test]
        public void BuildFromDNA_Should_Set_Next_Layers_Dendrites_Weights_From_DNA()
        {
            NeuralNetwork.NeuralNetwork nn = new NeuralNetwork.NeuralNetwork();
            int inputCount = 2;
            int outputCount = 2;
            int depth = 1;
            int hiddenNeuronsPerLayer = 2;
            List<double> weights = new List<double>
            {
                1, 2,
                3, 4,

                5, 6,
                7, 8,
            };
            string DNA = $"{inputCount}||{outputCount}||{depth}||{hiddenNeuronsPerLayer}||{string.Join("||", weights)}";

            nn.BuildFromDNA(DNA);

            nn.HiddenLayers[0].Neurons[0].Dendrites[0].Weight.Should().Be(weights[0]);
            nn.HiddenLayers[0].Neurons[0].Dendrites[1].Weight.Should().Be(weights[1]);
            nn.HiddenLayers[0].Neurons[1].Dendrites[0].Weight.Should().Be(weights[2]);
            nn.HiddenLayers[0].Neurons[1].Dendrites[1].Weight.Should().Be(weights[3]);
            nn.OutputLayer.Neurons[0].Dendrites[0].Weight.Should().Be(weights[4]);
            nn.OutputLayer.Neurons[0].Dendrites[1].Weight.Should().Be(weights[5]);
            nn.OutputLayer.Neurons[1].Dendrites[0].Weight.Should().Be(weights[6]);
            nn.OutputLayer.Neurons[1].Dendrites[1].Weight.Should().Be(weights[7]);
        }

        [Test]
        public void ProcessInput_Should_Process_Input_Pulses_To_Output_Neurons()
        {
            NeuralNetwork.NeuralNetwork nn = new NeuralNetwork.NeuralNetwork();
            int inputCount = 2;
            int outputCount = 2;
            int depth = 1;
            int hiddenNeuronsPerLayer = 2;
            List<double> weights = new List<double>
            {
                1, -1,
                -1, 1,

                1, -1,
                -1, 1,
            };
            string DNA = $"{inputCount}||{outputCount}||{depth}||{hiddenNeuronsPerLayer}||{string.Join("||", weights)}";
            nn.BuildFromDNA(DNA);
            double A = 1;
            double B = -1;

            nn.ProcessInput(new List<double> { A, B });

            nn.OutputLayer.Neurons[0].PulseValue.Should().Be(Math.Tanh(Math.Tanh(Math.Tanh(A) - Math.Tanh(B)) - Math.Tanh(Math.Tanh(B) - Math.Tanh(A))));
            nn.OutputLayer.Neurons[1].PulseValue.Should().Be(Math.Tanh(Math.Tanh(Math.Tanh(B) - Math.Tanh(A)) - Math.Tanh(Math.Tanh(A) - Math.Tanh(B))));
        }
    }
}
