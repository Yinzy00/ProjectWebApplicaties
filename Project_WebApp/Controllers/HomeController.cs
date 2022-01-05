using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Project_WebApp.Data.UnitOfWork;
using Project_WebApp.Models;
using Project_WebApp.ViewModels.Message.Post;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Project_WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IUnitOfWork _uow;
        public HomeController(ILogger<HomeController> logger, IUnitOfWork uow)
        {
            _uow = uow;
            _logger = logger;
        }

        public IActionResult Index()
        {
            List<RecentPostViewModel> pvm = new List<RecentPostViewModel>();
            var posts = _uow._PostRepository.GetAllPostsIncludeSubjects();
            posts = posts.Skip(posts.Count() - 3);
            posts.ToList().ForEach(p=>pvm.Add(new RecentPostViewModel(p)));
            return View(pvm);
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult About()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Admin()
        {
            return View();
        }
    }
}
