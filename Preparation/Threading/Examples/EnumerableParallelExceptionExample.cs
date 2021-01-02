using System;
using System.Linq;
using System.Threading;
using Common;

namespace Threading.Examples
{
    /// <summary>
    ///     Can Take break ordering?
    /// </summary>
    public class EnumerableParallelExceptionExample : Example
    {
        public EnumerableParallelExceptionExample() : base("Enumerable Parallel example", "1.1")
        {
        }

        public override void Execute()
        {
            Console.WriteLine($"Starting execution from main thread {Thread.CurrentThread.ManagedThreadId}");

            try
            {
                var result = Enumerable.Range(0, 10)
                    .AsParallel()
                    .Select(Compute)
                    .ToList();

                foreach (var sum in result) Console.WriteLine($"Result: {sum}");
            }
            catch (AggregateException e)
            {
                Console.WriteLine($"Thrown exceptions: {e.InnerExceptions.Count}");
            }
        }

        private long Compute(int i)
        {
            Console.WriteLine($"{i} from thread {Thread.CurrentThread.ManagedThreadId}");

            var result = 0L;
            for (var j = 0; j <= i; j++)
            {
                if (j == 4) throw new Exception("asdasd");
                result += j;
            }

            return result;
        }
    }
}