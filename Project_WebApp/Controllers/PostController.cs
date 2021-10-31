using Microsoft.AspNetCore.Mvc;
using Project_WebApp.Data;
using Project_WebApp.Data.UnitOfWork;
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
            
            //if(ViewBag.Subjects = null)
            //{
            //    ViewBag.Subjects = new List<Subject>();
            //}
            Post p = _uow.PostRepository.Get(p=>p.Comments, p=>p.MessageImages, p=>p.User).FirstOrDefault();
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
            ViewBag.FormTitle = "Nieuw bericht";
            ViewBag.New = true;
            ViewBag.Subjects = _uow.SubjectRepository.Get();
            if (id != null)
            {
                Post p = await _uow.PostRepository.getById((int)id);
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
        //[HttpPost]
        //public async Task<IActionResult> Editor(CreateEditPostViewModel model)
        //{
        //        else if(!ViewBag.New)
        //    {
                
        //    }
        //    //var CreatedPost = new Post(model);
        //    //CreatedPost.PostSubjects = new List<PostSubject>();
        //    //if (model.Id == null)
        //    //{
        //    //    _uow.PostRepository.Create(CreatedPost);
        //    //    await _uow.Save();
        //    //    CreatedPost = _uow.PostRepository.GetAll().Where(p=>p.Title == CreatedPost.Title && p.Created.Date == DateTime.Now.Date && p.Text == CreatedPost.Text).FirstOrDefault();
        //    //}
        //    //foreach (var s in model.SubjectsString.Split(',').ToList())
        //    //{
        //    //    if (!string.IsNullOrEmpty(s))
        //    //    {
        //    //        var subject = await _uow.SubjectRepository.getById(Convert.ToInt32(s));
        //    //        if (subject != null)
        //    //        {
        //    //            CreatedPost.PostSubjects.Add(new PostSubject()
        //    //            {
        //    //                PostId = (int)CreatedPost.Id,
        //    //                SubjectId = (int)subject.Id
        //    //            });
        //    //        }
        //    //    }
        //    //}
        //    //    //_uow.PostRepository.Update(CreatedPost);
        //    await _uow.Save();
        //    return View("Index");
        //}
        public async Task<IActionResult> Update(CreateEditPostViewModel model)
        {
            var Post = await _uow.PostRepository.getById((int)model.Id);
            return View();
        }
        public async Task<IActionResult> Create(CreateEditPostViewModel model)
        {
            var CreatedPost = new Post(model);
            CreatedPost.UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            CreatedPost.PostSubjects = new List<PostSubject>();
            _uow.PostRepository.Create(CreatedPost);
            //await _uow.Save();
            //CreatedPost = _uow.PostRepository.GetAll().Where(p => p.Title == CreatedPost.Title && p.Created.Date == DateTime.Now.Date && p.Text == CreatedPost.Text).FirstOrDefault();
            foreach (var s in model.SubjectsString.Split(',').ToList())
            {
                if (!string.IsNullOrEmpty(s))
                {
                    var subject = await _uow.SubjectRepository.getById(Convert.ToInt32(s));
                    if (subject != null)
                    {
                        CreatedPost.PostSubjects.Add(new PostSubject()
                        {
                            Post = CreatedPost,
                            Subject = subject
                            //PostId = (int)CreatedPost.Id,
                            //SubjectId = (int)subject.Id
                        }); ;
                    }
                }
            }
            await _uow.Save();
            //_uow.PostRepository.Update(CreatedPost);
            //return Index(CreatedPost.Id);
            return View(new PostViewModel(CreatedPost));
        }
        public IActionResult PostNotFound()
        {
            return View();
        }
    }
}
