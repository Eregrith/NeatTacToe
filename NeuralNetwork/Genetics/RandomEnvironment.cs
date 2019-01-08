using System;
using System.Collections.Generic;
using System.Text;

namespace NeuralNetwork.Genetics
{
    public class RandomEnvironment : IRandomEnvironment
    {
        private readonly Random _rand;

        private const double AdaptationChance = 0.02f;

        private const double FlipChance = 0.02f;

        private const double InversionChance = 0.02f;

        private const double ReplacementChance = 0.01f;

        private const double SwapChance = 0.02f;

        public RandomEnvironment()
        {
            _rand = new Random(DateTime.Now.Millisecond);
        }

        public virtual bool DoesRecombinationOccur() => true;

        public virtual bool DoesAdaptationMutationOccur() => _rand.NextDouble() >= AdaptationChance;

        public virtual bool DoesFlipMutationOccur() => _rand.NextDouble() >= FlipChance;

        public virtual bool DoesInversionMutationOccur() => _rand.NextDouble() >= InversionChance;

        public virtual bool DoesReplacementMutationOccur() => _rand.NextDouble() >= ReplacementChance;

        public virtual bool DoesSwapMutationOccur() => _rand.NextDouble() >= SwapChance;

        public double GetNextDouble(double min, double max) => min + _rand.NextDouble() * (max - min);

        public int GetNextInt(int max) => _rand.Next(0, max);

        public Directions GetRandomDirection() => _rand.NextDouble() >= 0.5f ? Directions.Up : Directions.Down;
    }
}
