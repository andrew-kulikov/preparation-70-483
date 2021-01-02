using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Common;

namespace Threading.Examples
{
    public class ParallelForManageLocalStatePerformanceExample : Example
    {
        private const int Limit = 1_000_000_000;
        private long _total;

        public ParallelForManageLocalStatePerformanceExample() : base(
            "Parallel.For performance comparison vs single thread by calculating sum", "1.1")
        {
        }

        public override void Execute()
        {
            Measure(CalculateWithParallel);
            Measure(CalculateWithLoop);
            Measure(CalculateWithEnumerable);
        }

        private void CalculateWithParallel()
        {
            var result = Parallel.For(0, Limit, () => 0L, CalculateSum, LocalFinally);

            Console.WriteLine($"Calculated total with parallel: {_total}");
        }

        private void CalculateWithLoop()
        {
            var result = 0L;

            for (long i = 0; i < Limit; i++) result += i;

            Console.WriteLine($"Calculated total with loop: {result}");
        }

        private void CalculateWithEnumerable()
        {
            var result = Enumerable.Range(0, Limit).Sum(i => (long)i);

            Console.WriteLine($"Calculated total with enumerable: {result}");
        }

        private void Measure(Action action)
        {
            var sw = new Stopwatch();

            sw.Start();
            action.Invoke();
            sw.Stop();

            Console.WriteLine($"Elapsed time: {sw.Elapsed}");
        }

        private void LocalFinally(long result)
        {
            Interlocked.Add(ref _total, result);
        }

        private long CalculateSum(int i, ParallelLoopState state, long subtotal)
        {
            return subtotal + i;
        }
    }
}