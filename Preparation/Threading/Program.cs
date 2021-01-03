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
                .WithExample(new ParallelForEachExample())
                .WithExample(new ParallelForManageStateExample())
                .WithExample(new ParallelForManageLocalStateExample())
                .WithExample(new ParallelForManageLocalStatePerformanceExample())
                .WithExample(new EnumerableParallelExample())
                .WithExample(new EnumerableParallelExceptionExample())
                .WithExample(new TasksExample())
                .WithExample(new TasksContinuationExample())
                .WithExample(new TaskChildExample())
                .WithExample(new ThreadExample())
                .WithExample(new ThreadAbortExample())
                .WithExample(new ThreadLocalAndStaticExample())
                .WithExample(new ThreadPoolExample())
                ;

            exampleSet.RunLast();

            Console.ReadLine();
        }
    }
}