using System;
using System.Threading;
using Common;

namespace Types.Examples
{
    public class Person
    {
        private long[] memory = new long[1000000];
        public string Name { get; set; }
    }

    public class GCExample : Example
    {
        public GCExample() : base("Simple GC example", "2.4")
        {
        }

        public override void Execute()
        {
            var x = new Person();
            for (long i = 0; i < 10000000000; i++)
            {
                var p = new Person();

                p = new Person();
                Console.WriteLine(GC.CollectionCount(0));
                Console.WriteLine(GC.CollectionCount(1));
                Console.WriteLine(GC.CollectionCount(2));
                Console.WriteLine(GC.GetTotalMemory(false));
                Thread.Sleep(20);
            }
        }
    }
}