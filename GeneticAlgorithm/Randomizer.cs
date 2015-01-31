using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithm
{
    public static class Randomizer
    {
        private static Random _random = new Random();

        public static void SetSeed(int seed)
        {
            _random = new Random(seed);
        }

        public static int Next()
        {
            return _random.Next();
        }

        public static int Next(int maxValue)
        {
            return _random.Next(maxValue);
        }

        public static int Next(int minValue, int maxValue)
        {
            return _random.Next(minValue, maxValue);
        }

        public static void NextBytes(byte[] buffer)
        {
            _random.NextBytes(buffer);
        }

        public static double NextDouble()
        {
            return _random.NextDouble();
        }

    }
}
