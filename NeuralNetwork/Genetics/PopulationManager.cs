using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NeuralNetwork.Genetics
{
    public class PopulationManager
    {
        private readonly IRandomEnvironment _randomEnvironment;

        public PopulationManager(IRandomEnvironment randomEnvironment)
        {
            _randomEnvironment = randomEnvironment;
        }

        public DNA GetChildOf(DNA mum, DNA dad)
        {
            string dna = dad.ToString();
            if (_randomEnvironment.DoesRecombinationOccur())
                dna = Recombine(mum, dad);

            DNA child = new DNA(dna);

            if (_randomEnvironment.DoesSwapMutationOccur())
                SwapMutate(child);

            if (_randomEnvironment.DoesAdaptationMutationOccur())
                AdaptationMutate(child);

            if (_randomEnvironment.DoesInversionMutationOccur())
                InversionMutate(child);

            if (_randomEnvironment.DoesReplacementMutationOccur())
                ReplacementMutate(child);

            if (_randomEnvironment.DoesFlipMutationOccur())
                FlipMutate(child);

            return child;
        }

        private string Recombine(DNA mum, DNA dad)
        {
            int splits = _randomEnvironment.GetNextInt(mum.Genes.Count - 1);
            int[] splitSizes = new int[splits];
            for (int i = 0; i < splits - 1; i++)
            {
                splitSizes[i] = _randomEnvironment.GetNextInt((mum.Genes.Count - 1) - (splits - i));
            }
            splitSizes[splits - 1] = mum.Genes.Count - splitSizes.Sum();
            List<double> genes = new List<double>();
            DNA source = _randomEnvironment.GetRandomDirection() == Directions.Down ? mum : dad;
            int s = 0;
            for (int g = 0; g < mum.Genes.Count; g++)
            {
                genes.Add(source.Genes[g]);
                splitSizes[s]--;
                if (splitSizes[s] == 0)
                {
                    s++;
                    source = source == dad ? mum : dad;
                }
            }
            return String.Join("||", genes);
        }

        private void FlipMutate(DNA child)
        {
            int gene = _randomEnvironment.GetNextInt(child.Genes.Count - 1);
            child.Genes[gene] = -child.Genes[gene];
        }

        private void ReplacementMutate(DNA child)
        {
            int replacedGene = _randomEnvironment.GetNextInt(child.Genes.Count - 1);
            double min = child.Genes[replacedGene] * -2;
            double max = child.Genes[replacedGene] * 2;

            child.Genes[replacedGene] = _randomEnvironment.GetNextDouble(min, max);
        }

        private void InversionMutate(DNA child)
        {
            int from = _randomEnvironment.GetNextInt(child.Genes.Count - 1);
            int size = _randomEnvironment.GetNextInt(child.Genes.Count - 1 - from);
            double[] inversedGenes = new double[size];
            for (int g = 0; g < size; g++)
            {
                inversedGenes[size - g - 1] = child.Genes[g + from];
            }
            for (int g = 0; g <size; g++)
            {
                child.Genes[g + from] = inversedGenes[g];
            }
        }

        private void AdaptationMutate(DNA child)
        {
            int numberOfGenesMuting = _randomEnvironment.GetNextInt(child.Genes.Count - 1);
            while (numberOfGenesMuting-- != 0)
            {
                int gene = _randomEnvironment.GetNextInt(child.Genes.Count - 1);
                Directions direction = _randomEnvironment.GetRandomDirection();
                child.Genes[gene] *= direction == Directions.Up ? 1.05f : 0.95f;
            }
        }

        private void SwapMutate(DNA child)
        {
            int first = _randomEnvironment.GetNextInt(child.Genes.Count - 1);
            int second = _randomEnvironment.GetNextInt(child.Genes.Count - 1);
            double save = child.Genes[first];
            child.Genes[first] = child.Genes[second];
            child.Genes[second] = save;
        }
    }
}
