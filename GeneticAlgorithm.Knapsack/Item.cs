using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithm.Knapsack
{
    public class Item
    {
        public Item(double value, double weight)
        {
            Value = value;
            Weight = weight;
        }

        public double Value { get; private set; }
        public double Weight { get; private set; }
    }
}
