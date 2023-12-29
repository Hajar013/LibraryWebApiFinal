using AutoMapper;
using BLL.DTOs;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
              CreateMap<Person, PersonDto>().ReverseMap();
              CreateMap<Librarian, LibrarianDto>().ReverseMap();
              CreateMap<Borrower, BorrowerDto>().ReverseMap();
              CreateMap<Accounter, AccounterDto>().ReverseMap();
        }

    }
}
