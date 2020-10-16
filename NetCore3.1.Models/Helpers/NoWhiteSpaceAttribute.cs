using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace NetCore3_1.Models.Helpers
{
    public class NoWhiteSpaceAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value.ToString().Any(Char.IsWhiteSpace)) return new ValidationResult("Should not contain whitespace");

            return ValidationResult.Success;
        }
    }
}