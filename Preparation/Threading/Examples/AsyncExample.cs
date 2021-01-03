using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Common;

namespace Threading.Examples
{
    public class AsyncExceptionExample : AsyncExample
    {
        public AsyncExceptionExample() : base("Async exceptions example", "1.1")
        {
        }

        public override async Task ExecuteAsync()
        {
            await CatchFirst();
            Console.WriteLine("=========");
            await CatchAll();
        }

        private async Task CatchAll()
        {
            var tasks = GenerateTasks();
            var aggregateTask = Task.WhenAll(tasks);

            try
            {
                await aggregateTask;
            }
            catch
            {
                var allExceptions = aggregateTask.Exception.InnerExceptions;

                Console.WriteLine($"Thrown exceptions: {allExceptions.Count}");

                foreach (var (i, exception) in allExceptions.Enumerate())
                    Console.WriteLine($"Exception #{i}: {exception.Message}");
            }
        }

        private async Task CatchFirst()
        {
            var tasks = GenerateTasks();

            try
            {
                await Task.WhenAll(tasks);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Caught: {e.Message}");
            }
        }

        private IEnumerable<Task> GenerateTasks()
        {
            return Enumerable.Range(0, 10).Select(i => Task.Run(() =>
            {
                if (i % 2 == 0) throw new Exception($"Error with {i}");
            }));
        }


        private void Print(string message)
        {
            Console.WriteLine($"{message} from thread {Thread.CurrentThread.ManagedThreadId}");
        }
    }
}