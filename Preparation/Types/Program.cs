using System;
using Common;
using Types.Examples;

namespace Types
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var examples = new ExampleSet()
                    .WithExample(new CodeDomExample())
                    .WithExample(new ConditionalExample())
                    .WithExample(new GCExample())
                    .WithExample(new StringReaderWriterExample())
                ;

            examples.RunLast();

            Console.ReadLine();
        }
    }
}