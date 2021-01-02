using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Common;

namespace Threading.Examples
{
    /// <summary>
    ///     https://stackoverflow.com/questions/42296364/understanding-parallel-invoke-creation-and-reusing-of-threads
    ///     Tasks are cancelled not as we expect, because main thread is blocked.
    ///     There is significant delay between cancellation request and exception!
    /// </summary>
    public class ParallelInvokeConfigurationExample : Example
    {
        public ParallelInvokeConfigurationExample() : base("Parallel.Invoke with configuration provided", "1.1")
        {
        }

        public override void Execute()
        {
            Console.WriteLine($"Starting execution from main thread {Thread.CurrentThread.ManagedThreadId}");
            Console.WriteLine($"Maximum concurrency level {TaskScheduler.Current.MaximumConcurrencyLevel}");

            var actions = Enumerable.Range(0, 100)
                .Select(i => new Action(() => Compute(i)))
                .ToArray();

            var cts = new CancellationTokenSource();

            _ = Task.Run(async () =>
            {
                Console.WriteLine($"Starting waiting cancellation from thread {Thread.CurrentThread.ManagedThreadId}");
                await Task.Delay(200);

                Console.WriteLine($"Woke up, cancelling...  {Thread.CurrentThread.ManagedThreadId}");
                cts.Cancel(false);
            });


            Parallel.Invoke(new ParallelOptions
            {
                MaxDegreeOfParallelism = 1000,
                CancellationToken = cts.Token
            }, actions);

            //Thread.Sleep(200);
            //Console.WriteLine("Woke up, cancelling...");
            //cts.Cancel();
        }

        private void Compute(int actionId)
        {
            Console.WriteLine($"Starting action {actionId} from thread {Thread.CurrentThread.ManagedThreadId}");

            Thread.Sleep(1000);

            Console.WriteLine($"Ending action {actionId} from thread {Thread.CurrentThread.ManagedThreadId}");
        }
    }
}