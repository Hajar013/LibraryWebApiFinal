using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryWebApiFinal.Models
{
    public class TransactionDto
    {
        public int Id { get; set; }
        public DateTime? DateOfIssues { get; set; }
        public int LibrarianId { get; set; }
        public int BorrowerId { get; set; }
        public int BookId { get; set; }

        public LibrarianDto Librarian { get; set; }
        public BorrowerDto Borrower { get; set; }
        public BookDto Book { get; set; }

    }
}
