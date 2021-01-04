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
                ;

            exampleSet.RunLast();
        }
    }
}