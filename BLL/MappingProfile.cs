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
           
            CreateMap<Accounter, AccounterDto>().ReverseMap();
            CreateMap<Author, AuthorDto>().ReverseMap();    
            CreateMap<Bill, BillDto>().ReverseMap();
            CreateMap<BookAuthor, BookAuthorDto>().ReverseMap();
            CreateMap<Book,BookDto>().ReverseMap();
            CreateMap<Borrower, BorrowerDto>().ReverseMap();
            CreateMap<Librarian, LibrarianDto>().ReverseMap();
            CreateMap<Person, PersonDto>().ReverseMap();
            CreateMap<Transaction, TransactionDto>().ReverseMap();  
          
        }

    }
}
