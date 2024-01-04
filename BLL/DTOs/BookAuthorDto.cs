using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BLL.DTOs
{
    public class BookAuthorDto
    {
        public int BookAuthorId { get; set; }
        public int BookId { get; set; }
        public int AuthorId { get; set; }
        public string? AwaredName { get; set; }

        // Navigation properties
        public AuthorDto Author { get; set; }
        public BookDto Book { get; set; }


    }

}
