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
            var list = _dbContext.Set<Post>().Include(p=>p.PostSubjects).ThenInclude(ps=>ps.Subject).ToList();
            return list;
        }
    }
}
