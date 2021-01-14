using System;
using System.Diagnostics;
using System.Threading;
using Common;

namespace Debugging.Examples
{
    public class CounterReadExample : Example
    {
        public CounterReadExample() : base("Reading performance counter example", "3.1")
        {
        }

        public override void Execute()
        {
            RunDotnetMemory();
        }

        public void RunProcessor()
        {
            var processor = new PerformanceCounter(
                "Processor Information",
                "% Processor Time",
                "_Total");

            Console.WriteLine("Press any key to stop");

            while (true)
            {
                Console.WriteLine("Processor time {0}", processor.NextValue());
                Thread.Sleep(500);
                if (Console.KeyAvailable)
                    break;
            }
        }

        public void RunDotnetMemory()
        {
            var processor = new PerformanceCounter(
                ".NET CLR Memory",
                "# Bytes in all Heaps",
                "_Global_");

            Console.WriteLine("Press any key to stop");

            while (true)
            {
                Console.WriteLine("Memory {0}", processor.NextValue());
                Thread.Sleep(500);
                if (Console.KeyAvailable)
                    break;
            }
        }
    }
}