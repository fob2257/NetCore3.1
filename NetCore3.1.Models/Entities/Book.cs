using System.ComponentModel.DataAnnotations;

namespace NetCore3_1.Models.Entities
{
    public class Book
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string AuthorId { get; set; }

        public Author Author { get; set; }
    }
}