﻿using FluentAssertions;
using Moq;
using NeuralNetwork.Genetics;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworkTests.Genetics
{
    [TestFixture]
    class PopulationManagerTests
    {
        private Mock<IRandomEnvironment> MockRandomEnvironment;

        [SetUp]
        public void BeforeEachTest()
        {
            MockRandomEnvironment = new Mock<IRandomEnvironment>();
        }

        private PopulationManager GetPopulationManager()
        {
            return new PopulationManager(MockRandomEnvironment.Object);
        }

        [Test]
        public void GetChildOf_Should_Return_DNA_With_Same_Number_Of_Genes_As_Parents()
        {
            PopulationManager populationManagerTested = GetPopulationManager();

            List<double> fakeGenes = Enumerable.Range(0, 100).Select(i => (double)i).ToList();
            DNA dad = new DNA(String.Join("||", fakeGenes));
            DNA mum = new DNA(String.Join("||", fakeGenes));

            DNA result = populationManagerTested.GetChildOf(mum, dad);

            result.Should().NotBeSameAs(dad);
            result.Should().NotBeSameAs(mum);
            result.Genes.Should().HaveCount(fakeGenes.Count);
        }

        [Test]
        public void GetChildOf_Should_Swap_Two_Genes_When_SwapMutation_Occurs()
        {
            int first = 4;
            int second = 7;
            MockRandomEnvironment.Setup(m => m.DoesSwapMutationOccur()).Returns(true);
            MockRandomEnvironment.SetupSequence(m => m.GetNextInt(It.IsAny<int>()))
                .Returns(first)
                .Returns(second);
            PopulationManager populationManagerTested = GetPopulationManager();

            List<double> fakeGenes = Enumerable.Range(0, 100).Select(i => (double)i).ToList();
            DNA dad = new DNA(String.Join("||", fakeGenes));
            DNA mum = new DNA(String.Join("||", fakeGenes));

            DNA result = populationManagerTested.GetChildOf(mum, dad);

            result.Genes[first].Should().Be(second);
            result.Genes[second].Should().Be(first);
        }

        [Test]
        public void GetChildOf_Should_Adjust_Gene_When_AdaptationMutation_Occurs()
        {
            int geneCount = 3;
            int firstGene = 2;
            int secondGene = 6;
            int thirdGene = 81;
            MockRandomEnvironment.Setup(m => m.DoesAdaptationMutationOccur()).Returns(true);
            MockRandomEnvironment.SetupSequence(m => m.GetNextInt(It.IsAny<int>()))
                .Returns(geneCount)
                .Returns(firstGene)
                .Returns(secondGene)
                .Returns(thirdGene);
            MockRandomEnvironment.SetupSequence(m => m.GetRandomDirection())
                .Returns(Directions.Up)
                .Returns(Directions.Down)
                .Returns(Directions.Up);
            PopulationManager populationManagerTested = GetPopulationManager();

            List<double> fakeGenes = Enumerable.Range(0, 100).Select(i => (double)i).ToList();
            DNA dad = new DNA(String.Join("||", fakeGenes));
            DNA mum = new DNA(String.Join("||", fakeGenes));

            DNA result = populationManagerTested.GetChildOf(mum, dad);

            result.Genes[firstGene].Should().Be(dad.Genes[firstGene] * 1.05f);
            result.Genes[secondGene].Should().Be(dad.Genes[secondGene] * 0.95f);
            result.Genes[thirdGene].Should().Be(dad.Genes[thirdGene] * 1.05f);
        }

        [Test]
        public void GetChildOf_Should_Reverse_Genes_When_InversionMutation_Occurs()
        {
            int fromGene = 2;
            int size = 6;
            MockRandomEnvironment.Setup(m => m.DoesInversionMutationOccur()).Returns(true);
            MockRandomEnvironment.SetupSequence(m => m.GetNextInt(It.IsAny<int>()))
                .Returns(fromGene)
                .Returns(size);
            PopulationManager populationManagerTested = GetPopulationManager();

            List<double> fakeGenes = Enumerable.Range(0, 100).Select(i => (double)i).ToList();
            DNA dad = new DNA(String.Join("||", fakeGenes));
            DNA mum = new DNA(String.Join("||", fakeGenes));

            DNA result = populationManagerTested.GetChildOf(mum, dad);

            result.Genes.Skip(fromGene).Take(size).Should().BeInDescendingOrder();
        }

        [Test]
        public void GetChildOf_Should_Replace_Gene_When_ReplacementMutation_Occurs()
        {
            int gene = 62;
            double replacement = 5.230218415f;
            MockRandomEnvironment.Setup(m => m.DoesReplacementMutationOccur()).Returns(true);
            MockRandomEnvironment.Setup(m => m.GetNextInt(It.IsAny<int>()))
                .Returns(gene);
            PopulationManager populationManagerTested = GetPopulationManager();
            List<double> fakeGenes = Enumerable.Range(0, 100).Select(i => (double)i).ToList();
            DNA dad = new DNA(String.Join("||", fakeGenes));
            DNA mum = new DNA(String.Join("||", fakeGenes));
            MockRandomEnvironment.Setup(m => m.GetNextDouble(dad.Genes[gene] * -2, dad.Genes[gene] * 2))
                .Returns(replacement);

            DNA result = populationManagerTested.GetChildOf(mum, dad);

            result.Genes[gene].Should().Be(replacement);
        }

        [Test]
        public void GetChildOf_Should_Flip_Gene_When_FlipMutation_Occurs()
        {
            int gene = 62;
            MockRandomEnvironment.Setup(m => m.DoesFlipMutationOccur()).Returns(true);
            MockRandomEnvironment.Setup(m => m.GetNextInt(It.IsAny<int>()))
                .Returns(gene);
            PopulationManager populationManagerTested = GetPopulationManager();
            List<double> fakeGenes = Enumerable.Range(0, 100).Select(i => (double)i).ToList();
            DNA dad = new DNA(String.Join("||", fakeGenes));
            DNA mum = new DNA(String.Join("||", fakeGenes));

            DNA result = populationManagerTested.GetChildOf(mum, dad);

            result.Genes[gene].Should().Be(-gene);
        }

        [Test]
        public void GetChildOf_Should_Combine_Alternating_Dad_And_Mum_With_Given_Number_Of_Splits_When_Recombination_Occurs()
        {
            int splits = 3;
            Directions firstDirection = Directions.Down;
            int firstSplitSize = 14;
            int secondSplitSize = 34;
            MockRandomEnvironment.Setup(m => m.DoesRecombinationOccur()).Returns(true);
            MockRandomEnvironment.SetupSequence(m => m.GetNextInt(It.IsAny<int>()))
                .Returns(splits)
                .Returns(firstSplitSize)
                .Returns(secondSplitSize);
            MockRandomEnvironment.Setup(m => m.GetRandomDirection())
                .Returns(firstDirection);
            PopulationManager populationManagerTested = GetPopulationManager();
            List<double> fakeDadGenes = Enumerable.Range(0, 100).Select(i => (double)i).ToList();
            List<double> fakeMumGenes = Enumerable.Range(200, 100).Select(i => (double)i).ToList();
            DNA dad = new DNA(String.Join("||", fakeDadGenes));
            DNA mum = new DNA(String.Join("||", fakeMumGenes));

            DNA result = populationManagerTested.GetChildOf(mum, dad);

            result.Genes.Take(firstSplitSize)
                .Should().BeEquivalentTo(
                    mum.Genes.Take(firstSplitSize)
                );
            result.Genes.Skip(firstSplitSize).Take(secondSplitSize)
                .Should().BeEquivalentTo(
                    dad.Genes.Skip(firstSplitSize).Take(secondSplitSize)
                );
            result.Genes.Skip(firstSplitSize + secondSplitSize)
                .Should().BeEquivalentTo(
                    mum.Genes.Skip(firstSplitSize + secondSplitSize)
                );
        }

        [Test]
        public void GeneratePopulation_Should_Create_Given_Number_Of_Individuals_With_Given_Number_Of_Genes_Each_Between_One_And_Minus_One()
        {
            int individuals = 10;
            int genesEach = 200;
            PopulationManager populationManagerTested = GetPopulationManager();

            populationManagerTested.GeneratePopulation(individuals, genesEach);

            populationManagerTested.Population.Should().HaveCount(individuals);
            populationManagerTested.Population.ForEach(p => p.Genes.Should().HaveCount(genesEach));
            populationManagerTested.Population.ForEach(p => p.Genes.ForEach(g => g.Should().BeInRange(-1, 1)));
        }

        [Test]
        public void NextGeneration_Should_Copy_Ten_Percent_Of_Population_With_Best_Fitness()
        {
            int individuals = 100;
            int genesEach = 20;
            PopulationManager populationManagerTested = GetPopulationManager();
            populationManagerTested.GeneratePopulation(individuals, genesEach);
            double fitness = 0;
            populationManagerTested.Population.ForEach(dna => dna.Fitness = fitness++);
            var oldPopulation = populationManagerTested.Population.ToList();
            var best = oldPopulation.OrderByDescending(dna => dna.Fitness).Take(10).ToList();

            populationManagerTested.NextGeneration();

            populationManagerTested.Population.Should().Contain(best);
        }

        [Test]
        public void NextGeneration_Should_Copy_At_Least_The_Individual_With_Best_Fitness()
        {
            int individuals = 5;
            int genesEach = 20;
            PopulationManager populationManagerTested = GetPopulationManager();
            populationManagerTested.GeneratePopulation(individuals, genesEach);
            double fitness = 0;
            populationManagerTested.Population.ForEach(dna => dna.Fitness = fitness++);
            var oldPopulation = populationManagerTested.Population.ToList();
            var best = oldPopulation.OrderByDescending(dna => dna.Fitness).First();

            populationManagerTested.NextGeneration();

            populationManagerTested.Population.Should().Contain(best);
        }

        [Test]
        public void NextGeneration_Should_Complete_Population_To_Reach_Same_Size_By_Combining_Best()
        {
            int individuals = 5;
            int genesEach = 20;
            MockRandomEnvironment.Setup(m => m.GetNextDouble(0, It.IsAny<double>())).Returns(0.1f);
            PopulationManager populationManagerTested = GetPopulationManager();
            populationManagerTested.GeneratePopulation(individuals, genesEach);
            double fitness = 0;
            populationManagerTested.Population.ForEach(dna => dna.Fitness = fitness++);

            populationManagerTested.NextGeneration();

            populationManagerTested.Population.Should().HaveCount(individuals);
        }

        [Test]
        public void NextGeneration_Should_Set_Fitness_Of_Population_To_Zero()
        {
            int individuals = 5;
            int genesEach = 20;
            MockRandomEnvironment.Setup(m => m.GetNextDouble(0, It.IsAny<double>())).Returns(0.1f);
            PopulationManager populationManagerTested = GetPopulationManager();
            populationManagerTested.GeneratePopulation(individuals, genesEach);
            double fitness = 0;
            populationManagerTested.Population.ForEach(dna => dna.Fitness = fitness++);

            populationManagerTested.NextGeneration();

            populationManagerTested.Population.Select(i => i.Fitness).Should().OnlyContain(i => i == 0.0f);
        }

        [Test]
        public void WriteToStream_Should_Write_All_Population_DNA_To_Given_Stream()
        {
            int individuals = 5;
            int genesEach = 20;
            MockRandomEnvironment.Setup(m => m.GetNextDouble(0, It.IsAny<double>())).Returns(0.1f);
            PopulationManager populationManagerTested = GetPopulationManager();
            populationManagerTested.GeneratePopulation(individuals, genesEach);
            double fitness = 0;
            populationManagerTested.Population.ForEach(dna => dna.Fitness = fitness++);
            MemoryStream stream = new MemoryStream();

            populationManagerTested.WriteToStream(stream);

            stream.Position = 0;
            using (StreamReader sr = new StreamReader(stream))
            {
                int currentIndividual = 0;
                do
                {
                    string line = sr.ReadLine();
                    string expectedLine = $"{populationManagerTested.Population[currentIndividual].Fitness}:{populationManagerTested.Population[currentIndividual].ToString()}";
                    line.Should().Be(expectedLine);
                    currentIndividual++;
                }
                while (currentIndividual < individuals);
            }
        }

        [Test]
        public void LoadFromStream_Should_Get_All_Population_DNA_From_Given_Stream()
        {
            int individuals = 5;
            MockRandomEnvironment.Setup(m => m.GetNextDouble(0, It.IsAny<double>())).Returns(0.1f);
            PopulationManager populationManagerTested = GetPopulationManager();
            MemoryStream stream = new MemoryStream();
            using (StreamWriter sw = new StreamWriter(stream, Encoding.UTF8, 1024, true))
            {
                for (int i = 0; i < individuals; i++)
                {
                    sw.WriteLine($"{i}:{i}:-1||1||-{i}||{i}");
                }
            }
            stream.Position = 0;

            populationManagerTested.LoadFromStream(stream);

            populationManagerTested.Population.Should().HaveCount(individuals);
        }
    }
}
