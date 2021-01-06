using System;
using Common;

namespace EventsAndCallbacks
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var exampleSet = new ExampleSet()
                ;

            exampleSet.RunLast();

            Console.ReadLine();
        }
    }
}