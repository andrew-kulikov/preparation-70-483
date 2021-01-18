using System;
using Common;
using DataAccess.Examples;

namespace DataAccess
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var examples = new ExampleSet()
                    .WithExample(new RequestsExample())
                ;

            examples.RunLast();

            Console.ReadLine();
        }
    }
}
