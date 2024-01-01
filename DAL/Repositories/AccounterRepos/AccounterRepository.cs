using DAL.Entities;
using DAL.Repositories.BorrowerRepos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.AccounterRepos
{
    public class AccounterRepository : RepositoryBase<Accounter>, IAccounterRepository
    {
        public AccounterRepository(AppDBContext appDBContext) : base(appDBContext) { }
    }
}
