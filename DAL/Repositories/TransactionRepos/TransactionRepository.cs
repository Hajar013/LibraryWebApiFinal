using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.TransactionRepos
{
    public class TransactionRepository : RepositoryBase<Transaction> ,ITransactionRepository
    {
        public TransactionRepository(AppDBContext dbContext) : base(dbContext) { }
    }
}
