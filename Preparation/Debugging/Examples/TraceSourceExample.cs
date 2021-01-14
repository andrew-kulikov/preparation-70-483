using System;
using System.Diagnostics;
using System.IO;
using Common;

namespace Debugging.Examples
{
    public class TraceSourceExample : Example
    {
        private TraceSource _source;

        public TraceSourceExample() : base("Tracing source example", "3.1")
        {
            _source = new TraceSource("source", SourceLevels.All);

            var control = new SourceSwitch("Control", "description") {Level = SourceLevels.All};

            _source.Switch = control;
            _source.Listeners.Add(new ConsoleTraceListener());
        }

        public override void Execute()
        {
            _source.TraceEvent(TraceEventType.Start, 1000);
            _source.TraceEvent(TraceEventType.Warning, 1001);
            _source.TraceEvent(TraceEventType.Verbose, 1002, "verbose");
            _source.TraceData(TraceEventType.Information, 1003, new object[] { "Note 1", "Message 2" });

            Foo();
        }

        public void Foo()
        {
            _source.TraceEvent(TraceEventType.Start, 1005, "Starting Foo");
            Trace.CorrelationManager.StartLogicalOperation();
            Bar();
            Trace.CorrelationManager.StopLogicalOperation();
            _source.TraceEvent(TraceEventType.Stop, 1005, "Ending Foo");
        }

        public void Bar()
        {
            _source.TraceEvent(TraceEventType.Start, 1006, "Starting Bar");

            _source.TraceEvent(TraceEventType.Stop, 1006, "Ending Bar");
        }
    }
}