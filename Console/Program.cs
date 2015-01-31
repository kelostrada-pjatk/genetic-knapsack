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
        private const int Seed = 1000;

        static void Main(string[] args)
        {
            Randomizer.SetSeed(Seed);

            var problem = new KnapsackProblem(50, 100, new Knapsack(1000.0));

            var population = new List<KnapsackSolutionVector>();

            for (var i = 0; i < problem.PopulationSize; i++)
            {
                population.Add(problem.RandomSolution());
            }

            Randomizer.SetSeed(Seed);
            System.Console.WriteLine(problem.Solve(population));

            for (var i = 1; i < problem.PopulationSize; i++)
            {
                Randomizer.SetSeed(Seed);
                System.Console.WriteLine(problem.SolveAnnealing(population.Take(i).Last()));
            }
            



        }
    }
}
