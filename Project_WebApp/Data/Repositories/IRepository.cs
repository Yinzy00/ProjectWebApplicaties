using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Project_WebApp.Data.Repositories
{
    public interface IRepository<T> where T : class, new()
    {
        /// <summary>
        /// Get all objects from database
        /// </summary>
        /// <returns>List of <typeparamref name="T"/></returns>
        IEnumerable<T> Get();
        /// <summary>
        /// Get all objects from database with conditions
        /// </summary>
        /// <param name="conditions">Conditions as Linq expression</param>
        /// <returns>List of <typeparamref name="T"/></returns>
        IEnumerable<T> Get(Expression<Func<T, bool>> conditions);
        /// <summary>
        /// Get all objects from database with includes
        /// </summary>
        /// <param name="includes">Includes as Linq expression</param>
        /// <returns>List of <typeparamref name="T"/></returns>
        IEnumerable<T> Get(params Expression<Func<T, object>>[] includes);
        /// <summary>
        /// Get all objects from database with conditions and includes
        /// </summary>
        /// <param name="conditions">Conditions as Linq expression</param>
        /// <param name="includes">Includes as Linq expression</param>
        /// <returns>List of <typeparamref name="T"/></returns>
        IEnumerable<T> Get(Expression<Func<T, bool>> conditions, params Expression<Func<T, object>>[] includes);
        Task<T> getById(int id);
        void Create(T entity);
        void Delete(T entity);
        void Update(T entity);
    }
}
