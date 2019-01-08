using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NeuralNetwork.Genetics
{
    public class DNA 
    {
        public DNA(string geneString)
        {
            Genes = geneString.Split(new[] { "||" }, StringSplitOptions.None).Select(s => double.Parse(s)).ToList();
        }

        public List<double> Genes { get; set; }
        public double Fitness { get; set; }

        public override string ToString() => String.Join("||", Genes);
    }
}
