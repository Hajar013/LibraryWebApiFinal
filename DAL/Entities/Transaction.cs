using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class Transaction
    {
        [Key]
        public int Id { get; set; }

        public DateTime? Date { get; set; }
        public string? BrrowStatus { get; set; }
        public string? ReturnStats { get; set; } 
        
        public int? LibrarianId { get; set; }
        [Required]
        public int BorrowerId { get; set; }
        [Required]
        public int BookId { get; set; }
        [ForeignKey("LibrarianId")]
        public Librarian? Librarian { get; set; }

        [ForeignKey("BorrowerId")]
        public Borrower Borrower { get; set; }

        [ForeignKey("BookId")]
        public Book Book { get; set; }
       


    }
}
