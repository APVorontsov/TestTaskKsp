using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestTaskKsp
{
    public static class ThreadHelper
    {
        public static void TaskExecuter<TSource>(IEnumerable<TSource> items, Action<TSource> action, int threads)
        {
            var tasks = items.Select(x => new Task(() => { action(x); }));
            DoTasksParallel(tasks, threads);
        }

        public static void DoTasksParallel(IEnumerable<Task> tasks, int countOfParallesTreads)
        {
            if (!tasks.Any())
            {
                return;
            }

            var workers = new List<Task>();
            var tasksStack = new ConcurrentStack<Task>();
            tasksStack.PushRange(tasks.Reverse().ToArray());
            for (var i = 0; i < countOfParallesTreads; i++)
            {
                var worker = new Task(() =>
                {
                    Task result;
                    while (tasksStack.Any() && tasksStack.TryPop(out result))
                    {
                        result.RunSynchronously();
                    }
                });

                workers.Add(worker);
                worker.Start();
            }

            Task.WaitAll(workers.ToArray());
        }
    }
}