using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Common;

namespace Threading.Examples
{
    /// <summary>
    ///     8 tasks launched in parallel, 7 failed, then threads were coming one-by one and all next updates were successful
    /// </summary>
    public class ConcurrentDictionaryExample : Example
    {
        private readonly ConcurrentDictionary<int, string> _data = new ConcurrentDictionary<int, string>();

        public ConcurrentDictionaryExample() : base("Concurrent dictionary example", "1.1")
        {
        }

        public override void Execute()
        {
            Task.Run(async () =>
            {
                for (var i = 0; i <= 10; i++)
                {
                    var result = _data.TryAdd(i, $"value_{i}");
                    Console.WriteLine($"- Writing {i}. Result: {result}");
                    //await Task.Delay(100);
                }
            });

            //Console.ReadLine();
            Console.WriteLine("Reading collection");
            Thread.Sleep(100);

            Enumerable.Range(1, 100).ToList().ForEach(i => Task.Run(() =>
            {
                if (_data.TryGetValue(0, out var value))
                {
                    var newValue = $"{value}_{i}";
                    var updateResult = _data.TryUpdate(0, newValue, value);

                    Console.WriteLine($"Trying to update {value} to {newValue}. Result: {updateResult}");
                }
            }));

            Console.ReadLine();
        }

        private void Print(string message)
        {
            Console.WriteLine($"{message} from thread {Thread.CurrentThread.ManagedThreadId}");
        }
    }
}