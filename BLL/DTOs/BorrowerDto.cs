using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BLL.DTOs
{ 
    public class BorrowerDto  
    {
        public int Id { get; set; }
        public int? PersonId { get; set; }
        public DateTime DateOfMembership { get; set; }
        public List<TransactionDto>? transactions { get; set; }
        public List<BillDto>? Bills { get; set; }
        public PersonDto? Person { get; set; }
    }
}
