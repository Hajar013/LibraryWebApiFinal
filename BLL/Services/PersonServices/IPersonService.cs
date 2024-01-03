using BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.PersonServices
{
    public interface IPersonService 
    {
        List<PersonDto> FindAll();
        List<PersonDto> FindByCondition(int id);
        //IQueryable<PersonDto> FindByCondition(Expression<Func<PersonDto, bool>> expression);
        void Create(PersonDto dto);
        void Update(PersonDto dto);
        void Delete(PersonDto dto);
    }
}
