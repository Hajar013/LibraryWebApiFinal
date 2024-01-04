using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using DAL.Entities;

namespace BLL.DTOs
{
    public class BillDto
    {
      


        public int BillNo { get; set; }
        public DateTime? Date { get; set; }
        public string Status { get; set; }

        
        public int BorrowerId { get; set; }
        public double Amount { get; set; }
        public int BookId { get; set; }

        public int? AccounterId { get; set; }

        public BorrowerDto Borrower { get; set; }
        public BookDto Book { get; set; }
        public AccounterDto? Accounter { get; set; }

    }

}
