using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_WebApp.Data.Repositories
{
    public class Repository<T> : IRepository<T> where T : class, new()
    {
        ApplicationDbContext DbContext { get; }
        public Repository(ApplicationDbContext dbContext)
        {
            DbContext = dbContext;
        }
        public void Create(T entity)
        {
            DbContext.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            DbContext.Set<T>().Remove(entity);
        }

        public IQueryable<T> GetAll()
        {
            return DbContext.Set<T>();
        }

        public async Task<T> getById(int id)
        {
            return await DbContext.Set<T>().FindAsync(id);
        }

        public void Update(T entity)
        {
            DbContext.Set<T>().Update(entity);
        }
    }
}
