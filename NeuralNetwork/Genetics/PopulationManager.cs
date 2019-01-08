using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace NeuralNetwork.Genetics
{
    public class PopulationManager
    {
        private readonly IRandomEnvironment _randomEnvironment;
        public List<DNA> Population { get; set; }

        public PopulationManager(IRandomEnvironment randomEnvironment)
        {
            _randomEnvironment = randomEnvironment;
        }

        public void GeneratePopulation(int individuals, int genesPer)
        {
            Population = new List<DNA>();
            while (individuals-- > 0)
            {
                IEnumerable<double> genes = Enumerable.Range(0, genesPer).Select(i => _randomEnvironment.GetNextDouble(-1, 1));
                string geneString = String.Join("||", genes);
                DNA individual = new DNA(geneString);
                Population.Add(individual);
            }
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
            int splits = _randomEnvironment.GetNextInt(3) + 2;
            int genesSplitted = 0;
            int[] splitSizes = new int[splits];
            for (int i = 0; i < splits - 1; i++)
            {
                splitSizes[i] = _randomEnvironment.GetNextInt((mum.Genes.Count - 1) - genesSplitted);
                genesSplitted += splitSizes[i];
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
            int size = _randomEnvironment.GetNextInt(Math.Min(6, child.Genes.Count - 1 - from));
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
            int numberOfGenesMuting = _randomEnvironment.GetNextInt(5);
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

        public void NextGeneration()
        {
            int d = 0;
            var bestIndividuals = Population.OrderByDescending(i => i.Fitness).Take(Math.Max(1, Population.Count / 10)).ToList();
            List<DNA> nextGeneration = bestIndividuals;

            while (nextGeneration.Count < Population.Count)
            {
                DNA mum = GetRandomIndividualBasedOnFitness();
                DNA dad = GetRandomIndividualBasedOnFitness();
                nextGeneration.Add(GetChildOf(mum, dad));
            }

            Population = nextGeneration;
            Population.ForEach(i => i.Fitness = 0.0f);
        }

        private DNA GetRandomIndividualBasedOnFitness()
        {
            double rand = _randomEnvironment.GetNextDouble(0.0f, 1f);
            foreach (DNA individual in Population.OrderByDescending(i => i.Fitness))
            {
                double ratio = individual.Fitness / Population.Sum(i => i.Fitness);
                if (rand <= ratio)
                    return individual;
                rand -= ratio;
            }
            return null;
        }

        public void WriteToStream(Stream stream)
        {
            using (StreamWriter sw = new StreamWriter(stream, Encoding.UTF8, 1024, true))
            {
                foreach (DNA individual in Population)
                {
                    sw.WriteLine($"{individual.Fitness}:{individual.ToString()}");
                }
            }
        }

        public void LoadFromStream(Stream stream)
        {
            Population = new List<DNA>();
            using (StreamReader sr = new StreamReader(stream, Encoding.UTF8, true, 1024, true))
            {
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    string[] parts = line.Split(':');
                    DNA individual = new DNA(parts[1]);
                    individual.Fitness = double.Parse(parts[0]);
                    Population.Add(individual);
                }
            }
        }
    }
}
