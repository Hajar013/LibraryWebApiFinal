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
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using BLL.Services.BookServices;
using BLL.Services.PersonServices;
using BLL.Services.TransactionServices;
using BLL.Services.BillServices;

namespace BLL.Services.BorrowerServices
{
    public  class BorrowerService : IBorrowerService, IAuthService<BorrowerDto>
    {
        private readonly IRepositoryFactory _repository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IBookService _bookService;
        private readonly IPersonService _personService;
        private readonly ITransactionService _transactionService;
        private readonly IBillService _billService;


        public BorrowerService(IRepositoryFactory repository, IMapper mapper, IConfiguration configuration, IBookService bookService,
            IPersonService personService, ITransactionService transactionService, IBillService billService)
        {
            _repository = repository;
            _mapper = mapper;
            _configuration = configuration;
            _bookService = bookService;
            _personService = personService;
            _transactionService = transactionService;
            _billService = billService; 
        }


        public void Create(BorrowerDto dto)
        {
            Borrower borrower = _mapper.Map<Borrower>(dto);
            _repository.Borrower.Create(borrower);
            _repository.Save();

        }

        public void Delete(BorrowerDto dto)
        {
            Borrower borrower = _mapper.Map<Borrower>(dto);
            _repository.Borrower.Delete(borrower);
            _repository.Save();
        }

        public IQueryable<BorrowerDto> FindAll()
        {

            IQueryable<Borrower> borrowersFromDB = _repository.Borrower.FindAll();
            IQueryable<BorrowerDto> borrowersDto = borrowersFromDB.Select(borrower => _mapper.Map<BorrowerDto>(borrower));

            return borrowersDto;
        }

        public BorrowerDto FindById(int id)
        {

            Borrower borrowersFromDB = _repository.Borrower.FindByCondition(x=>x.Id == id).FirstOrDefault();
            BorrowerDto borrowersDto =  _mapper.Map<BorrowerDto>(borrowersFromDB);

            return borrowersDto;
        }


        public void Update(BorrowerDto dto)
        {
            Borrower borrower = _mapper.Map<Borrower>(dto);
            _repository.Borrower.Update(borrower);
            _repository.Save();
        }

        public string GenerateJwtToken(BorrowerDto user)
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


        public BorrowerDto Authenticate(string username, string password)
        {
            var authenticatedBorrower = _repository.Borrower
                .FindByCondition(borrower =>
                    borrower.Person.UserName == username && borrower.Person.Password == password)
                .Include(borrower => borrower.Person)
                .FirstOrDefault();
            var borrower = _mapper.Map<BorrowerDto>(authenticatedBorrower);

            return borrower;

        }


        public void Register(BorrowerDto user)
        {
            var borrower = _mapper.Map<Borrower>(user);
            _repository.Person.Create(borrower.Person);
            _repository.Borrower.Create(borrower);
            _repository.Save();

        }



        public bool RequestToBorrowBook(int bookId, int borrowerId)
        {
            // Assuming there's a method to search for a book by ID in the BookRepository
            var book = _bookService.FindByCondition(bookId);

            if (book == null)
            {
                Console.WriteLine("Book not found or not available for borrowing.");
                return false;
            }

            // Assuming there's a method to get a borrower by their ID from a PersonRepository
            var borrower =FindById(borrowerId);

            if (borrower == null)
            {
                Console.WriteLine("Borrower not found.");
                return false;
            }

            // Create a transaction and add it to the pending transactions
            var transaction = new TransactionDto
            {
                BookId = book.Id,
                BorrowerId = borrowerId,
                Status = "Pending",
                Date = DateTime.Now
            };

            _transactionService.Create(transaction);

            return true; // Successfully borrowed the book
        }
        public bool RequestToBill(int bookId, int borrowerId)
        {
            // Assuming there's a method to search for a book by ID in the BookRepository
            var book = _bookService.FindByCondition(bookId);

            if (book == null)
            {
                Console.WriteLine("Book not found or not available for buyig");
                return false;
            }

            // Assuming there's a method to get a borrower by their ID from a PersonRepository
            var borrower = FindById(borrowerId);

            if (borrower == null)
            {
                Console.WriteLine("Borrower not found.");
                return false;
            }

            // Create a transaction and add it to the pending transactions
            var bill= new BillDto
            {
                BookId = book.Id,
                BorrowerId = borrowerId,
                Status = "Pending",
                Date = DateTime.Now,
                Amount = book.Amount 
            };

            _billService.Create(bill);


            return true; // Successfully borrowed the book
        }


    }
}
