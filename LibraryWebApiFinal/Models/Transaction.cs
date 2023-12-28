using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryWebApiFinal.Models
{
    public class Transaction
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public DateTime? DateOfIssues { get; set; }
        [Required]
        public int LibrarianId { get; set; }
        [Required]
        public int BorrowerId { get; set; }
        [Required]
        public int BookId { get; set; }
        [ForeignKey("LibrarianId")]
        public Librarian Librarian { get; set; }

        [ForeignKey("BorrowerId")]
        public Borrower Borrower { get; set; }

        [ForeignKey("BookId")]
        public Book Book { get; set; }

    }
}
