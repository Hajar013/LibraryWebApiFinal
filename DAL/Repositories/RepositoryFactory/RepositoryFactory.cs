using DAL.Repositories.PersonRepos;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Repositories.LibrarianRepos;
using DAL.Repositories.BorrowerRepos;
using DAL.Repositories.AccounterRepos;
using DAL.Repositories.BookRepos;
using DAL.Repositories.AuthorRepos;
using DAL.Repositories.TransactionRepos;
using DAL.Repositories.BillRepos;
using DAL.Repositories.BookAuthorRepos;

namespace DAL.Repositories.RepositoryFactory
{
    public class RepositoryFactory : IRepositoryFactory
    {
        private readonly AppDBContext _appDBContext;
        private IPersonRepository _person;
        private ILibrarianRepository _librarian;
        private IBorrowerRepository _borrower;
        private IAccounterRepository _accounter;
        private IBookRepository _book;
        private IAuthorRepository _author;
        private ITransactionRepository _transaction;
        private IBillRepository _bill;
        private IBookAuthorRepository _bookAuthor;

        public IPersonRepository Person
        {
            get
            {
                if (_person == null)
                {
                    _person = new PersonRepository(_appDBContext);
                }
                return _person;
            }
        }
        public ILibrarianRepository Librarian
        {
            get
            {
                if (_librarian == null)
                {
                    _librarian = new LibrarianRepository(_appDBContext);
                }
                return _librarian;
            }
        }
        public IBorrowerRepository Borrower
        {
            get
            {
                if (_borrower == null)
                {
                    _borrower = new BorrowerRepository(_appDBContext);
                }
                return _borrower;
            }
        }
        public IAccounterRepository Accounter
        {
            get
            {
                if (_accounter == null)
                {
                    _accounter = new AccounterRepository(_appDBContext);
                }
                return _accounter;
            }
        }
        public IBookRepository Book
        {
            get
            {
                if (_book == null)
                {
                    _book = new BookRepository(_appDBContext);
                }
                return _book;
            }
        }
        public IAuthorRepository Author
        {
            get 
            { 
                if(_author == null)
                {
                    _author = new AuthorRepository(_appDBContext);
                }
                return _author;
            }
        }
        public ITransactionRepository Transaction
        {
            get 
            { 
                if(_transaction == null)
                    _transaction = new TransactionRepository(_appDBContext);
                return _transaction;
            }
        }
        public IBillRepository Bill
        {
            get 
            { 
                if (_bill == null)
                    _bill = new BillRepository(_appDBContext);
                return _bill;
            }
        }
        public IBookAuthorRepository BookAuthor
        {
            get 
            {
                if (_bookAuthor == null)
                    _bookAuthor = new BookAuthorRepository(_appDBContext);
                return _bookAuthor; 
            }

        }
        
        public RepositoryFactory(AppDBContext appDBContext)
        {
            _appDBContext = appDBContext;
        }
        public void Save()
        {
            _appDBContext.SaveChanges();
        }
    }
}
