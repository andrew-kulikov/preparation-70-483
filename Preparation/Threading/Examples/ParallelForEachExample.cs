using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Common;

namespace Threading.Examples
{
    public class ParallelForEachExample : Example
    {
        public ParallelForEachExample() : base("Parallel.Foreach simple example", "1.1")
        {
        }

        public override void Execute()
        {
            Console.WriteLine($"Starting execution from main thread {Thread.CurrentThread.ManagedThreadId}");

            var humans = Enumerable.Range(0, 5).Select(i => new Human(i));

            Parallel.ForEach(humans, Compute);
        }

        private void Compute(Human human)
        {
            Console.WriteLine($"{human.Id} rom thread {Thread.CurrentThread.ManagedThreadId}");
            human.Work();
        }
    }

    public class Human
    {
        public Human(int id)
        {
            Id = id;
        }

        public int Id { get; }

        public void Work()
        {
            Console.WriteLine($"Human #{Id} Working...");
        }
    }
}