using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Project_WebApp.Data.Repositories
{
    public class PostRepository: IPostRepository
    {
        ApplicationDbContext _dbContext { get; }
        public PostRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IEnumerable<Post> GetAllPostsIncludeSubjects(Expression<Func<Post, bool>> conditions = null)
        {

            if (conditions!=null)
            {
                var query = _dbContext.Set<Post>()
               .Include(p => p.PostSubjects)
               .ThenInclude(ps => ps.Subject)
               .Where(conditions);
                return query.ToList();
            }
            else
            {
                var query = _dbContext.Set<Post>()
               .Include(p => p.PostSubjects)
               .ThenInclude(ps => ps.Subject);
                return query.ToList();
            }
        }
        public Post GetPostByIdForPostViewMode(int id)
        
        {
            var post = _dbContext.Set<Post>()
                .Include(p => p.User)
                .Include(p => p.Likes)
                //Layer 1
                .Include(p => p.Comments)
                .ThenInclude(c=>c.Likes)
                .Include(p => p.Comments)
                .ThenInclude(c => c.User)
                //Layer 2
                .Include(p=>p.Comments)
                .ThenInclude(c=>c.Comments)
                .ThenInclude(c => c.Likes)
                .Include(p => p.Comments)
                .ThenInclude(c => c.Comments)
                .ThenInclude(c=>c.User)
                //Layer 3 (Last layer)
                .Include(p => p.Comments)
                .ThenInclude(c => c.Comments)
                .ThenInclude(c=>c.Comments)
                .ThenInclude(c => c.Likes)
                .Include(p => p.Comments)
                .ThenInclude(c => c.Comments)
                .ThenInclude(c => c.Comments)
                .ThenInclude(c => c.User)
                //
                .Include(p => p.MessageImages)
                .Include(p => p.PostSubjects)
                .ThenInclude(ps => ps.Subject)
                .ThenInclude(s=>s.PostSubjects)
                .Where(p=>p.Id == id)
                .FirstOrDefault();
            return post;
        }
    }
}
