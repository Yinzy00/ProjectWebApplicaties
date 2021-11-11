using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_WebApp.Data.Repositories
{
    public interface ISubjectRepository
    {
        /// <summary>
        /// /// Get all subjects from db include postSubjects thenInclude Posts
        /// </summary>
        /// <param name="include"></param>
        /// <param name="thenInclude"></param>
        /// <returns></returns>
        public Subject GetSubjectByIdIncludePosts(int subjectId);
    }
}
