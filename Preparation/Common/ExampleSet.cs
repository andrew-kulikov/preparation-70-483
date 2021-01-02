using System;
using System.Collections.Generic;

namespace Common
{
    public class ExampleSet
    {
        private readonly ICollection<Example> _examples = new List<Example>();

        public ExampleSet WithExample(Example example)
        {
            _examples.Add(example);

            return this;
        }

        public void Run()
        {
            Console.WriteLine(new string('=', 40));

            foreach (var example in _examples)
            {
                Console.WriteLine($"\n{example}\n");

                example.Execute();
            }

            Console.WriteLine();
            Console.WriteLine(new string('=', 40));
            Console.WriteLine();
        }
    }
}