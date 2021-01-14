using System.Reflection;
using System.Text.RegularExpressions;
using Common;

namespace Debugging.Examples
{
    /// <summary>
    ///     Regex compilation in assembly does not work in .net core
    /// </summary>
    public class RegexCompilationExample : Example
    {
        public RegexCompilationExample() : base("Regex compilation example", "3.1")
        {
        }

        public override void Execute()
        {
            //Compile();
        }

        public void Compile()
        {
            var SentencePattern = new RegexCompilationInfo(@"[a-z]+(0)",
                RegexOptions.Singleline,
                "SentencePattern",
                "Utilities.RegularExpressions",
                true);

            var regexes = new[] {SentencePattern};
            var assemblyName = new AssemblyName("RegexLib, Version=1.0.0.1001, Culture=neutral, PublicKeyToken=null");
            Regex.CompileToAssembly(regexes, assemblyName);
        }
    }
}