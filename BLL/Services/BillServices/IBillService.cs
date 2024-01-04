using BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.BillServices
{
    public interface IBillService
    {
        //IList<BillDto> FindAll();
        BillDto FindByCondition(int id);
        void Create(BillDto dto);
        void Update(BillDto dto);
        void Delete(BillDto dto);
    }
}
