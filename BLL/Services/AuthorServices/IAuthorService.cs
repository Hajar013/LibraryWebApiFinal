using BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.AuthorServices
{
    public interface IAuthorService
    {
        IQueryable<AuthorDto> FindAll();
        IQueryable<AuthorDto> FindByCondition(int id);
        void Create(AuthorDto dto);
        void Update(AuthorDto dto);
        void Delete(AuthorDto dto);
    }
}
