using Project_WebApp.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_WebApp.Data.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<User> UserRepository { get; }
        IRepository<Subject> SubjectRepository { get; }
        IRepository<PostSubject> PostSubjectRepository { get; }
        IRepository<Post> PostRepository { get; }
        IRepository<MessageImage> MessageImageRepository { get; }
        IRepository<Like> LikeRepository { get; }
        IRepository<Image> ImageRepository { get; }
        IRepository<Comment> CommentRepository { get; }

        //Custom
        IPostRepository _PostRepository { get; }
        ISubjectRepository _SubjectRepository { get; }
        Task Save();
    }
}
