using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryWebApiFinal.Models
{
    public class Librarian
    {
        [Key]
        public int Id { get; set; }
        public int PersonId { get; set; }
        [Required]
        [MaxLength(20)]
        public string LiberianLicenseNumber { get; set; }       
        public List<Transaction>? transactions { get; set; }
        public Person Person { get; set; }

    }
}
