using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            Fails = 100;
        }

        public override KnapsackSolutionVector RandomSolution()
        {
            var vector = new KnapsackSolutionVector(this);

            for (var i = 0; i < ProblemSize; i++)
            {
                vector[i] = Randomizer.Next(0, 2);
            }

            while (Math.Abs(vector.Value) < 0.0001 && vector.Data.Any(d => d > 0))
            {
                var indexes = vector.Data.Select((value, index) => new {Value = value, Index = index})
                    .Where(c => c.Value == 1).Select(x => x.Index);
                var enumerable = indexes as IList<int> ?? indexes.ToList();
                var i = Randomizer.Next(enumerable.Count());
                var j = enumerable.ElementAt(i);
                vector[j] = 0;
            }

            return vector;
        }

        public KnapsackSolutionVector Solve(List<KnapsackSolutionVector> population)
        {
            population = population.ToList();

            population.Sort();
            population.Reverse();

            var fails = 0;

            while (fails < Fails)
            {
                // Trace.WriteLine("Fails: " + fails + ", Vector: " + population.First());

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
                    if (child1.Value > 0 && !newPopulation.Contains(child1))
                    {
                        newPopulation.Add(child1);
                    }
                    if (child2.Value > 0 && !newPopulation.Contains(child2))
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

        public int Fails { get; set; }

        public KnapsackSolutionVector SolveAnnealing(KnapsackSolutionVector start)
        {
            var temperature = Fails * Fails;
            var visited = new List<KnapsackSolutionVector>();
            KnapsackSolutionVector tmp = null;
            var i = 1;

            visited.Add(start);
            while (temperature>0)
            {
                //var neighbours = visited[i - 1].GetNeihgbours();
                //var randIndex = Randomizer.Next(neighbours.Count);
                //tmp = (KnapsackSolutionVector)neighbours[randIndex];
                tmp = visited[i - 1].GetRandomNeihgbour();
                if (tmp == null)
                {
                    break;
                }

                if (tmp.CompareTo(visited[i - 1]) > 0)
                {
                    visited.Add(tmp);
                }
                else
                {
                    visited.Add(Randomizer.Next(Fails * Fails) < temperature ? tmp : visited[i - 1]);
                }
                i++;
                temperature--;
            }

            foreach (var solution in visited)
            {
                if (solution.CompareTo(tmp) > 0)
                {
                    tmp = solution;
                }
            }

            return tmp;
        }

    }
}
