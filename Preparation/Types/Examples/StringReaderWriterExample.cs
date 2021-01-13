using System;
using System.IO;
using System.Linq.Expressions;
using Common;

namespace Types.Examples
{
    public class StringReaderWriterExample : Example
    {
        public StringReaderWriterExample() : base("StringReaderWriterExample example", "2.4")
        {
        }

        public override void Execute()
        {
            var writer = new StringWriter();

            writer.NewLine = "\n\n";

            writer.WriteLine("asd");
            writer.WriteLine("123");
            writer.WriteLineAsync("sdf");

            writer.Close();

            Console.WriteLine(writer.ToString());
            Console.WriteLine("Reading...");

            var reader = new StringReader(writer.ToString());

            while (true)
            {
                var line = reader.ReadLine();
                if (line == null) break;

                Console.WriteLine(line);
            }
        }
    }
}