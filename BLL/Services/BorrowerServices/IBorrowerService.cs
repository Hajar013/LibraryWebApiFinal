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
        IQueryable<BorrowerDto> FindByCondition(int id);
        void Create(BorrowerDto dto);
        void Update(BorrowerDto dto);
        void Delete(BorrowerDto dto);
    }
}
