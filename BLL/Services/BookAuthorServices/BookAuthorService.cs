﻿using DAL.Repositories.PersonRepos;
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
            _repository.Save();
        }

         void Delete(BookAuthorDto dto)
        {
            BookAuthor bookAuthor = _mapper.Map<BookAuthor>(dto);
            _repository.BookAuthor.Delete(bookAuthor);
            _repository.Save();
        }

         IList<BookAuthorDto> FindAll()
        {

            List<BookAuthor> bookAuthorsFromDB = _repository.BookAuthor.FindAll().ToList().ToList();
            List<BookAuthorDto> bookAuthorsDto = bookAuthorsFromDB.Select(bookAuthor => _mapper.Map<BookAuthorDto>(bookAuthor)).ToList();

            return bookAuthorsDto;
        }

        BookAuthorDto FindByCondition(int id)
        {
            BookAuthor bookAuthorsFromDB = _repository.BookAuthor.FindByCondition(x => x.BookAuthorId == id).FirstOrDefault();
            BookAuthorDto bookAuthorsDto = _mapper.Map<BookAuthorDto>(bookAuthorsFromDB);
            return bookAuthorsDto;
        }

        void Update(BookAuthorDto dto)
        {
            BookAuthor bookAuthor = _mapper.Map<BookAuthor>(dto);
            _repository.BookAuthor.Update(bookAuthor);
            _repository.Save();
        }
    }
}
