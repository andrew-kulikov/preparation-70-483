using System;
using System.Threading;
using System.Threading.Tasks;
using Common;

namespace Threading.Examples
{
    public class TasksExample : Example
    {
        public TasksExample() : base("Task simple example", "1.1")
        {
        }
        public override void Execute()
        {
            for (int i = 0; i < 4; i++)
            {
                var iCopy = i;
                Task.Run(() => DoWork(iCopy));
            }
        }

        private async Task DoWork(int i)
        {
            Print($"Starting work {i}");

            await Task.Delay(2000);

            Print($"Ending work {i}");
        }
        private void Print(string message)
        {
            Console.WriteLine($"{message} from thread {Thread.CurrentThread.ManagedThreadId}");
        }
    }
}