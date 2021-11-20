using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Project_WebApp.Data.Repositories
{
    public class Repository<T> : IRepository<T> where T : class, new()
    {
        ApplicationDbContext _dbContext { get; }
        public Repository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Create(T entity)
        {
            _dbContext.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
        }
        public void DeleteWhere(Expression<Func<T, bool>> conditions)
        {
            var x = _dbContext.Set<T>().Where(conditions);
            _dbContext.Set<T>().RemoveRange(x);
        }
        public IEnumerable<T> Get()
        {
            return _dbContext.Set<T>().ToList();
        }
        public IEnumerable<T> Get(Expression<Func<T, bool>> conditions)
        {
            return Get(conditions, null);
        }
        public IEnumerable<T> Get(params Expression<Func<T, object>>[] includes)
        {
            return Get(null, includes);
        }

        public IEnumerable<T> Get(Expression<Func<T, bool>> conditions, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbContext.Set<T>();
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }
            if (conditions != null)
            {
                query = query.Where(conditions);
            }
            return query;

        }
        public async Task<T> getById(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public void Update(T entity)
        {
            _dbContext.Set<T>().Update(entity);
        }
    }
}
