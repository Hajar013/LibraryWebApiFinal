using System.ComponentModel.DataAnnotations;
using System.Transactions;

namespace DAL.Entities
{
    public class Book
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Title { get; set; }
        [Required]
        [MaxLength(100)]
        public string Subject { get; set; }
        [Required]
        [MaxLength(50)]
        public string Publisher { get; set; }
        [MaxLength(50)]
        public string? Edition { get; set; }
        [Required]
        public int Copies { get; set; }
        [Required]
        public bool Availability { get; set; }
        // Navigation properties
        public List<Bill> Bills { get; set; }
        public List<Transaction> Transactions { get; set; }
        public List<BookAuthor>? BookAuthors { get; set; }

    }

}
