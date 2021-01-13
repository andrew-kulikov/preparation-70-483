#define KEK
#if TRACE
#undef TRACE
#endif

using System;
using System.Diagnostics;
using Common;

namespace Types.Examples
{
    public class ConditionalExample : Example
    {
        public ConditionalExample() : base("Conditional attribute", "2.4")
        {
        }

        public override void Execute()
        {
#if TRACE
            Console.WriteLine("Trace defined");
#endif
            Trace();
            Debug();
            Release();
            Kek();
        }

        [Conditional("TRACE")]
        public void Trace()
        {
            Console.WriteLine("Trace");
        }

        [Conditional("DEBUG")]
        public void Debug()
        {
            Console.WriteLine("Debug");
        }

        [Conditional("RELEASE")]
        public void Release()
        {
            Console.WriteLine("Release");
        }

        [Conditional("KEK")]
        public void Kek()
        {
            Console.WriteLine("Kek");
        }
    }
}