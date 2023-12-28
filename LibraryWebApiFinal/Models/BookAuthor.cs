using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryWebApiFinal.Models
{
    public class BookAuthor
    {
        [Key]
        public int BookAuthorId { get; set; }
        [Required]
        public int BookId { get; set; }
        [Required]
        public int AuthorId { get; set; }
        public string? AwaredName { get; set; }

        // Navigation properties
        [ForeignKey("AuthorId")]
        public Author Author { get; set; }
        [ForeignKey("BookId")]
        public Book Book { get; set; }


    }

}
