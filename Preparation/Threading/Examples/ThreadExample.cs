using System;
using System.Threading;
using System.Threading.Tasks;
using Common;

namespace Threading.Examples
{
    public class ThreadExample : Example
    {
        public ThreadExample() : base("Thread simple example", "1.1")
        {
        }
        public override void Execute()
        {
            var start = new ThreadStart(() => DoWork(1));

            var t = new Thread(start);
            t.Start();

            var parameterizedStart = new ParameterizedThreadStart((obj) => DoWork((int) obj));

            var t1 = new Thread(parameterizedStart);
            t1.Start(2);
        }

        private void DoWork(int i)
        {
            Print($"Starting work {i}");

            Thread.Sleep(2000);

            Print($"Ending work {i}");
        }
        private void Print(string message)
        {
            Console.WriteLine($"{message} from thread {Thread.CurrentThread.ManagedThreadId}");
        }
    }
}