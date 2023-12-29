using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryWebApiFinal.Models
{
    public class LibrarianDto
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public string LiberianLicenseNumber { get; set; }      
        
        public List<TransactionDto>? transactions { get; set; }
        public PersonDto Person { get; set; }

    }
}
