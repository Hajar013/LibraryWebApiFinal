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
        PersonDto FindByCondition(int id);
        void Create(PersonDto dto);
        void Update(PersonDto dto);
        void Delete(PersonDto dto);
    }
}
