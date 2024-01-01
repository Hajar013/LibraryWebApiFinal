using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.BillRepos
{
    public class BillRepository : RepositoryBase<Bill> ,IBillRepository
    {
        public BillRepository(AppDBContext dbContext) : base(dbContext) { }

    }
}
