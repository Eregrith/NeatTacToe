using FluentAssertions;
using NeuralNetwork.Genetics;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NeuralNetworkTests.Genetics
{
    [TestFixture]
    class DNATests
    {
        [Test]
        public void Constructor_Should_Create_DNA_With_Genes_From_String()
        {
            List<double> expectedGenes = Enumerable.Range(0, 100).Select(i => (double)i).ToList();
            DNA dnaTested = new DNA(String.Join("||", expectedGenes));

            dnaTested.Genes.Should().HaveCount(expectedGenes.Count);
        }
    }
}
