using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project_WebApp.Data;
using Project_WebApp.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_WebApp.Controllers
{
    public class UserController : Controller
    {
        ApplicationDbContext DbContext;
        User LoggedInUser;
        public UserController(ApplicationDbContext dbContext)
        {
            DbContext = dbContext;
            //LoggedInUser = DbContext.Users.Where(u=>u.Id == );
            LoggedInUser = new User()
            {
                Id = "id",
                FirstName = "Yari",
                LastName = "Marien",
                Created = new DateTime(),
                Email = "mail@mail.com",
                PhoneNumber = "0456789012",
                EmailConfirmed = true,
                UserName = "Yinzy",
                TwoFactorEnabled = false,
                PhoneNumberConfirmed = true,
                Likes = new List<Like>(),
                Messages = new List<Message>(),

            };
        }
        //[Authorize]
        public IActionResult Index(string id)
        {
            if (id != null)
            {
                var user = DbContext.Users.Where(u => u.Id == id).FirstOrDefault();
                if (user != null)
                {
                    var vm = new UserEditProfileViewModel(user);
                    return View(vm);
                }
            }
            return View(new UserProfileViewModel(LoggedInUser));
        }

        //[Authorize]
        public IActionResult EditProfile()
        {
            return View(new UserEditProfileViewModel(LoggedInUser));
        }
    }
}
