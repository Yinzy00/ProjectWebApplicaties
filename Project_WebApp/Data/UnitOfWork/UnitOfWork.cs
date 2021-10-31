using Project_WebApp.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_WebApp.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext DbContext { get; }
        public UnitOfWork(ApplicationDbContext dbContext)
        {
            DbContext = dbContext;
        }

        private IRepository<User> userRepository;

        private IRepository<Subject> subjectRepository;

        private IRepository<PostSubject> postSubjectRepository;

        private IRepository<Post> postRepository;

        private IRepository<MessageImage> messageImageRepository;

        private IRepository<Like> likeRepository;

        private IRepository<Image> imageRepository;

        private IRepository<Comment> commentRepository;
        public IRepository<User> UserRepository
        {
            get
            {
                if (this.userRepository == null)
                {
                    this.userRepository = new Repository<User>(DbContext);
                }
                return this.userRepository;
            }
        }

        public IRepository<Subject> SubjectRepository
        {
            get
            {
                if (this.subjectRepository == null)
                {
                    this.subjectRepository = new Repository<Subject>(DbContext);
                }
                return this.subjectRepository;
            }
        }

        public IRepository<PostSubject> PostSubjectRepository
        {
            get
            {
                if (this.postSubjectRepository == null)
                {
                    this.postSubjectRepository = new Repository<PostSubject>(DbContext);
                }
                return this.postSubjectRepository;
            }
        }

        public IRepository<Post> PostRepository
        {
            get
            {
                if (this.postRepository == null)
                {
                    this.postRepository = new Repository<Post>(DbContext);
                }
                return this.postRepository;
            }
        }

        public IRepository<MessageImage> MessageImageRepository
        {
            get
            {
                if (this.messageImageRepository == null)
                {
                    this.messageImageRepository = new Repository<MessageImage>(DbContext);
                }
                return this.messageImageRepository;
            }
        }

        public IRepository<Like> LikeRepository
        {
            get
            {
                if (this.likeRepository == null)
                {
                    this.likeRepository = new Repository<Like>(DbContext);
                }
                return this.likeRepository;
            }
        }

        public IRepository<Image> ImageRepository
        {
            get
            {
                if (this.imageRepository == null)
                {
                    this.imageRepository = new Repository<Image>(DbContext);
                }
                return this.imageRepository;
            }
        }

        public IRepository<Comment> CommentRepository
        {
            get
            {
                if (this.commentRepository == null)
                {
                    this.commentRepository = new Repository<Comment>(DbContext);
                }
                return this.commentRepository;
            }
        }

        public void Dispose()
        {
            DbContext.Dispose();
        }

        public async Task Save()
        {
            await DbContext.SaveChangesAsync();
        }
    }
}
