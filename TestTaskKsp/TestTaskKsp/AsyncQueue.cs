using System.Collections.Generic;
using System.Threading.Tasks;

namespace TestTaskKsp
{
    public class AsyncQueue<T>
    {
        private readonly object _lock = new { };
        private readonly Queue<T> _dataQueue;
        private readonly Queue<TaskCompletionSource<T>> _queryQueue;

        public AsyncQueue()
        {
            _queryQueue = new Queue<TaskCompletionSource<T>>();
            _dataQueue = new Queue<T>();
        }

        public void Push(T data)
        {
            lock (_lock)
            {
                if (_queryQueue.Count > 0)
                {
                    var taskCompletionSource = _queryQueue.Dequeue();
                    taskCompletionSource.SetResult(data);
                }
                else
                {
                    _dataQueue.Enqueue(data);
                }
            }
        }

        public T Pop()
        {
            TaskCompletionSource<T> taskCompletionSource;
            lock (_lock)
            {
                if (_dataQueue.Count > 0)
                {
                    return _dataQueue.Dequeue();
                }
                else
                {
                    taskCompletionSource = new TaskCompletionSource<T>();
                    _queryQueue.Enqueue(taskCompletionSource);
                }
            }

            return taskCompletionSource.Task.Result;
        }
    }
}