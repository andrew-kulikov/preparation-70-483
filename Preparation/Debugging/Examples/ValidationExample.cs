using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Common;

namespace Debugging.Examples
{
    public class ValidationExample : Example
    {
        public ValidationExample() : base("Validation example", "3.1")
        {
        }

        public override void Execute()
        {
            var p = new ValidatablePerson {Age = -1};

            var results = p.Validate(new ValidationContext(p));

            foreach (var validationResult in results) Console.WriteLine(validationResult);
        }
    }

    public class ValidatablePerson : IValidatableObject
    {
        public int Age { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Age < 0) yield return new ValidationResult("Age less than zero");
        }
    }
}