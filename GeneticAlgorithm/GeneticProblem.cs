using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithm
{
    public abstract class GeneticProblem<TSolutionVector, T> : IGeneticProblem<TSolutionVector, T> where TSolutionVector : class, ISolutionVector<T>
    {
        public int PopulationSize { get; private set; }
        public int ProblemSize { get; private set; }

        protected GeneticProblem(int problemSize, int populationSize)
        {
            ProblemSize = problemSize;
            PopulationSize = populationSize;
        }

        public abstract TSolutionVector RandomSolution();

        
    }
}
