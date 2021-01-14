using System;
using System.Diagnostics;
using System.IO;
using Common;

namespace Debugging.Examples
{
    public class TraceExample : Example
    {
        public TraceExample() : base("Tracing example", "3.1")
        {
        }

        public override void Execute()
        {
            var writer = new StringWriter();

            Trace.Listeners.Add(new ConsoleTraceListener(true));
            Trace.Listeners.Add(new TextWriterTraceListener(writer));

            Trace.WriteLine("Dorova");

            Console.WriteLine(writer.ToString());
        }
    }
}