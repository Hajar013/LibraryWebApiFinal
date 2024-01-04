using BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.TransactionServices
{
    public interface ITransactionService
    {
        //List<TransactionDto> FindAll();
        TransactionDto FindByCondition(int id);
        void Create(TransactionDto dto);
        void Update(TransactionDto dto);
        void Delete(TransactionDto dto);
    }
}
