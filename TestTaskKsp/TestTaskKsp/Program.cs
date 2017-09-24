using System;
using System.Collections.Generic;
using System.Linq;

namespace TestTaskKsp
{
    class Program
    {
        static void Main()
        {
            Solver.Solve(
                new int[]
                {
                    64, 4, 94, 16, 67, 33, 17, 58, 59, 58, 57, 32, 96, 98, 87, 20, 27, 95, 10, 14, 79, 53, 25, 91, 99, 15,
                    96, 62, 89, 99, 66, 99, 23, 84, 28, 91, 12, 63, 49, 80, 88, 73, 81, 86, 91, 53, 37, 54, 42, 45, 71,
                    6, 75, 27, 39, 11, 38, 82, 16, 18, 69, 23, 40, 73, 91, 56, 86, 66, 89, 68, 51, 3, 56, 47, 50, 5, 62,
                    96, 8, 59, 73, 50, 11, 3, 99, 72, 32, 44, 68, 23, 24, 15, 35, 54, 92, 45, 18, 26, 40, 20
                }, 70);

            Console.ReadLine();
        }
        
        public static void QueueUsageExample()
        {
            var asyncQueue = new AsyncQueue<int>();
            var numbers = new List<int>();
            for (int i = 0; i < 1000; i++)
            {
                numbers.Add(i);
            }

            var random = new Random();
            
            ThreadHelper.TaskExecuter(numbers.OrderBy(x => x), i =>
            {
                var next = random.NextDouble();

                if (next < 0.4)
                {
                    asyncQueue.Push(i);
                }
                else
                {
                    asyncQueue.Pop();
                }
            }, 100);
        }
    }
}