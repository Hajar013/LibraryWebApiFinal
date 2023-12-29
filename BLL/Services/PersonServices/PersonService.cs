using DAL.Repositories.PersonRepos;
using DAL.Repositories;
using LibraryWebApiFinal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.PersonServices
{
    public interface PersonService : ServicesBase<PersonDto>, IPersonService
    {
    }
}
