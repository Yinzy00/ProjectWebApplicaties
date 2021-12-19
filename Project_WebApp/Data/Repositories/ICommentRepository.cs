using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_WebApp.Data.Repositories
{
    public interface ICommentRepository
    {
        /// <summary>
        /// Gets comment with all subcomments
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Comment GetFullCommentById(int id);
    }
}
