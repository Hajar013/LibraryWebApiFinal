using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.BookRepos
{
    public class BookRepository :RepositoryBase<Book> ,IBookRepository
    {
        public BookRepository(AppDBContext appDBContext):base(appDBContext) { }

    }
}
