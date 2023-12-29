using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{ 
    public class Borrower  
    {
        [Key]
        public int Id { get; set; }
        public int PersonId { get; set; }

        [Required]
        public DateTime DateOfMembership { get; set; }
        public List<Bill>? Bills { get; set; }
        public Person Person { get; set; }
    }
}
