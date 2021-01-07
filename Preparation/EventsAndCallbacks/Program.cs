using System;
using Common;
using EventsAndCallbacks.Examples;

namespace EventsAndCallbacks
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var exampleSet = new ExampleSet()
                    .WithExample(new DelegateSubscriptionExample())
                ;

            exampleSet.RunLast();

            Console.ReadLine();
        }
    }
}