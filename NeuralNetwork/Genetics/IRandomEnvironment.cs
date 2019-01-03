using System;
using System.Collections.Generic;
using System.Text;

namespace NeuralNetwork.Genetics
{
    public enum Directions
    {
        Up,
        Down
    };

    public interface IRandomEnvironment
    {
        bool DoesSwapMutationOccur();
        bool DoesAdaptationMutationOccur();
        int GetNextInt(int max);
        Directions GetRandomDirection();
        bool DoesInversionMutationOccur();
        bool DoesReplacementMutationOccur();
        double GetNextDouble(double min, double max);
        bool DoesRecombinationOccur();
        bool DoesFlipMutationOccur();
    }
}
