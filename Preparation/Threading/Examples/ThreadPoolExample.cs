using System;
using System.Threading;
using Common;

namespace Threading.Examples
{
    /// <summary>
    ///     ThreadPool starts to spawn new threads after some time if queue grows
    /// </summary>
    public class ThreadPoolExample : Example
    {
        public ThreadPoolExample() : base("Thread Pool", "1.1")
        {
        }

        public override void Execute()
        {
            while (true)
            {
                ThreadPool.QueueUserWorkItem(Work, "hello1");
                ThreadPool.QueueUserWorkItem(Work, "hello2");
                ThreadPool.QueueUserWorkItem(Work, "hello3");
                ThreadPool.QueueUserWorkItem(Work, "hello4");
                ThreadPool.QueueUserWorkItem(Work, "hello5");
                ThreadPool.QueueUserWorkItem(Work, "hello5");
                ThreadPool.QueueUserWorkItem(Work, "hello5");
                ThreadPool.QueueUserWorkItem(Work, "hello5");
                ThreadPool.QueueUserWorkItem(Work, "hello5");
                ThreadPool.QueueUserWorkItem(Work, "hello5");
                ThreadPool.QueueUserWorkItem(Work, "hello5");

                ThreadPool.GetMinThreads(out var worker, out var completion);
                Console.WriteLine($"Worker: {worker}, completion: {completion}");

                Console.WriteLine(
                    $"Pending: {ThreadPool.PendingWorkItemCount}, Completed: {ThreadPool.CompletedWorkItemCount}");
                Thread.Sleep(50);
            }
        }

        private void Work(object state)
        {
            Thread.Sleep(100);
        }

        private void PrintThreadInfo()
        {
            Print($"Context: {Thread.CurrentThread.ExecutionContext}");
            Print($"Name: {Thread.CurrentThread.Name}");
            Print($"Id: {Thread.CurrentThread.ManagedThreadId}");
            Print($"Priority: {Thread.CurrentThread.Priority}");
            Print($"IsThreadPoolThread: {Thread.CurrentThread.IsThreadPoolThread}");
            Print($"CurrentCulture: {Thread.CurrentThread.CurrentCulture}");
            Print($"CurrentUICulture: {Thread.CurrentThread.CurrentUICulture}");
        }

        private void Print(string message)
        {
            Console.WriteLine($"{message} from thread {Thread.CurrentThread.ManagedThreadId}");
        }
    }
}