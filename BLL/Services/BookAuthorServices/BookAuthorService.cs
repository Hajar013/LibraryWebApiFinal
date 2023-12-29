using DAL.Repositories.PersonRepos;
using DAL.Repositories;
using BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Repositories.RepositoryFactory;
using System.Linq.Expressions;
using AutoMapper;
using DAL.Entities;

namespace BLL.Services.BookAuthorServices
{
    public  class BookAuthorService : IBookAuthorService
    {
        private readonly IRepositoryFactory _repository;
        private readonly IMapper _mapper;

        public BookAuthorService(IRepositoryFactory repository, IMapper mapper) 
        {
            _repository = repository;
            _mapper = mapper;
        }

        public void Create(BookAuthorDto dto)
        {
            BookAuthor bookAuthor = _mapper.Map<BookAuthor>(dto);
            _repository.BookAuthor.Create(bookAuthor);

        }

        public void Delete(BookAuthorDto dto)
        {
            BookAuthor bookAuthor = _mapper.Map<BookAuthor>(dto);
            _repository.BookAuthor.Delete(bookAuthor);
        }

        public IQueryable<BookAuthorDto> FindAll()
        {

            IQueryable<BookAuthor> bookAuthorsFromDB = _repository.BookAuthor.FindAll();
            IQueryable<BookAuthorDto> bookAuthorsDto = bookAuthorsFromDB.Select(bookAuthor => _mapper.Map<BookAuthorDto>(bookAuthor));

            return bookAuthorsDto;
        }

/*        public IQueryable<BookAuthorDto> FindByCondition(int id)
        {

            IQueryable<BookAuthor> bookAuthorsFromDB = _repository.BookAuthor.FindByCondition(x=>x.Id == id);
            IQueryable<BookAuthorDto> bookAuthorsDto = bookAuthorsFromDB.Select(bookAuthor => _mapper.Map<BookAuthorDto>(bookAuthor));

            return bookAuthorsDto;
        }*/

        public void Update(BookAuthorDto dto)
        {
            BookAuthor bookAuthor = _mapper.Map<BookAuthor>(dto);
            _repository.BookAuthor.Update(bookAuthor);
        }
    }
}
