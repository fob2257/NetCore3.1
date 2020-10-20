using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NetCore3_1.Models.DTOs
{
    public class AuthorDTO : IValidatableObject
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public IList<BookDTO> Books { get; set; }

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