using NeuralNetwork.Genetics;
using System;
using System.Collections.Generic;
using System.Text;

namespace NeatTacToe
{
    public class YesRandomEnvironment : RandomEnvironment
    {
        public bool DoesAdaptationMutationOccur() => true;
        public bool DoesFlipMutationOccur() => true;

        public bool DoesInversionMutationOccur() => true;

        public bool DoesRecombinationOccur() => true;

        public bool DoesReplacementMutationOccur() => true;

        public bool DoesSwapMutationOccur() => true;
    }
}
