using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using Common;

namespace Threading.Examples
{
    /// <summary>
    ///     Amount of items in collection cannot be greater than value provided.
    ///     If someone wants to write it will block until someone read data
    /// </summary>
    public class BlockingStackExample : Example
    {
        private readonly BlockingCollection<int> _data = new BlockingCollection<int>(new ConcurrentStack<int>(), 4);

        public BlockingStackExample() : base("Blocking collection with concurrent stack example", "1.1")
        {
        }

        public override void Execute()
        {
            Task.Run(() =>
            {
                // attempt to add 10 items to the collection - blocks after 5th
                for (var i = 0; i <= 10; i++)
                {
                    _data.Add(i);
                    Console.WriteLine($"Writing {i}.");
                }

                _data.CompleteAdding();
            });

            Console.ReadLine();
            Console.WriteLine("Reading collection");

            Task.Run(() =>
            {
                while (!_data.IsCompleted)
                    if (_data.TryTake(out var value))
                    {
                        Console.WriteLine($"Reading {value}.");
                    }
            });

            Console.ReadLine();
        }

        private void Print(string message)
        {
            Console.WriteLine($"{message} from thread {Thread.CurrentThread.ManagedThreadId}");
        }
    }
}