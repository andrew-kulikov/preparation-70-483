using System;
using System.Threading;
using System.Threading.Tasks;
using Common;

namespace Threading.Examples
{
    public class ParallelForManageStateExample : Example
    {
        public ParallelForManageStateExample() : base("Parallel.For with state management example", "1.1")
        {
        }

        public override void Execute()
        {
            Console.WriteLine($"Starting execution from main thread {Thread.CurrentThread.ManagedThreadId}");

            var result = Parallel.For(0, 10, Compute);
            Console.WriteLine($"Completed: {result.IsCompleted}. Lowest break iteration: {result.LowestBreakIteration}");
        }

        private void Compute(int i, ParallelLoopState state)
        {
            Console.WriteLine($"{i} from thread {Thread.CurrentThread.ManagedThreadId}");
            state.Break();
        }
    }
}