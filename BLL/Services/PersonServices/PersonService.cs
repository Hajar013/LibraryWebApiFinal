using DAL.Repositories.PersonRepos;
using DAL.Repositories;
using LibraryWebApiFinal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Repositories.RepositoryFactory;

namespace BLL.Services.PersonServices
{
    public  class PersonService : ServicesBase<PersonDto>, IPersonService
    {
        private readonly IRepositoryFactory _repository;

        public PersonService(IRepositoryFactory repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
