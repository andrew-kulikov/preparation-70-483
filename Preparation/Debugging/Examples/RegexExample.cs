using System;
using System.Text.RegularExpressions;
using Common;

namespace Debugging.Examples
{
    public class RegexExample : Example
    {
        public RegexExample() : base("Regex example", "3.1")
        {
        }

        public override void Execute()
        {
            var regex = new Regex("[a-z]+(0)", RegexOptions.Compiled);

            Console.WriteLine(regex.IsMatch("50"));
            Console.WriteLine(regex.IsMatch("a0"));
            Console.WriteLine(regex.IsMatch("50a0"));
            Console.WriteLine(regex.IsMatch("50a0z0"));

            foreach (Match match in regex.Matches("50a0z0"))   
            {
                if (match.Success)
                {
                    foreach (Group group in match.Groups)      
                    {
                        Console.WriteLine(group.Name);
                        Console.WriteLine(group);
                    }
                    Console.WriteLine(match);
                }
            }
        }
    }
}