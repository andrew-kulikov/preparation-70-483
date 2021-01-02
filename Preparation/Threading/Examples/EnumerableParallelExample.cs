using System;
using System.Linq;
using System.Threading;
using Common;

namespace Threading.Examples
{
    /// <summary>
    ///     Can Take break ordering?
    /// </summary>
    public class EnumerableParallelExample : Example
    {
        public EnumerableParallelExample() : base("Enumerable Parallel example", "1.1")
        {
        }

        public override void Execute()
        {
            Console.WriteLine($"Starting execution from main thread {Thread.CurrentThread.ManagedThreadId}");

            var result = Enumerable.Range(0, 10)
                .AsParallel()
                .AsOrdered()
                .Select(Compute)
                .Take(4)
                .ToList();

            foreach (var sum in result) Console.WriteLine($"Result: {sum}");
        }

        private long Compute(int i)
        {
            Console.WriteLine($"{i} from thread {Thread.CurrentThread.ManagedThreadId}");

            var result = 0L;
            for (var j = 0; j <= i; j++) result += j;

            return result;
        }
    }
}