using BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.AccounterServices
{
    public interface IAccounterService 
    {
        IQueryable<AccounterDto> FindAll();
        IQueryable<AccounterDto> FindByCondition(int id);
        void Create(AccounterDto dto);
        void Update(AccounterDto dto);
        void Delete(AccounterDto dto);
    }
}
