using System.ComponentModel.DataAnnotations;

namespace NetCore3_1.Models.Helpers
{
    public class FirstLetterUppercaseAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (string.IsNullOrEmpty(value.ToString())) return ValidationResult.Success;

            var firstLetter = value.ToString()[0].ToString();

            if (firstLetter != firstLetter.ToUpper()) return new ValidationResult("First letter should be in uppercase");

            return ValidationResult.Success;
        }
    }
}