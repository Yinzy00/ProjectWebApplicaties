﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Project_WebApp.Data;
using Project_WebApp.Data.UnitOfWork;
using Project_WebApp.ViewModels.Message.Post;
using Project_WebApp.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Project_WebApp.Controllers
{
    public class UserController : Controller
    {
        IUnitOfWork _uow;
        UserManager<User> _usermanager;
        //User LoggedInUser;
        public UserController(IUnitOfWork uow, UserManager<User> userManager)
        {
            _usermanager = userManager;
            _uow = uow;

            //LoggedInUser = DbContext.Users.Where(u=>u.Id == );
            //LoggedInUser = new User()
            //{
            //    Id = "id",
            //    FirstName = "Yari",
            //    LastName = "Marien",
            //    Created = new DateTime(),
            //    Email = "mail@mail.com",
            //    PhoneNumber = "0456789012",
            //    EmailConfirmed = true,
            //    UserName = "Yinzy",
            //    TwoFactorEnabled = false,
            //    PhoneNumberConfirmed = true,
            //    Likes = new List<Like>(),
            //    Messages = new List<Message>(),

            //};
        }
        //[Authorize]
        public async Task<IActionResult> Index(string id)
        {
            if (id == null)
            {
                var x = await _usermanager.GetUserAsync(User);
                id = x.Id;
            }
            var user = _uow.UserRepository.Get(u => u.Id == id, u=>u.Messages, u => u.Images, u => u.Likes).FirstOrDefault();
            var vm = new UserProfileViewModel(user);
            _uow._PostRepository.GetAllPostsIncludeSubjects(p => p.UserId == user.Id).OrderByDescending(p=>p.Created).Take(3).ToList().ForEach(p =>
            {
                vm.MostRecentPosts.Add(new RecentPostViewModel(p));
            }
            );
            ViewData["CurrentUser"] = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return View(vm);
            //if (id != null)
            //{
            //    var user = _uow.UserRepository.Get(u => u.Id == id).FirstOrDefault();
            //    if (user != null)
            //    {
            //        var vm = new UserEditProfileViewModel(user);
            //        return View(vm);
            //    }
            //}
            //return View(new UserProfileViewModel(User));
        }

        //[Authorize]
        public async Task<IActionResult> EditProfile()
        {
            return View(new UserEditProfileViewModel(await _usermanager.GetUserAsync(User)));
        }

        //public class EditProfileFormModel
        //{
        //    public string userName { get; set; }
        //    public string lastName { get; set; }
        //    public string firstName { get; set; }
        //    public string email { get; set; }
        //    public DateTime dateOfBirth { get; set; }
        //}
        [HttpPost]
        public async Task<IActionResult> SaveProfile(UserEditProfileViewModel model)
        {
            var currentUser = await _usermanager.GetUserAsync(User);
            currentUser.UserName = model.Username;
            currentUser.LastName = model.LastName;
            currentUser.FirstName = model.FirstName;
            currentUser.Email = model.Email;
            currentUser.DateOfBirth = model.DateOfBirth;
            _uow.UserRepository.Update(currentUser);
            return View("Index");
        }
    }
}
