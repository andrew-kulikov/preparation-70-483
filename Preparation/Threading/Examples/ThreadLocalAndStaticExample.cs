using System;
using System.Threading;
using Common;

namespace Threading.Examples
{
    /// <summary>
    ///     [ThreadStatic] calls field initialize only for the first thread, all next will get default value
    /// </summary>
    public class ThreadLocalAndStaticExample : Example
    {
        [ThreadStatic] private static readonly string _static = "I'm static!";

        private static readonly ThreadLocal<string> _local = new ThreadLocal<string>(() => "I'm local!");

        public ThreadLocalAndStaticExample() : base("Thread Local And Static Example comparison", "1.1")
        {
        }

        public override void Execute()
        {
            var t = new Thread(Work);
            t.Start();

            var t1 = new Thread(Work);
            t1.Start();
        }

        private void Work()
        {
            Print("Working...");

            Print($"Static: {_static}");
            Print($"Local: {_local}");

            Thread.Sleep(1000);
            Print("Stopping...");
        }

        private void Print(string message)
        {
            Console.WriteLine($"{message} from thread {Thread.CurrentThread.ManagedThreadId}");
        }
    }
}