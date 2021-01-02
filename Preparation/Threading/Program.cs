using System;
using Common;
using Threading.Examples;

namespace Threading
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var exampleSet = new ExampleSet()
                .WithExample(new ParallelInvokeOneActionExample())
                .WithExample(new ParallelInvokeManyActionsExample())
                .WithExample(new ParallelInvokeBigAmountOfActionsExample())
                .WithExample(new ParallelInvokeConfigurationExample())
                .WithExample(new ParallelForExample())
                .WithExample(new ParallelForEachExample(), true);

            exampleSet.Run();

            Console.ReadLine();
        }
    }
}