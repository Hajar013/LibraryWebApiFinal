using BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.BorrowerServices
{
    public interface IBorrowerService
    {
        IQueryable<BorrowerDto> FindAll();
       BorrowerDto FindById(int id);
        void Create(BorrowerDto dto);
        void Update(BorrowerDto dto);
        void Delete(BorrowerDto dto);
        bool RequestToBorrowBook(int bookTitle, int borrowerId);
         bool RequestToBill(int bookId, int borrowerId);

    }
}
