using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using Common;

namespace Threading.Examples
{
    /// <summary>
    ///     Values got by Peek and Pop can be different
    /// </summary>
    public class ConcurrentStackExample : Example
    {
        private readonly ConcurrentStack<int> _data = new ConcurrentStack<int>();

        public ConcurrentStackExample() : base("Concurrent stack example", "1.1")
        {
        }

        public override void Execute()
        {
            Task.Run(async () =>
            {
                // attempt to add 10 items to the collection - blocks after 5th
                for (var i = 0; i <= 10; i++)
                {
                    _data.Push(i);
                    Console.WriteLine($"- Writing {i}.");
                    await Task.Delay(100);
                }
            });

            //Console.ReadLine();
            Console.WriteLine("Reading collection");

            Task.Run(() =>
            {
                while (true)
                    if (_data.TryPop(out var value))
                        Console.WriteLine($"# Reading {value}.");
            });

            Console.ReadLine();
        }

        private void Print(string message)
        {
            Console.WriteLine($"{message} from thread {Thread.CurrentThread.ManagedThreadId}");
        }
    }
}