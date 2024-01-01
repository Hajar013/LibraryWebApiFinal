using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class Librarian
    {
        [Key]
        public int Id { get; set; }
        public int PersonId { get; set; }
        [Required]
        [MaxLength(20)]
        public string LibrarianlicenseNumber { get; set; }       
        public List<Transaction>? transactions { get; set; }
        public Person Person { get; set; }

    }
}
