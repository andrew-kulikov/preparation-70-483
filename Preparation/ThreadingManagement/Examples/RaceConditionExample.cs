using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Common;

namespace ThreadingManagement.Examples
{
    /// <summary>
    ///     We can lock only on static variables? Wow
    ///     Code with interlocked sums 2-3 times faster, than with locks
    /// </summary>
    public class RaceConditionExample : Example
    {
        private const int N = 500000000;
        private static long _sharedTotal;
        private static readonly object _locker = new object();

        public RaceConditionExample() : base("Race condition example", "1.2")
        {
        }

        private static void AddWithRaceCondition(int start, int end)
        {
            for (var i = start; i < end; i++) _sharedTotal += i;
        }

        private static void AddWithInterlocked(int start, int end)
        {
            for (var i = start; i < end; i++) Interlocked.Add(ref _sharedTotal, i);
        }

        private static void AddWithLock(int start, int end)
        {
            for (var i = start; i < end; i++)
                lock (_locker)
                {
                    _sharedTotal += i;
                }

            ;
        }

        public override void Execute()
        {
            Measure(() =>
            {
                _sharedTotal = 0;
                Calculate(AddWithRaceCondition);
                Console.WriteLine($"Result with race condition: {_sharedTotal}");
            });

            Measure(() =>
            {
                _sharedTotal = 0;
                Calculate(AddWithInterlocked);
                Console.WriteLine($"Result with interlocked: {_sharedTotal}");
            });

            Measure(() =>
            {
                _sharedTotal = 0;
                Calculate(AddWithLock);
                Console.WriteLine($"Result with lock: {_sharedTotal}");
            });
        }

        private void Measure(Action action)
        {
            var sw = new Stopwatch();

            sw.Start();
            action.Invoke();
            sw.Stop();

            Console.WriteLine($"Execution took time: {sw.Elapsed}\n\n");
        }

        private void Calculate(Action<int, int> add)
        {
            var tasks = new List<Task>();

            var rangeSize = 1000;
            var rangeStart = 0;

            while (rangeStart < N)
            {
                var rangeEnd = Math.Min(rangeStart + rangeSize, N);

                // create local copies of the parameters
                var rs = rangeStart;
                var re = rangeEnd;

                tasks.Add(Task.Run(() => add(rs, re)));

                rangeStart = rangeEnd;
            }

            Task.WaitAll(tasks.ToArray());
        }
    }
}