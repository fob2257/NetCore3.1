using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using NetCore3_1.Models.Helpers;

namespace NetCore3_1.Models.Entities
{
    public class Author : IValidatableObject
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public IList<Book> Books { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!string.IsNullOrEmpty(Name))
            {
                var firstLetter = Name[0].ToString();

                if (firstLetter != firstLetter.ToUpper())
                {
                    yield return new ValidationResult("First letter should be in uppercase", new string[] { nameof(Name) });
                }

            }
        }
    }
}