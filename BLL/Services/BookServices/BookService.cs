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

namespace BLL.Services.BookServices
{
    public  class BookService : IBookService
    {
        private readonly IRepositoryFactory _repository;
        private readonly IMapper _mapper;

        public BookService(IRepositoryFactory repository, IMapper mapper) 
        {
            _repository = repository;
            _mapper = mapper;
        }

        public void Create(BookDto dto)
        {
            Book book = _mapper.Map<Book>(dto);
            _repository.Book.Create(book);
            _repository.Save();
           
        }

        public void Delete(BookDto dto)
        {
            Book book = _mapper.Map<Book>(dto);
            _repository.Book.Delete(book);
            _repository.Save();
        }

        public IQueryable<BookDto> FindAll()
        {

            IQueryable<Book> booksFromDB = _repository.Book.FindAll();
            IQueryable<BookDto> booksDto = booksFromDB.Select(book => _mapper.Map<BookDto>(book));

            return booksDto;
        }

        public BookDto FindByCondition(int id)
        {

            Book booksFromDB = _repository.Book.FindByCondition(x=>x.Id == id).FirstOrDefault();
            BookDto booksDto =  _mapper.Map<BookDto>(booksFromDB);

            return booksDto;
        }

        public IQueryable<BookDto> FindByTitle(string title)
        {

            IQueryable<Book> booksFromDB = _repository.Book.FindByCondition(x => x.Title == title);
            IQueryable<BookDto> booksDto = booksFromDB.Select(book => _mapper.Map<BookDto>(book));

            return booksDto;
        }
        public void Update(BookDto dto)
        {
            Book book = _mapper.Map<Book>(dto);
            _repository.Book.Update(book);
            _repository.Save();
        }
    }
}
