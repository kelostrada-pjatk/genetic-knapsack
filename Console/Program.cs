using GeneticAlgorithm;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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

            var problem = new KnapsackProblem(20, 10, new Knapsack(600.0));

            var population = new List<KnapsackSolutionVector>();

            for (var i = 0; i < problem.PopulationSize; i++)
            {
                population.Add(problem.RandomSolution());
            }

            StreamWriter geneticFile = new StreamWriter("genetic.csv", true);
            StreamWriter annealingFile = new StreamWriter("annealing.csv", true);

            for (var k = 2; k < 100; k+=3)
            {
                for (var j = 100; j >=5; j-=5)
                {
                    problem = new KnapsackProblem(20, j, new Knapsack(600.0));
                    problem.MutationChance = k;

                    population.ForEach(v => v.Problem = problem);
                    population = population.Take(j).ToList();

                    Randomizer.SetSeed(Seed);
                    geneticFile.Write("{0:0.00};", problem.Solve(population).Value);

                    
                    Randomizer.SetSeed(Seed);
                    var solutionAnnealing = problem.SolveAnnealing(population.First()).Value;
                    //System.Console.WriteLine("{0:0.00} ", problem.SolveAnnealing(population.Take(i).Last()).Value);


                    annealingFile.Write("{0:0.00};", solutionAnnealing);

                    System.Console.WriteLine("{0},{1}", k, j);
                }
                geneticFile.WriteLine();
                annealingFile.WriteLine();

                
            }

            geneticFile.Close();
            annealingFile.Close();
            
        }
    }
}
