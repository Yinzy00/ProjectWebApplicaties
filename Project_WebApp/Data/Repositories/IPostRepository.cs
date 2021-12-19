using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Project_WebApp.Data.Repositories
{
    public interface IPostRepository
    {
        /// <summary>
        /// /// Get all posts from db include postSubjects thenInclude Subjects
        /// </summary>
        /// <param name="include"></param>
        /// <param name="thenInclude"></param>
        /// <returns></returns>
        public IEnumerable<Post> GetAllPostsIncludeSubjects(Expression<Func<Post, bool>> conditions = null);
        /// <summary>
        /// /// Get post by id from db include postSubjects thenInclude Subjects
        /// </summary>
        /// <param name="include"></param>
        /// <param name="thenInclude"></param>
        /// <returns></returns>
        public Post GetPostByIdForPostViewMode(int id);
    }
}
