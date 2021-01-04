using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Common;

namespace ThreadingManagement.Examples
{
    /// <summary>
    ///     Don't forget to release monitor in case of exceptions
    /// </summary>
    public class MonitorExample : AsyncExample
    {
        private static readonly object _locker = new object();
        private static readonly Random _rnd = new Random(); 

        public MonitorExample() : base("Monitor example", "1.2")
        {
        }

        private static Action Random()
        {
            return _rnd.Next() % 2 == 0 ? new Action(DoWork) : new Action(DoWork1);
        }

        private static void DoWork1()
        {
            if (!Monitor.TryEnter(_locker))
            {
                Console.WriteLine($"Oh shit, cannot do another work");
                return;
            }

            Console.WriteLine($"Working from another task {Task.CurrentId}");

            Thread.Sleep(100);

            // if not exit we get deadlock :)
            Monitor.Exit(_locker);
        }

        private static void DoWork()
        {
            Thread.Sleep(100);

            Monitor.Enter(_locker);

            Console.WriteLine($"Working from task {Task.CurrentId}");

            Monitor.Exit(_locker);
        }


        public override async Task ExecuteAsync()
        {
            var tasks = Enumerable.Range(0, 10).Select(i => Task.Run(Random()));

            await Task.WhenAll(tasks);
        }
    }
}