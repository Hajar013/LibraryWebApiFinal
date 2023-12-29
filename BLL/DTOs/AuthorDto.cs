using System.ComponentModel.DataAnnotations;

namespace LibraryWebApiFinal.Models
{
    public class AuthorDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Biography { get; set; }
        public List<BookAuthorDto> BookAuthors { get; set; }


    }
}
