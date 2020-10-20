using System.ComponentModel.DataAnnotations;

namespace NetCore3_1.Models.DTOs
{
    public class BookDTO
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string AuthorId { get; set; }

        public AuthorDTO Author { get; set; }
    }
}