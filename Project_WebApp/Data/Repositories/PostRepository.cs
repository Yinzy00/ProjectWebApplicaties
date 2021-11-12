using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public IEnumerable<Post> GetAllPostsIncludeSubjects()
        {
            var list = _dbContext.Set<Post>()
                .Include(p=>p.PostSubjects)
                .ThenInclude(ps=>ps.Subject)
                .ToList();
            return list;
        }
        public Post GetPostByIdForPostViewMode(int id)
        {
            var post = _dbContext.Set<Post>()
                .Include(p => p.User)
                .Include(p => p.Comments)
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
