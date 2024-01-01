using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.LibrarianRepos
{
    public class LibrarianRepository : RepositoryBase<Librarian>, ILibrarianRepository
    {
        public LibrarianRepository(AppDBContext appDBContext)
            : base(appDBContext)
        {
        }
    }
}
