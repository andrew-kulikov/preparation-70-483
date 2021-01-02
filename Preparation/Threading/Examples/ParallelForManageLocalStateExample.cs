using System;
using System.Threading;
using System.Threading.Tasks;
using Common;

namespace Threading.Examples
{
    /// <summary>
    ///     Initializes local state for all threads! that will perform work.
    /// https://docs.microsoft.com/en-us/dotnet/standard/parallel-programming/how-to-write-a-parallel-foreach-loop-with-partition-local-variables
    /// </summary>
    public class ParallelForManageLocalStateExample : Example
    {
        private long _total;

        public ParallelForManageLocalStateExample() : base(
            "Parallel.For with local delegates and state management example", "1.1")
        {
        }

        public override void Execute()
        {
            Console.WriteLine($"Starting execution from main thread {Thread.CurrentThread.ManagedThreadId}");

            var result = Parallel.For(0, 1001, LocalInit, CalculateSum, LocalFinally);

            Console.WriteLine(
                $"Completed: {result.IsCompleted}. Lowest break iteration: {result.LowestBreakIteration}");

            Console.WriteLine($"Calculated total: {_total}");
        }

        private void LocalFinally(long result)
        {
            Console.WriteLine($"Result from thread {Thread.CurrentThread.ManagedThreadId}: {result}");

            Interlocked.Add(ref _total, result);
        }

        private long LocalInit()
        {
            Console.WriteLine($"Initializing state from thread {Thread.CurrentThread.ManagedThreadId}");

            return 0;
        }

        private long CalculateSum(int i, ParallelLoopState state, long subtotal)
        {
            Console.WriteLine($"{i} from thread {Thread.CurrentThread.ManagedThreadId}");

            return subtotal + i;
        }
    }
}