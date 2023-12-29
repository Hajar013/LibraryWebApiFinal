using DAL.Repositories.PersonRepos;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Repositories.LibrarianRepos;
using DAL.Repositories.BorrowerRepos;

namespace DAL.Repositories.RepositoryFactory
{
    public class RepositoryFactory : IRepositoryFactory
    {
        private readonly AppDBContext _appDBContext;
        private IPersonRepository _person;
        private ILibrarianRepository _librarian;
        private IBorrowerRepository _borrower;
        //private IAccountRepository _account;
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
        //public IAccountRepository Account
        //{
        //    get
        //    {
        //        if (_account == null)
        //        {
        //            _account = new AccountRepository(_repoContext);
        //        }
        //        return _account;
        //    }
        //}
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
