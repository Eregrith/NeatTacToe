using System;
using System.Collections.Generic;
using System.Text;

namespace NeuralNetwork.Genetics
{
    public class RandomEnvironment : IRandomEnvironment
    {
        private readonly Random _rand;

        private const double AdaptationChance = 0.10f;

        private const double FlipChance = 0.02f;

        private const double InversionChance = 0.10f;

        private const double ReplacementChance = 0.05f;

        private const double SwapChance = 0.05f;

        public RandomEnvironment()
        {
            _rand = new Random(DateTime.Now.Millisecond);
        }

        public bool DoesRecombinationOccur() => true;

        public bool DoesAdaptationMutationOccur() => _rand.NextDouble() >= AdaptationChance;

        public bool DoesFlipMutationOccur() => _rand.NextDouble() >= FlipChance;

        public bool DoesInversionMutationOccur() => _rand.NextDouble() >= InversionChance;

        public bool DoesReplacementMutationOccur() => _rand.NextDouble() >= ReplacementChance;

        public bool DoesSwapMutationOccur() => _rand.NextDouble() >= SwapChance;

        public double GetNextDouble(double min, double max) => min + _rand.NextDouble() * (max - min);

        public int GetNextInt(int max) => _rand.Next(0, max);

        public Directions GetRandomDirection() => _rand.NextDouble() >= 0.5f ? Directions.Up : Directions.Down;
    }
}
