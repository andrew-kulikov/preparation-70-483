using System;
using System.Threading;
using System.Threading.Tasks;
using Common;

namespace Threading.Examples
{
    public class ParallelForExample : Example
    {
        public ParallelForExample() : base("Parallel.For simple example", "1.1")
        {
        }

        public override void Execute()
        {
            Console.WriteLine($"Starting execution from main thread {Thread.CurrentThread.ManagedThreadId}");

            Parallel.For(0, 10, Compute);
        }

        private void Compute(int i)
        {
            Console.WriteLine($"{i} from thread {Thread.CurrentThread.ManagedThreadId}");
        }
    }
}