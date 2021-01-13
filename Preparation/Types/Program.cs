﻿using System;
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
                ;

            examples.RunLast();

            Console.ReadLine();
        }
    }
}