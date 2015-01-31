using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithm.Knapsack
{
    public class KnapsackProblem : GeneticProblem<KnapsackSolutionVector, int>
    {
        public List<Item> Items { get; private set; }
        public Knapsack Knapsack { get; private set; }

        public KnapsackProblem(int problemSize, int populationSize, Knapsack knapsack)
            : base(problemSize, populationSize)
        {
            Items = new List<Item>();

            for (var i = 0; i < problemSize; i++)
            {
                Items.Add(new Item(Randomizer.NextDouble()*100, Randomizer.NextDouble()*100));
            }

            Knapsack = knapsack;
        }

        public override KnapsackSolutionVector RandomSolution()
        {
            var vector = new KnapsackSolutionVector(this);

            for (var i = 0; i < ProblemSize; i++)
            {
                vector[i] = Randomizer.Next(0, 2);
            }

            return vector;
        }

        public KnapsackSolutionVector Solve(List<KnapsackSolutionVector> population)
        {
            population.Sort();
            population.Reverse();

            var fails = 0;

            while (fails < 100)
            {
                var newPopulation = new List<KnapsackSolutionVector>();
                newPopulation.AddRange(population.Take(2));

                for (var i = 0; i < PopulationSize; i++)
                {
                    var x = Randomizer.Next(population.Count);
                    var y = Randomizer.Next(population.Count);
                    var isMutating = Randomizer.Next(100) < 5;

                    var child1 = population[x].Crossover(population[y]) as KnapsackSolutionVector;
                    var child2 = population[y].Crossover(population[x]) as KnapsackSolutionVector;
                    if (child1 == null || child2 == null) throw new InvalidCastException("Cannot cast the solution to KnapsackSolutionVector");
                    if (isMutating)
                    {
                        child1.Mutation();
                        child2.Mutation();
                    }
                    if (child1.Value > 0)
                    {
                        newPopulation.Add(child1);
                    }
                    if (child2.Value > 0)
                    {
                        newPopulation.Add(child2);
                    }
                }

                newPopulation.Sort();
                newPopulation.Reverse();

                if (population.First().CompareTo(newPopulation.First()) == 0)
                {
                    fails++;
                }
                else
                {
                    fails = 0;
                }

                population = newPopulation;
            }

            return population.First();
        }

    }
}
