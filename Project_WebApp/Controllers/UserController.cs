using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Project_WebApp.Data;
using Project_WebApp.Data.UnitOfWork;
using Project_WebApp.ViewModels.Message.Post;
using Project_WebApp.ViewModels.User;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Project_WebApp.Controllers
{
    public class UserController : Controller
    {
        IUnitOfWork _uow;
        UserManager<User> _usermanager;
        private readonly IHostingEnvironment _appEnvironment;
        //User LoggedInUser;
        public UserController(IUnitOfWork uow, UserManager<User> um, IHostingEnvironment appEnvironment)
        {
            _usermanager = um;
            _uow = uow;
            _appEnvironment = appEnvironment;
        }
        [Authorize]
        public async Task<IActionResult> Index(string id)
        {
            if (id == null)
            {
                var x = await _usermanager.GetUserAsync(User);
                id = x.Id;
            }
            var user = _uow.UserRepository.Get(u => u.Id == id, u => u.Messages, u => u.Images, u => u.Likes).FirstOrDefault();
            var vm = new UserProfileViewModel(user);
            _uow._PostRepository.GetAllPostsIncludeSubjects(p => p.UserId == user.Id).OrderByDescending(p => p.Created).Take(3).ToList().ForEach(p =>
              {
                  vm.MostRecentPosts.Add(new RecentPostViewModel(p));
              }
            );
            ViewData["CurrentUser"] = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return View(vm);
        }

        [Authorize]
        public async Task<IActionResult> EditProfile()
        {
            return View(new UserEditProfileViewModel(await _usermanager.GetUserAsync(User)));
        }

        public async Task<IActionResult> SaveProfile(UserEditProfileViewModel model)
        {
            var currentUser = _uow.UserRepository.Get(u => u.Id == User.FindFirst(ClaimTypes.NameIdentifier).Value, u => u.Images).First();
            currentUser.UserName = model.UserName;
            currentUser.LastName = model.LastName;
            currentUser.FirstName = model.FirstName;
            currentUser.Email = model.Email;
            currentUser.DateOfBirth = model.DateOfBirth;
            if (model.NewProfilePicture != null)
            {
                var filename = Guid.NewGuid().ToString() + "." + model.NewProfilePicture.FileName.Split('.')[model.NewProfilePicture.FileName.Split('.').Count() - 1];
                var folderPath = Path.Combine(_appEnvironment.WebRootPath, "UPLOADS", currentUser.Id);
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                var path = Path.Combine(folderPath, filename);
                using (var stream = System.IO.File.Create(path))
                {
                    await model.NewProfilePicture.CopyToAsync(stream);
                }
                var i = new Image()
                {
                    Description = "",
                    IsProfilePicture = true,
                    Path = filename,
                    User = currentUser
                };
                currentUser.Images.Add(i);
                _uow.UserRepository.Update(currentUser);
                var old = _uow.ImageRepository.Get(img => img.IsProfilePicture && img.UserId == currentUser.Id).FirstOrDefault();
                if (old != null)
                {
                    path = Path.Combine(folderPath, old.Path);
                    System.IO.File.Delete(path);
                    _uow.ImageRepository.Delete(old);
                }
            }
            _uow.UserRepository.Update(currentUser);
            await _uow.Save();
            return Redirect("Index");
        }
    }
}
