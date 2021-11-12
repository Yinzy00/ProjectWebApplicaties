﻿using Microsoft.AspNetCore.Mvc;
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
            else
            {
                return Redirect("../Auth/NoAcces");
            }
        }

        public async Task<IActionResult> Update(CreateEditPostViewModel model)
        {
            var post = await _uow.PostRepository.getById((int)model.Id);
            post = await FillPostObjectWithModel(post, model);
            //post.Public = model.Public;
            //post.Text = model.Text;
            //post.Title = model.Title;
            //model.SubjectsString.Split(',').ToList().ForEach(i =>
            //{
            //    if (!string.IsNullOrEmpty(i))
            //    {
            //        var subject = _uow.SubjectRepository.Get(s => s.Id == Convert.ToInt32(i)).FirstOrDefault();
            //        if (subject != null)
            //        {
            //            post.PostSubjects.Add(new PostSubject()
            //            {
            //                Post = post,
            //                Subject = subject
            //            });
            //        }
            //    }
            //});

            return Redirect("Index/" + post.Id);
        }
        public async Task<IActionResult> Create(CreateEditPostViewModel model)
        {
            var CreatedPost = new Post();
            CreatedPost.UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            CreatedPost = await FillPostObjectWithModel(CreatedPost, model);
            _uow.PostRepository.Create(CreatedPost);
            //foreach (var s in model.SubjectsString.Split(',').ToList())
            //{
            //    if (!string.IsNullOrEmpty(s))
            //    {
            //        var subject = await _uow.SubjectRepository.getById(Convert.ToInt32(s));
            //        if (subject != null)
            //        {
            //            CreatedPost.PostSubjects.Add(new PostSubject()
            //            {
            //                Post = CreatedPost,
            //                Subject = subject
            //            }); ;
            //        }
            //    }
            //}
            await _uow.Save();
            return View("Index/" + CreatedPost.Id);
        }

        public async Task<Post> FillPostObjectWithModel(Post p, CreateEditPostViewModel model)
        {
            //if (model.Id != null)
            //{
            //    p.Id = (int)model.Id;
            //    model.Subjects.ForEach(s =>
            //    {
            //        p.PostSubjects.Add(new PostSubject()
            //        {
            //            PostId = p.Id,
            //            SubjectId = (int)s.Id
            //        });
            //    });
            //}
            p.Title = model.Title;
            p.Text = model.Text;
            p.Public = model.Public;
            p.Created = DateTime.Now;

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
            return p;
        }
        public IActionResult PostNotFound()
        {
            return View();
        }
    }
}
