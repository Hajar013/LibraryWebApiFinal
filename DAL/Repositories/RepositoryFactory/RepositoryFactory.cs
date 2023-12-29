using DAL.Repositories.PersonRepos;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.RepositoryFactory
{
    public class RepositoryFactory : IRepositoryFactory
    {
        private readonly AppDBContext _appDBContext;
        private IPersonRepository _person;
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
