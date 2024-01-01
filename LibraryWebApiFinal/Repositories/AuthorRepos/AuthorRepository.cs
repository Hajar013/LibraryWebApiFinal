using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.AuthorRepos
{
    public class AuthorRepository : RepositoryBase<Author>,IAuthorRepository
    {
        public AuthorRepository(AppDBContext appDBContext) : base(appDBContext) { }
    }
}
