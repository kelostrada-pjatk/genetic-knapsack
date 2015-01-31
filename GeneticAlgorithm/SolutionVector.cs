using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithm
{
    public abstract class SolutionVector<T> : ISolutionVector<T>
    {
        public abstract double Value { get; }
        public abstract void Mutation();
        public abstract ISolutionVector<T> Crossover(ISolutionVector<T> vector);

        public T this[int i]
        {
            get { return Data[i]; }
            set { Data.Insert(i, value); }
        }

        public IGeneticProblem<ISolutionVector<T>, T> Problem { get; protected set; }

        public abstract int CompareTo(object obj);

        protected IList<T> Data { get; private set; }

        protected SolutionVector(IEnumerable<T> data, IGeneticProblem<ISolutionVector<T>, T> problem)
        {
            Data = new List<T>(data);
            Problem = problem;
        }
        
    }
}
