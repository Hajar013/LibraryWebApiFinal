using AutoMapper;
using BLL.DTOs;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Profiles
{
    public class MappingProfile :  Profile
    {
        public MappingProfile()
        {
            //CreateMap<PersonDto, Person>().ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name)).ForMember(dest => dest.Id, opt => opt.Ignore());
            //CreateMap<Product, ProductDto>().ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.ProductName));
            CreateMap<Person, PersonDto>().ReverseMap();
            CreateMap<Librarian, LibrarianDto>().ReverseMap();
            CreateMap<Borrower, BorrowerDto>().ReverseMap();
            CreateMap<Accounter, AccounterDto>().ReverseMap();

        }

    }
}
