using System;
using System.Collections.Generic;

namespace TestTaskKsp
{
    public static class Solver
    {
        public static void Solve(IEnumerable<int> inputNumbers, int n)
        {
            var dictionary = new Dictionary<int, State>();
            foreach (var i in inputNumbers)
            {
                if (dictionary.ContainsKey(n - i) && dictionary[n - i] == State.Unpaired)
                {
                    Console.WriteLine("{0}, {1}", n - i, i);
                    dictionary[n - i] = State.Paired;

                    if (dictionary.ContainsKey(i))
                    {
                        dictionary[i] = State.Paired;
                    }
                    else
                    {
                        dictionary.Add(i, State.Paired);
                    }
                }

                if (!dictionary.ContainsKey(i))
                {
                    dictionary.Add(i, State.Unpaired);
                }
            }
        }

        private enum State
        {
            Paired, Unpaired
        }
    }
}