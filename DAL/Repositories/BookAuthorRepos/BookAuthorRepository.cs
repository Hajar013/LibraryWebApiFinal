using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.BookAuthorRepos
{
    public class BookAuthorRepository : RepositoryBase<BookAuthor>,IBookAuthorRepository
    {
        public BookAuthorRepository(AppDBContext dbContext) : base(dbContext) { }
    }
}
