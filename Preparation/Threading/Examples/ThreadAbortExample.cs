using System;
using System.Threading;
using Common;

namespace Threading.Examples
{
    /// <summary>
    /// Thread.Abort throws an exception in .NET Core -> use Interrupt instead
    /// </summary>
    public class ThreadAbortExample : Example
    {
        public ThreadAbortExample() : base("Thread abort", "1.1")
        {
        }

        public override void Execute()
        {
            var t = new Thread(() =>
            {
                while (true)
                {
                    Console.WriteLine("Working...");
                    Thread.Sleep(1000);
                }
            });

            t.Start();

            Console.WriteLine("Press any key to KILL HIM");
            Console.ReadLine();

            //t.Abort();
            t.Interrupt();
        }

        private void Print(string message)
        {
            Console.WriteLine($"{message} from thread {Thread.CurrentThread.ManagedThreadId}");
        }
    }
}