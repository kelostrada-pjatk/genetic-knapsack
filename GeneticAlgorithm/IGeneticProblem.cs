using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithm
{
    public interface IGeneticProblem<out TSolutionVector, T> where TSolutionVector : class, ISolutionVector<T>
    {
        int PopulationSize { get; }
        int ProblemSize { get; }
        TSolutionVector RandomSolution();
    }
}
