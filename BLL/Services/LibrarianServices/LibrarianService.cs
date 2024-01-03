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
        private readonly IConfiguration _configuration;
        private readonly ITransactionService _transactionService;
        private readonly IBookService _bookService;
        private readonly IBookAuthorService _bookAuthorService;
        public LibrarianService(IRepositoryFactory repository, IMapper mapper, IConfiguration configuration, ITransactionService transactionService, IBookService bookService, IBookAuthorService bookAuthorService) 
        {
            _repository = repository;
            _mapper = mapper;
            _configuration = configuration;
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

        public IQueryable<LibrarianDto> FindAll()
        {

            IQueryable<Librarian> librariansFromDB = _repository.Librarian.FindAll();
            IQueryable<LibrarianDto> librariansDto = librariansFromDB.Select(librarian => _mapper.Map<LibrarianDto>(librarian));

            return librariansDto;
        }

        public IQueryable<LibrarianDto> FindByCondition(int id)
        {

            IQueryable<Librarian> librariansFromDB = _repository.Librarian.FindByCondition(x=>x.Id == id);
            IQueryable<LibrarianDto> librariansDto = librariansFromDB.Select(librarian => _mapper.Map<LibrarianDto>(librarian));

            return librariansDto;
        }

        public void Update(LibrarianDto dto)
        {
            Librarian librarian = _mapper.Map<Librarian>(dto);
            _repository.Librarian.Update(librarian);
            _repository.Save();
        }

        public string GenerateJwtToken(LibrarianDto user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Person.Role),
                },
                expires: DateTime.Now.AddMinutes(30), // Token expiry time (adjust as needed)
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        public LibrarianDto Authenticate(string username, string password)
        {
            var authenticatedLibrarian = _repository.Librarian
                .FindByCondition(librarian =>
                    librarian.Person.UserName == username && librarian.Person.Password == password)
                .Include(librarian => librarian.Person)
                .FirstOrDefault();
            var librarian = _mapper.Map<LibrarianDto>(authenticatedLibrarian);

            return librarian;

        }


        public void Register(LibrarianDto user)
        {
            var librarian = _mapper.Map<Librarian>(user);
            _repository.Person.Create(librarian.Person);
            _repository.Librarian.Create(librarian);
            _repository.Save();

        }

        public bool AllowBorrow(int librarianId,int transactionId)
        {
            var transaction = _transactionService.FindByCondition(transactionId).FirstOrDefault();
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
            var transaction = _transactionService.FindByCondition(transactionId).FirstOrDefault();
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
