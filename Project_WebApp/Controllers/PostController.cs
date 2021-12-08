using Microsoft.AspNetCore.Mvc;
using Project_WebApp.Data;
using Project_WebApp.Data.UnitOfWork;
using Project_WebApp.ViewModels.Message.Comment;
using Project_WebApp.ViewModels.Message.Post;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Project_WebApp.Controllers
{
    public class PostController : Controller
    {
        //List<Post> posts = new List<Post>();
        public IUnitOfWork _uow;
        public PostController(IUnitOfWork uow)
        {
            _uow = uow;
            _uow.SubjectRepository.Get();
        }
        /// <summary>
        /// Show existing post by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult Index(int id)
        {
            if (User.FindFirst(ClaimTypes.NameIdentifier) != null)
            {
                ViewData["CurrentUser"] = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            }
            else
            {
                ViewData["CurrentUser"] = null;

            }
            Post p = _uow._PostRepository.GetPostByIdForPostViewMode(id);
            if (p != null)
            {
                var vm = new PostViewModel(p);
                return View(vm);
            }
            else
            {
                return View("PostNotFound");
            }


        }
        public async Task<IActionResult> Editor(int? id)
        {
            if (this.User.Identity.IsAuthenticated)
            {
                ViewBag.FormTitle = "Nieuw bericht";
                ViewBag.New = true;
                ViewBag.Subjects = _uow.SubjectRepository.Get();
                if (id != null)
                {
                    Post p = _uow.PostRepository.Get(p => p.Id == (int)id, p => p.PostSubjects).FirstOrDefault();
                    if (p != null)
                    {
                        var vm = new CreateEditPostViewModel(p);
                        ViewBag.FormTitle = "Bericht bewerken";
                        ViewBag.New = false;
                        return View(vm);
                    }
                    else
                    {
                        return View("PostNotFound");
                    }
                }
                else
                {
                    return View(null);
                }
            }
            else
            {
                return Redirect("../Auth/NoAcces");
            }
        }
        public async Task<IActionResult> Delete(int id)
        {
            //Load all posts into memory for clientCascading to work
            var x = _uow._PostRepository.GetPostByIdForPostViewMode(id);
            _uow.PostRepository.Delete(x);
            await _uow.Save();
            return Redirect("~/Home");
        }
        public async Task<IActionResult> Update(CreateEditPostViewModel model)
        {
            var post = _uow.PostRepository.Get(p => p.Id == (int)model.Id, p => p.PostSubjects).First();
            post = await FillPostObjectWithModel(post, model);

            _uow.PostRepository.Update(post);
            await _uow.Save();
            return Redirect("Index/" + post.Id);
        }
        public async Task<IActionResult> Create(CreateEditPostViewModel model)
        {
            var CreatedPost = new Post()
            {
                Created = DateTime.Now
            };
            CreatedPost.UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            CreatedPost = await FillPostObjectWithModel(CreatedPost, model);
            _uow.PostRepository.Create(CreatedPost);
            await _uow.Save();
            return Redirect("Index/" + CreatedPost.Id);
        }
        public async Task<Post> FillPostObjectWithModel(Post p, CreateEditPostViewModel model)
        {
            p.Title = model.Title;
            p.Text = model.Text;
            p.Public = model.Public;
            if (!string.IsNullOrEmpty(model.SubjectsString))
            {
                p.PostSubjects = new List<PostSubject>();
                foreach (var s in model.SubjectsString.Split(',').ToList())
                {
                    if (!string.IsNullOrEmpty(s))
                    {
                        var subject = await _uow.SubjectRepository.getById(Convert.ToInt32(s));
                        if (subject != null)
                        {
                            p.PostSubjects.Add(new PostSubject()
                            {
                                Post = p,
                                Subject = subject
                            });
                        }
                    }
                }
            }
            return p;
        }
        public async Task PostComment(string text, int postId)
        {
            Comment createdComment = new Comment()
            {
                UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value,
                Created = DateTime.Now,
                Text = text,
                ParentId = postId,
                MessageImages = null
            };
            _uow.CommentRepository.Create(createdComment);
            await _uow.Save();
        }
        public async Task PostLike(int id, bool up)
        {
            //
            var x = _uow.LikeRepository.Get();
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var postid = id;
            var like = new Like() { MessageId = id, UserId = userId };
            if (up)
            {
                _uow.LikeRepository.Create(like);
            }
            else
            {
                _uow.LikeRepository.DeleteWhere(l => l.MessageId == like.MessageId && l.UserId == like.UserId);
            }
            await _uow.Save();
        }
        public IActionResult PostNotFound()
        {
            return View();
        }
    }
}
