using System;
using System.Collections.Generic;

namespace GeneticAlgorithm.Knapsack
{
    public class KnapsackSolutionVector : SolutionVector<int>
    {
        public KnapsackSolutionVector(KnapsackProblem problem)
            : base(new List<int>(), problem)
        {
        }

        public KnapsackSolutionVector(IEnumerable<int> data, KnapsackProblem problem)
            : base(data, problem)
        {
        }

        private bool _valueCalculated;
        private double _value;
        private double _weight;

        private void CalculateValue()
        {
            var problem = Problem as KnapsackProblem;
            if (problem == null) throw new Exception("Wrong initialization of KnapsackProblem");
            double value = 0;
            _weight = 0;

            for (var i = 0; i < problem.ProblemSize; i++)
            {
                value += problem.Items[i].Value * Data[i];
                _weight += problem.Items[i].Weight * Data[i];
            }

            _value = _weight <= problem.Knapsack.MaxWeight ? value : 0;
            _valueCalculated = true;
        }

        public override double Value
        {
            get
            {
                if (!_valueCalculated)
                {
                    CalculateValue();
                }
                return _value;
            }
        }

        public double Weight
        {
            get
            {
                if (!_valueCalculated)
                {
                    CalculateValue();
                }
                return _weight;
            }
        }

        public override void Mutation()
        {
            var a = Randomizer.Next(0, Data.Count);
            var b = Randomizer.Next(0, Data.Count);
            var temp = Data[a];
            Data[a] = Data[b];
            Data[b] = temp;
        }
        
        public override ISolutionVector<int> Crossover(ISolutionVector<int> vector)
        {
            var child = new KnapsackSolutionVector(Problem as KnapsackProblem);
            var index = Randomizer.Next(0, Data.Count);

            for (var i = 0; i < Data.Count; i++)
            {
                child.Data.Add(i < index ? Data[i] : vector[i]);
            }

            return child;
        }
        
        /// <summary>
        /// Greater means better solution
        /// </summary>
        public override int CompareTo(object obj)
        {
            var solutionVector = obj as KnapsackSolutionVector;
            if (solutionVector == null) throw new Exception("Cannot compare KnapsackSolutionVector to any other type");
            if (Value > solutionVector.Value)
            {
                return 1;
            }
            else if (Math.Abs(Value - solutionVector.Value) < 0.0001)
            {
                return solutionVector.Weight.CompareTo(Weight);
            }
            else
            {
                return -1;
            }
        }

        public override string ToString()
        {
            return String.Join(", ", Data) + ", Value: " + Value + ", Weight: " + Weight;
        }
    }
}
