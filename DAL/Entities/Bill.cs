using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    public class Bill
    {
        [Key]
        public int BillNo { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public int BorrowerId { get; set; }
        [Required]
        public double Amount { get; set; }
        [Required]
        public int BookId { get; set; }
        [Required]
        public int AccounterId { get; set; }

        [ForeignKey("BorrowerId")]
        public Borrower Borrower { get; set; }
        [ForeignKey("BookId")]
        public Book Book { get; set; }
        [ForeignKey("AccounterId")]
        public Accounter Accounter { get; set; }

    }

}
