using System.ComponentModel.DataAnnotations;
using System;

namespace BLL.DTOs
{
    public class AccounterDto
    {
        [Key]
        public int Id { get; set; }
        public int PersonId { get; set; }
        public string Certification { get; set; }
        public List<BillDto>? Bills { get; set; }
        public PersonDto Person { get; set; }


    }
}
