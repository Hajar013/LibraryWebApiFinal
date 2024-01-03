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
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Unicode;
using BLL.Services.TransactionServices;
using BLL.Services.BookServices;
using BLL.Services.BookAuthorServices;
using BLL.Services.AuthServices;

namespace BLL.Services.LibrarianServices
{
    public  class LibrarianService : ILibrarianService  
    {
        private readonly IRepositoryFactory _repository;
        private readonly IMapper _mapper;
        private readonly ITransactionService _transactionService;
        private readonly IBookService _bookService;
        private readonly IBookAuthorService _bookAuthorService;
        public LibrarianService(IRepositoryFactory repository, IMapper mapper, ITransactionService transactionService, IBookService bookService, IBookAuthorService bookAuthorService) 
        {
            _repository = repository;
            _mapper = mapper;
            _bookService=bookService;
            _transactionService = transactionService;
            _bookAuthorService= bookAuthorService;
        }

        public void Create(LibrarianDto dto)
        {
            Librarian librarian = _mapper.Map<Librarian>(dto);
            _repository.Librarian.Create(librarian);
            _repository.Save();

        }

        public void Delete(LibrarianDto dto)
        {
            Librarian librarian = _mapper.Map<Librarian>(dto);
            _repository.Librarian.Delete(librarian);
            _repository.Save();
        }

        public List<LibrarianDto> FindAll()
        {

            List<Librarian> librariansFromDB = _repository.Librarian.FindAll().ToList();
            List<LibrarianDto> librariansDto = librariansFromDB.Select(librarian => _mapper.Map<LibrarianDto>(librarian)).ToList();

            return librariansDto;
        }

        public LibrarianDto FindByCondition(int id)
        {

            Librarian librariansFromDB = _repository.Librarian.FindByCondition(x=>x.Id == id).FirstOrDefault();
            LibrarianDto librariansDto = _mapper.Map<LibrarianDto>(librariansFromDB);

            return librariansDto;
        }

        public void Update(LibrarianDto dto)
        {
            Librarian librarian = _mapper.Map<Librarian>(dto);
            _repository.Librarian.Update(librarian);
            _repository.Save();
        }

        public bool AllowBorrow(int librarianId,int transactionId)
        {
            var transaction = _transactionService.FindByCondition(transactionId);
            BookDto book = _bookService.FindByCondition(transaction.BookId);
           if (transaction == null)
            {
                Console.WriteLine(" NULL");
                return false;
            }

            if (book == null )
            {
                Console.WriteLine(" B NULL");
                return false;
                
            }
            if(!book.Availability || book.Copies == 0)
            {
                Console.WriteLine(" B NAV COPIES 0");

                _transactionService.Delete(transaction);

                return false;
            }

            if (transaction != null && transaction.BrrowStatus == "Pending")
            {
                // Logic to approve the borrow request

                book.Copies--;
                if (book.Copies == 0)
                {
                    book.Availability = false;
                }
                _bookService.Update(book);

                transaction.LibrarianId = librarianId;
                transaction.BrrowStatus = "Success";
                _transactionService.Update(transaction);

                return true;
            }

            return false;

        }

        public bool AllowReturn(int librarianId, int transactionId)
        {
            var transaction = _transactionService.FindByCondition(transactionId);
            BookDto book = _bookService.FindByCondition(transaction.BookId);
            if (transaction == null)
            {
                Console.WriteLine(" NULL");
                return false;
            }

            if (book == null)
            {
                Console.WriteLine(" B NULL");
                return false;

            }

            if (transaction != null && transaction.ReturnStats == "Pending")
            {
                // Logic to approve the borrow request

                book.Copies++;
                book.Availability = true;
                _bookService.Update(book);

                transaction.LibrarianId = librarianId;
                transaction.ReturnStats = "Success";
                _transactionService.Update(transaction);

                return true;
            }

            return false;

        }
        public void AddBookAndAuther(BookAuthorDto bookAuthorDto)
        {
                _bookAuthorService.Create(bookAuthorDto);
        }

    }
}
