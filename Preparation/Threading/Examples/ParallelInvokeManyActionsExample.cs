using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Common;

namespace Threading.Examples
{
    /// <summary>
    ///     All actions run in the different threads. First action still runs in the main thread
    /// </summary>
    public class ParallelInvokeManyActionsExample : Example
    {
        public ParallelInvokeManyActionsExample() : base("Parallel.Invoke with multiple actions", "1.1")
        {
        }

        public override void Execute()
        {
            Console.WriteLine($"Starting execution from main thread {Thread.CurrentThread.ManagedThreadId}");

            var actions = Enumerable.Range(0, 5)
                .Select(i => new Action(() => Compute(i)))
                .ToArray();

            Parallel.Invoke(actions);
        }

        private void Compute(int actionId)
        {
            Console.WriteLine($"Starting action {actionId}");

            for (var i = 0; i < 5; i++) Console.WriteLine($"{i} from thread {Thread.CurrentThread.ManagedThreadId}");
        }
    }
}