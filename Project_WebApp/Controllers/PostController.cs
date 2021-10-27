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
        IUnitOfWork _uow;
        public PostController(IUnitOfWork uow)
        {
            _uow = uow;
        }
        /// <summary>
        /// Show existing post by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult Index(int id)
        {
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
            if (id != null)
            {
                Post p = await _uow.PostRepository.getById((int)id);
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
            else
            {
                return View(null);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Editor(PostViewModel model)
        {
            if (model.Id != null)
            {
                Post p = await _uow.PostRepository.getById((int)model.Id);
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
            else
            {
                return View(null);
            }
        }
        public IActionResult PostNotFound()
        {
            return View();
        }
    }
}
