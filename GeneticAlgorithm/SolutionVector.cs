using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithm
{
    public abstract class SolutionVector<T> : ISolutionVector<T>
    {

        protected bool ValueCalculated;
        protected void CalculateValue()
        {
            ValueCalculated = true;
        }
        public abstract double Value { get; }
        public abstract void Mutation();
        public abstract ISolutionVector<T> Crossover(ISolutionVector<T> vector);
        public abstract List<ISolutionVector<T>> GetNeihgbours();

        public T this[int i]
        {
            get { return Data[i]; }
            set
            {
                if (Data.Count > i)
                {
                    Data[i] = value;
                }
                else
                {
                    Data.Insert(i, value);
                }
                ValueCalculated = false;
            }
        }

        public IGeneticProblem<ISolutionVector<T>, T> Problem { get; protected set; }

        public abstract int CompareTo(object obj);

        public IList<T> Data { get; private set; }

        protected SolutionVector(IEnumerable<T> data, IGeneticProblem<ISolutionVector<T>, T> problem)
        {
            Data = new List<T>(data);
            Problem = problem;
        }

        public abstract object Clone();
    }
}
