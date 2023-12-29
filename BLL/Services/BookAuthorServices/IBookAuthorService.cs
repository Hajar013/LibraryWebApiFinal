using BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.BookAuthorServices
{
    public interface IBookAuthorService
    {
        IQueryable<BookAuthorDto> FindAll();
/*        IQueryable<BookAuthorDto> FindByCondition(int id);
*/        void Create(BookAuthorDto dto);
        void Update(BookAuthorDto dto);
        void Delete(BookAuthorDto dto);
    }
}
