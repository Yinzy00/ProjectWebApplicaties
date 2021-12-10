using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_WebApp.Data.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        public CommentRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        ApplicationDbContext _dbContext { get; }
        public Comment GetFullCommentById(int id)
        {
            var comment = _dbContext.Set<Comment>()
                .Include(c => c.Comments)
                .ThenInclude(c => c.Comments)
                .ThenInclude(c => c.Comments)
                .Include(c=>c.Parent)
                .Where(c=>c.Id == id)
                .FirstOrDefault();

            return comment;

        }
    }
}
