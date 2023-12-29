using DAL.Repositories.BorrowerRepos;
using DAL.Repositories.LibrarianRepos;
using DAL.Repositories.PersonRepos;
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
        void Save();
    }
}
