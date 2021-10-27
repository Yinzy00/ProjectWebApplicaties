using Microsoft.AspNetCore.Mvc;
using Project_WebApp.Data;
using Project_WebApp.Data.UnitOfWork;
using Project_WebApp.ViewModels.Message.Post;
using System;
using System.Collections.Generic;
using System.Linq;
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
            _uow.SubjectRepository.GetAll();
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
            Post p = _uow.PostRepository.GetAll().Where(p => p.Id == id).FirstOrDefault();
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
            ViewBag.FormTitle = "Nieuw bericht aanmaken";
            ViewBag.Subjects = _uow.SubjectRepository.GetAll();
            if (id != null)
            {
                Post p = await _uow.PostRepository.getById((int)id);
                if (p != null)
                {
                    var vm = new CreateEditPostViewModel(p);
                    ViewBag.FormTitle = "Bericht bewerken";
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
        [HttpPost]
        public async Task<IActionResult> Editor(CreateEditPostViewModel model)
        {
            var CreatedPost = new Post(model);
            CreatedPost.PostSubjects = new List<PostSubject>();
            if (model.Id == null)
            {
                _uow.PostRepository.Create(CreatedPost);
                await _uow.Save();
                CreatedPost = _uow.PostRepository.GetAll().Where(p=>p.Title == CreatedPost.Title && p.Created.Date == DateTime.Now.Date && p.Text == CreatedPost.Text).FirstOrDefault();
            }
            foreach (var s in model.SubjectsString.Split(',').ToList())
            {
                if (!string.IsNullOrEmpty(s))
                {
                    var subject = await _uow.SubjectRepository.getById(Convert.ToInt32(s));
                    if (subject != null)
                    {
                        CreatedPost.PostSubjects.Add(new PostSubject()
                        {
                            PostId = (int)CreatedPost.Id,
                            SubjectId = (int)subject.Id
                        });
                    }
                }
            }
                //_uow.PostRepository.Update(CreatedPost);
            _uow.PostRepository.Update(CreatedPost);
            await _uow.Save();
            return View("Index");
        }
        public IActionResult PostNotFound()
        {
            return View();
        }
    }
}
