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
                    .WithExample(new RegexExample())
                    .WithExample(new RegexCompilationExample())
                    .WithExample(new ValidationExample())
                    .WithExample(new TraceExample())
                    .WithExample(new TraceSourceExample())
                ;

            examples.RunLast();

            Console.ReadLine();
        }
    }
}