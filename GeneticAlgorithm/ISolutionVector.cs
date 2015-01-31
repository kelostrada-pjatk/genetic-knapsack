using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithm
{
    public interface ISolutionVector<T> : IComparable, ICloneable
    {
        double Value { get; }
        void Mutation();
        ISolutionVector<T> Crossover(ISolutionVector<T> vector);
        List<ISolutionVector<T>> GetNeihgbours();
        T this[int i] { get; set; }
        IGeneticProblem<ISolutionVector<T>, T> Problem { get; } 

    }
}
