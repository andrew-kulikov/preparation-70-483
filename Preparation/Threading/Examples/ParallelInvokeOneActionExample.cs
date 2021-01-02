using System;
using System.Threading;
using System.Threading.Tasks;
using Common;

namespace Threading.Examples
{
    /// <summary>
    ///     First action always runs on the main thread
    /// </summary>
    public class ParallelInvokeOneActionExample : Example
    {
        public ParallelInvokeOneActionExample() : base("Parallel.Invoke one action", "1.1")
        {
        }

        public override void Execute()
        {
            Console.WriteLine($"Starting execution from main thread {Thread.CurrentThread.ManagedThreadId}");

            Parallel.Invoke(Compute);
        }

        private void Compute()
        {
            for (var i = 0; i < 10; i++) Console.WriteLine($"{i} from thread {Thread.CurrentThread.ManagedThreadId}");
        }
    }
}