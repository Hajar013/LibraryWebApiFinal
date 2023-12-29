using LibraryWebApiFinal.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected AppDBContext _appDBContext { get; set; }
        public RepositoryBase(AppDBContext appDBContext)
        {
            _appDBContext = appDBContext;
        }
        public IQueryable<T> FindAll() => _appDBContext.Set<T>().AsNoTracking();
        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression) =>
            _appDBContext.Set<T>().Where(expression).AsNoTracking();
        public void Create(T entity) => _appDBContext.Set<T>().Add(entity);
        public void Update(T entity) => _appDBContext.Set<T>().Update(entity);
        public void Delete(T entity) => _appDBContext.Set<T>().Remove(entity);
    }
}
