using System;
using System.Threading;
using System.Threading.Tasks;
using Common;

namespace Threading.Examples
{
    public class TasksContinuationExample : Example
    {
        public TasksContinuationExample() : base("Task continuation example", "1.1")
        {
        }

        public override void Execute()
        {
            var task = Task.Run(async () => await DoWork(1));

            task.ContinueWith(t => Print($"Completed task {t.Id}"), TaskContinuationOptions.OnlyOnRanToCompletion);
            task.ContinueWith(t => Print($"Failed task {t.Id}"), TaskContinuationOptions.OnlyOnFaulted);
        }

        private async Task DoWork(int i)
        {
            Print($"Starting work {i}");

            await Task.Delay(2000);

            if (new Random().Next() % 2 == 0) throw new Exception();

            Print($"Ending work {i}");
        }

        private void Print(string message)
        {
            Console.WriteLine($"{message} from thread {Thread.CurrentThread.ManagedThreadId}");
        }
    }
}