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

        private double _value;
        private double _weight;

        protected new void CalculateValue()
        {
            base.CalculateValue();
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
        }

        public override double Value
        {
            get
            {
                if (!ValueCalculated)
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
                if (!ValueCalculated)
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
            Mutation(a,b);
        }

        public void Mutation(int a, int b)
        {
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

        public override List<ISolutionVector<int>> GetNeihgbours()
        {
            var neighbours = new List<ISolutionVector<int>>();

            for (var i = 0; i < Data.Count - 1; i++)
            {
                for (var j = i + 1; j < Data.Count; j++)
                {
                    var solution = Clone() as KnapsackSolutionVector;
                    if (solution == null) continue;
                    solution.Mutation(i, j);
                    if (!neighbours.Contains(solution) && solution.Value > 0)
                    {
                        neighbours.Add(solution);
                    }
                }
            }

            return neighbours;
        }

        public KnapsackSolutionVector GetRandomNeihgbour()
        {
            var i = 1000;
            while (i > 0)
            {
                var neighbour = (KnapsackSolutionVector)Clone();
                var a = Randomizer.Next(0, Data.Count);
                var b = Randomizer.Next(0, Data.Count);
                neighbour.Mutation(a,b);
                if (neighbour.Value > 0)
                {
                    return neighbour;
                }
                i--;
            }
            return null;
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

        public override object Clone()
        {
            return new KnapsackSolutionVector(Data, Problem as KnapsackProblem);
        }

        public override string ToString()
        {
            return String.Join(", ", Data) + ", Value: " + Value + ", Weight: " + Weight;
        }

        public override bool Equals(object obj)
        {
            var vector = obj as KnapsackSolutionVector;
            if (vector == null) return false;
            var equals = true;
            for (var i = 0; i < Data.Count; i++)
            {
                equals &= Data[i] == vector[i];
            }
            return equals;
        }
    }
}
