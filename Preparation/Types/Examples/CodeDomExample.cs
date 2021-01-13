using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.IO;
using System.Reflection;
using Common;

namespace Types.Examples
{
    public class CodeDomExample : Example
    {
        public CodeDomExample() : base("Code Dom", "2.4")
        {
        }

        public override void Execute()
        {
            var compileUnit = new CodeCompileUnit();

            // Create a namespace to hold the types we are going to create
            var personnelNameSpace = new CodeNamespace("Personnel");

            // Import the system namespace
            personnelNameSpace.Imports.Add(new CodeNamespaceImport("System"));
            // Create a Person class
            var personClass = new CodeTypeDeclaration("Person")
            {
                IsClass = true,
                TypeAttributes = TypeAttributes.Public
            };

            // Add the Person class to personnelNamespace
            personnelNameSpace.Types.Add(personClass);

            // Create a field to hold the name of a person
            var nameField = new CodeMemberField("String", "name") {Attributes = MemberAttributes.Public};

            // Add the name field to the Person class
            personClass.Members.Add(nameField);

            // Add the namespace to the document
            compileUnit.Namespaces.Add(personnelNameSpace);

            // Now we need to send our document somewhere
            // Create a provider to parse the document
            var provider = CodeDomProvider.CreateProvider("CSharp");
            // Give the provider somewhere to send the parsed output
            var s = new StringWriter();
            // Set some options for the parse - we can use the defaults
            var options = new CodeGeneratorOptions();

            // Generate the C# source from the CodeDOM
            provider.GenerateCodeFromCompileUnit(compileUnit, s, options);
            // Close the output stream
            s.Close();

            // Print the C# output
            Console.WriteLine(s.ToString());
        }
    }
}