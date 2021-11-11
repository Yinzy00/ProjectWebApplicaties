using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_WebApp.Data.Repositories
{
    public class SubjectRepository : ISubjectRepository
    {
        ApplicationDbContext _dbContext { get; }
        public SubjectRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Subject GetSubjectByIdIncludePosts(int subjectId)
        {
            return _dbContext.Set<Subject>().Where(s=>s.Id == subjectId).Include(s=>s.PostSubjects).ThenInclude(ps=>ps.Post).FirstOrDefault();
        }
    }
}
