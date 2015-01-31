using GeneticAlgorithm;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeneticAlgorithm.Knapsack;

namespace Console
{
    class Program
    {
        static void Main(string[] args)
        {
            Randomizer.SetSeed(1000);

            var problem = new KnapsackProblem(50, 50, new Knapsack(1000.0));

            var population = new List<KnapsackSolutionVector>();

            for (var i = 0; i < problem.PopulationSize; i++)
            {
                population.Add(problem.RandomSolution());
            }

            Trace.WriteLine(problem.Solve(population));





        }
    }
}
