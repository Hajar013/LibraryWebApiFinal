using System.ComponentModel.DataAnnotations;
using System;

namespace LibraryWebApiFinal.Models
{
    public class Accounter
    {
        [Key]
        public int Id { get; set; }
        public int PersonId { get; set; }
        [Required]
        public string Certification { get; set; }
        public List<Bill>? Bills { get; set; }
        public Person Person { get; set; }


    }
}
