using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using NetCore3_1.Models.Helpers;

namespace NetCore3_1.Models.Entities
{
    public class Author
    {
        public int Id { get; set; }
        [Required]
        [FirstLetterUppercase]
        public string Name { get; set; }

        public IList<Book> Books { get; set; }
    }
}