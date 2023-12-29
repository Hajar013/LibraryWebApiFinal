using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryWebApiFinal.Models
{ 
    public class BorrowerDto  
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public DateTime DateOfMembership { get; set; }

        public List<BillDto>? Bills { get; set; }
        public PersonDto Person { get; set; }
    }
}
