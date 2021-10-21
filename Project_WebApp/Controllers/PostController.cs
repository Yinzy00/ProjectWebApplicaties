using Microsoft.AspNetCore.Mvc;
using Project_WebApp.ViewModels.Message.Post;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_WebApp.Controllers
{
    public class PostController : Controller
    {
        List<Post> posts = new List<Post>();
        public PostController()
        {
            //int max = 50;
            //for (int i = 0; i < max; i++)
            //{
            //    posts.Add(new Post()
            //    {
            //        PostId = i,
            //        Comments = new List<Comment>()
            //    {
            //        new Comment()
            //        {
            //            PostId = i+max,
            //            Created = new DateTime(),
            //            ParentId = i,
            //            Text = "1th comment! on post " + i + "!",
            //            UserId = 0
            //        },
            //        new Comment()
            //        {
            //            PostId = i+(max*2),
            //            Created = new DateTime(),
            //            ParentId = i,
            //            Text = "2th comment! on past " + 1 + "!",
            //            UserId = 1
            //        },
            //    }
            //    });
            //}
        }
        /// <summary>
        /// Show existing post by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult Index(int id)
        {
            Post p = posts.Where(p => p.PostId == id).FirstOrDefault();
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
        /// <summary>
        /// Create new post if id == null
        /// Edit post by id if id != null
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult Editor(int? id)
        {
            if (id != null)
            {
                Post p = posts.Where(p => p.PostId == id).FirstOrDefault();
                if (p!=null)
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
