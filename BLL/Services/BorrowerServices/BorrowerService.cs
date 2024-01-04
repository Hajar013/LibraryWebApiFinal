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
using System.Security.Claims;
using BLL.Services.BookServices;
using BLL.Services.PersonServices;
using BLL.Services.TransactionServices;
using BLL.Services.BillServices;
using BLL.Services.AuthServices;

namespace BLL.Services.BorrowerServices
{
    public  class BorrowerService : IBorrowerService
    {
        private readonly IRepositoryFactory _repository;
        private readonly IMapper _mapper;
        private readonly IBookService _bookService;
        private readonly ITransactionService _transactionService;
        private readonly IBillService _billService;
        private readonly IPersonService _personService;


        public BorrowerService(IRepositoryFactory repository, IMapper mapper, IBookService bookService,
             ITransactionService transactionService, IBillService billService, IPersonService personService)
        {
            _repository = repository;
            _mapper = mapper;
            _bookService = bookService;
            _transactionService = transactionService;
            _billService = billService; 
            _personService = personService;
        }


         void Create(BorrowerDto dto)
        {
            Borrower borrower = _mapper.Map<Borrower>(dto);
            _repository.Borrower.Create(borrower);
            _repository.Save();

        }

         void Delete(BorrowerDto dto)
        {
            Borrower borrower = _mapper.Map<Borrower>(dto);
            _repository.Borrower.Delete(borrower);
            _repository.Save();
        }

         IList<BorrowerDto> FindAll()
        {

            List<Borrower> borrowersFromDB = _repository.Borrower.FindAll().ToList();
            List<BorrowerDto> borrowersDto = borrowersFromDB.Select(borrower => _mapper.Map<BorrowerDto>(borrower)).ToList();

            return borrowersDto;
        }

        public BorrowerDto FindById(int id)
        {

            Borrower borrowersFromDB = _repository.Borrower.FindByCondition(x=>x.Id == id).FirstOrDefault();
            BorrowerDto borrowersDto =  _mapper.Map<BorrowerDto>(borrowersFromDB);

            return borrowersDto;
        }


         void Update(BorrowerDto dto)
        {
            Borrower borrower = _mapper.Map<Borrower>(dto);
            _repository.Borrower.Update(borrower);
            _repository.Save();
        }

        //public bool RequestToBorrowBook(int bookId, int borrowerId)
        //{
        //    // Assuming there's a method to search for a book by ID in the BookRepository
        //    var book = _bookService.FindByCondition(bookId);

        //    if (book == null)
        //    {
        //        Console.WriteLine("Book not found or not available for borrowing.");
        //        return false;
        //    }

        //    // Assuming there's a method to get a borrower by their ID from a PersonRepository
        //    //var borrower =FindById(borrowerId);
        //    var person = _personService.FindByCondition(borrowerId);

        //    if (person == null && person.Borrower == null)
        //    {
        //        Console.WriteLine("Borrower not found.");
        //        return false;
        //    }

        //    // Create a transaction and add it to the pending transactions
        //    var transaction = new TransactionDto
        //    {
        //        BookId = book.Id,
        //        BorrowerId = person.Borrower.Id,
        //        BrrowStatus = "Pending",
        //        Date = DateTime.Now
        //    };

        //    _transactionService.Create(transaction);

        //    return true; // Successfully borrowed the book
        //}

        //public bool RequestToBorrowBook(int bookId, int borrowerId)
        //{
        //    var book = _bookService.FindByCondition(bookId);

        //    if (book == null)
        //    {
        //        Console.WriteLine("Book not found or not available for borrowing.");
        //        return false;
        //    }

        //    var person = _personService.FindByCondition(borrowerId);

        //    if (person == null )
        //    {
        //        Console.WriteLine("person not found.");
        //        return false;
        //    }

        //    if (person.Borrower == null)
        //    {
        //        Console.WriteLine("Borrower not found.");
        //        return false;
        //    }
        //    var transaction = new TransactionDto
        //    {
        //        BookId = book.Id,
        //        BorrowerId = person.Borrower.Id,
        //        BrrowStatus = "Pending",
        //        Date = DateTime.Now
        //    };

        //    _transactionService.Create(transaction);

        //    return true;
        //}
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
            var borrower = FindById(borrowerId);

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
                BrrowStatus = "Pending",
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

        public bool RequestToReturnBook(int transactionId, int borrowerId)
        {
            // Assuming there's a method to search for a book by ID in the BookRepository
            TransactionDto transaction = _transactionService.FindByCondition(transactionId);

            if (transaction == null)
            {
                Console.WriteLine("Book not found or not available for borrowing.");
                return false;
            }

            // Assuming there's a method to get a borrower by their ID from a PersonRepository
            var borrower = FindById(borrowerId);

            if (borrower == null)
            {
                Console.WriteLine("Borrower not found.");
                return false;
            }
            if (transaction.BrrowStatus != "Success")
                return false;

            transaction.ReturnStats = "Pending";
            _transactionService.Update(transaction);

            return true; // Successfully borrowed the book
        }


    }
}
