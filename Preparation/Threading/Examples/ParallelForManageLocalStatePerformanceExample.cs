using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Common;

namespace Threading.Examples
{
    public class ParallelForManageLocalStatePerformanceExample : Example
    {
        public ParallelForManageLocalStatePerformanceExample() : base(
            "Parallel.For performance comparison vs single thread by calculating sum", "1.1")
        {
        }

        public override void Execute()
        {
            BenchmarkRunner.Run<LoopsBenchmark>();
        }
    }

    public class LoopsBenchmark
    {
        private long _total;

        [Params(0L, 100, 1000, 100_000, 10_000_000, 1_000_000_000)]
        public int N;

        [Benchmark]
        public long CalculateWithParallel()
        {
            Parallel.For(0, N, () => 0L, CalculateSum, result => Interlocked.Add(ref _total, result));

            return _total;
        }

        [Benchmark]
        public long CalculateWithLoop()
        {
            var result = 0L;

            for (long i = 0; i < N; i++) result += i;

            return result;
        }

        [Benchmark]
        public long CalculateWithEnumerable()
        {
            return Enumerable.Range(0, N).Sum(i => (long) i);
        }

        private long CalculateSum(int i, ParallelLoopState state, long subtotal)
        {
            return subtotal + i;
        }
    }
}