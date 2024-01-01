using DAL.Repositories.AccounterRepos;
using DAL.Repositories.AuthorRepos;
using DAL.Repositories.BillRepos;
using DAL.Repositories.BookAuthorRepos;
using DAL.Repositories.BookRepos;
using DAL.Repositories.BorrowerRepos;
using DAL.Repositories.LibrarianRepos;
using DAL.Repositories.PersonRepos;
using DAL.Repositories.TransactionRepos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.RepositoryFactory
{
    public interface IRepositoryFactory
    {
        IPersonRepository Person { get; }
        ILibrarianRepository Librarian { get; }
        IBorrowerRepository Borrower { get; }
        IAccounterRepository Accounter { get; }
        IBookRepository Book { get; }
        IAuthorRepository Author { get; }
        ITransactionRepository Transaction { get; }
        IBillRepository Bill { get; }
        IBookAuthorRepository BookAuthor { get; }
        void Save();
    }
}
