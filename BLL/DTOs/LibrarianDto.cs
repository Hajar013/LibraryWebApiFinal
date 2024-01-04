using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BLL.DTOs
{
    public class LibrarianDto
    {
        public int Id { get; set; }
        public int? PersonId { get; set; }
        public string? LibrarianlicenseNumber { get; set; }      
        
        public List<TransactionDto>? Transactions { get; set; }
        public PersonDto? Person { get; set; }

    }
}
