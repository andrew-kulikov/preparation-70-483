using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Common;

namespace Threading.Examples
{
    /// <summary>
    ///     All actions run in the different threads. First action still runs in the main thread.
    ///     Max default amount of worker threads is 12.
    /// </summary>
    public class ParallelInvokeBigAmountOfActionsExample : Example
    {
        public ParallelInvokeBigAmountOfActionsExample() : base("Parallel.Invoke with very big amount of actions", "1.1")
        {
        }

        public override void Execute()
        {
            Console.WriteLine($"Starting execution from main thread {Thread.CurrentThread.ManagedThreadId}");

            var actions = Enumerable.Range(0, 100)
                .Select(i => new Action(() => Compute(i)))
                .ToArray();

            Parallel.Invoke(actions);
        }

        private void Compute(int actionId)
        {
            Console.WriteLine($"Starting action {actionId} from thread {Thread.CurrentThread.ManagedThreadId}");

            Thread.Sleep(100);

            Console.WriteLine($"Ending action {actionId} from thread {Thread.CurrentThread.ManagedThreadId}");
        }
    }
}