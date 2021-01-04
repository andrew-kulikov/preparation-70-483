using System;
using Common;
using ThreadingManagement.Examples;

namespace ThreadingManagement
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var exampleSet = new ExampleSet()
                    .WithExample(new RaceConditionExample())
                    .WithExample(new MonitorExample())
                ;

            exampleSet.RunLast();

            Console.ReadLine();
        }
    }
}