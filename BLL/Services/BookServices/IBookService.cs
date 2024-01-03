using BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.BookServices
{
    public interface IBookService
    {
        List<BookDto> FindAll();
        BookDto FindByCondition(int id);
        List<BookDto> FindByTitle(string title);
        void Create(BookDto dto);
        void Update(BookDto dto);
        void Delete(BookDto dto);
    }
}
