﻿using System;
using Common;
using Debugging.Examples;

namespace Debugging
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var examples = new ExampleSet()
                    .WithExample(new JsonExample())
                ;

            examples.RunLast();

            Console.ReadLine();
        }
    }
}