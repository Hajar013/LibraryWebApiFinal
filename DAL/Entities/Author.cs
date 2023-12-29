using System.ComponentModel.DataAnnotations;

namespace LibraryWebApiFinal.Models
{
    public class Author
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        [MaxLength(200)]
        public string Biography { get; set; }
        public List<BookAuthor> BookAuthors { get; set; }


    }
}
