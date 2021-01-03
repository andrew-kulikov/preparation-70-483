using System;
using System.Threading;
using System.Threading.Tasks;
using Common;

namespace Threading.Examples
{
    /// <summary>
    ///     Be aware of passing async tasks to factory, because in this case parent wll not wait
    ///     https://docs.microsoft.com/en-us/dotnet/standard/parallel-programming/attached-and-detached-child-tasks
    ///     By default all tasks are created with TaskCreationOptions.DenyChildAttach
    /// </summary>
    public class TaskChildExample : Example
    {
        public TaskChildExample() : base("Task child example", "1.1")
        {
        }

        public override void Execute()
        {
            Task.Factory.StartNew(DoWork).Wait();

            Console.WriteLine("Execution completed");
        }

        private void DoWork()
        {
            Print("Starting work");

            for (var i = 0; i < 5; i++) Task.Factory.StartNew(DoChildWork, TaskCreationOptions.AttachedToParent);

            Print("Ending work");
        }

        private void DoChildWork()
        {
            Print("Starting child work");

            Thread.Sleep(2000);

            Print("Ending child work");
        }

        private void Print(string message)
        {
            Console.WriteLine($"{message} from thread {Thread.CurrentThread.ManagedThreadId}");
        }
    }
}