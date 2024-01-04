using DAL.Repositories.RepositoryFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public abstract class ServicesBase<T> : IServicesBase<T> where T : class
    {
        private readonly IRepositoryFactory _repository;

        public ServicesBase(IRepositoryFactory repository)
        {
            _repository = repository;
        }
        public void Create(T dto)
        {
            throw new NotImplementedException();
        }

        public void Delete(T dto)
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> FindAll()
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public void Update(T dto)
        {
            throw new NotImplementedException();
        }
    }
}
