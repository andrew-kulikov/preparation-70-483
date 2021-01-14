using System;
using System.Diagnostics;
using System.Threading;
using Common;

namespace Debugging.Examples
{
    /// <summary>
    ///     You cannot add new counter to the existing category. Instead need to recreate category
    ///     CounterCreationData -> CounterCreationDataCollection -> PerformanceCounterCategory
    /// </summary>
    public class CounterCreateExample : Example
    {
        private const string CategoryName = "Practice Random numbers";

        private static PerformanceCounter _totalNumbersCounter;
        private static PerformanceCounter _threesPerSecondCounter;

        public CounterCreateExample() : base("Creating performance counter example", "3.1")
        {
        }

        public override void Execute()
        {
            CreateCounters();
            SetupPerformanceCounters();

            var random = new Random();
            while (true)
            {
                var number = random.Next(0, 4);
                _totalNumbersCounter.Increment();

                if (number == 3) _threesPerSecondCounter.Increment();

                var sleep = random.Next(5, 50);
                Thread.Sleep(sleep);
            }
        }

        private static void CreateCounters()
        {
            if (PerformanceCounterCategory.Exists(CategoryName)) return;

            var counters = new[]
            {
                new CounterCreationData("# numbers generated",
                    "number of numbers generated",
                    PerformanceCounterType.NumberOfItems64),
                new CounterCreationData("# threes generated",
                    "number of threes generated per second",
                    PerformanceCounterType.RateOfCountsPerSecond32)
            };

            var counterCollection = new CounterCreationDataCollection(counters);

            PerformanceCounterCategory.Create(CategoryName,
                "Random numbers processing test",
                PerformanceCounterCategoryType.SingleInstance,
                counterCollection);
        }

        private static void SetupPerformanceCounters()
        {
            if (!PerformanceCounterCategory.Exists(CategoryName)) return;

            // production code should use using 
            _totalNumbersCounter = new PerformanceCounter(CategoryName,
                "# numbers generated",
                false);

            // production code should use using 
            _threesPerSecondCounter = new PerformanceCounter(CategoryName,
                "# threes generated",
                false);
        }
    }
}