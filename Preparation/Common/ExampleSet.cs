using System;
using System.Collections.Generic;
using System.Linq;

namespace Common
{
    public class ExampleSet
    {
        private readonly ICollection<ExampleRunConfiguration> _exampleConfigs = new List<ExampleRunConfiguration>();

        public ExampleSet WithExample(Example example, bool run = false)
        {
            var config = new ExampleRunConfiguration
            {
                Example = example,
                Run = run
            };

            _exampleConfigs.Add(config);

            return this;
        }

        public void RunLast()
        {
            var lastExample = _exampleConfigs.Last().Example;
            Run(new List<Example> {lastExample});
        }

        private static void Run(ICollection<Example> examples)
        {
            Console.WriteLine(new string('=', 40));

            foreach (var example in examples)
            {
                Console.WriteLine($"\n{example}\n");

                example.Execute();
            }

            Console.WriteLine();
            Console.WriteLine(new string('=', 40));
            Console.WriteLine();
        }

        public void Run()
        {
            var examples = _exampleConfigs.Where(config => config.Run)
                .Select(config => config.Example)
                .ToList();

            Run(examples);
        }
    }

    internal class ExampleRunConfiguration
    {
        public bool Run { get; set; }
        public Example Example { get; set; }
    }
}