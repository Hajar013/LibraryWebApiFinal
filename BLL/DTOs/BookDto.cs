using System.ComponentModel.DataAnnotations;
using System.Transactions;

namespace BLL.DTOs
{
    public class BookDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Subject { get; set; }
        public string Publisher { get; set; }
        public string? Edition { get; set; }
        public int Copies { get; set; }
        public bool Availability { get; set; }

        // Navigation properties
        public List<BillDto> Bills { get; set; }
        public List<TransactionDto> Transactions { get; set; }
        public List<BookAuthorDto>? BookAuthors { get; set; }

    }

}
