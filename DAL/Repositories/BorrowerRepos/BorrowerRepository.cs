using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.BorrowerRepos
{
    public class BorrowerRepository : RepositoryBase<Borrower>, IBorrowerRepository
    {
        public BorrowerRepository(AppDBContext appDBContext)
            : base(appDBContext)
        {
        }
    }
}
