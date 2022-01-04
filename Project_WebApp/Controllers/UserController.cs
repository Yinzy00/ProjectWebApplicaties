using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Project_WebApp.Data;
using Project_WebApp.Data.UnitOfWork;
using Project_WebApp.Handlers;
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
        private PasswordHasher<User> _hasher;
        //User LoggedInUser;
        public UserController(IUnitOfWork uow, UserManager<User> um, IHostingEnvironment appEnvironment)
        {
            _usermanager = um;
            _uow = uow;
            _appEnvironment = appEnvironment;
            _hasher = new PasswordHasher<User>();
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
        [Authorize]
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
        [Authorize]
        public async Task<IActionResult> DeleteProfile()
        {
            var currentUser = _uow.UserRepository.Get(u => u.Id == User.FindFirst(ClaimTypes.NameIdentifier).Value, u => u.Images).First();

            string url = $"https://{Request.Host.Value}/User/ConfirmDeleteProfile?UserId={currentUser.Id}";
            string urlTag = $"<a href=\"{url}\">Klik hier</a>";

            EmailHandler emh = new EmailHandler();
            emh.SendMail(currentUser.Email, currentUser.UserName, "Delete account", $"Beste {currentUser.UserName},<br/><br/>" +
                $"Indien u uw account wil verwijderen ga dan verder via onderstaande link:<br/>" +
                $"{urlTag}<br/> <br/>" +
                $"Indien u dit niet was raden wij u aan zo snel mogelijk uw wachtwoord te wijzigen.<br/><br/>" +
                $"Met vriendelijke groeten<br/>" +
                $"Het Question.be team  "
                );

            ViewData["Message"] = "Er is een mail verstuurd naar uw email adres.";
            ViewData["CurrentUser"] = currentUser.Id;
            return View("Index", new UserProfileViewModel(currentUser));
        }

        [Authorize]
        public async Task<IActionResult> ConfirmDeleteProfile(string userId)
        {
            var currentUser = _uow.UserRepository.Get(u => u.Id == User.FindFirst(ClaimTypes.NameIdentifier).Value, u => u.Images).First();
            if (userId == currentUser.Id)
            {
                return View(new UserDeleteProfileViewModel() { UserId = userId});
            }
            return Redirect("/Home/Index");
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ConfirmDeleteProfile(UserDeleteProfileViewModel vm)
        {
            var currentUser = _uow.UserRepository.Get(u => u.Id == User.FindFirst(ClaimTypes.NameIdentifier).Value, u => u.Images, u=>u.Messages, u=>u.Likes, u=>u.Images).First();
            if (vm.UserId == currentUser.Id)
            {
                if (vm.Confirmed)
                {
                    if (!string.IsNullOrEmpty(vm.Password))
                    {
                        if (_hasher.VerifyHashedPassword(currentUser, currentUser.PasswordHash, vm.Password) != PasswordVerificationResult.Failed)
                        {
                            var folderPath = Path.Combine(_appEnvironment.WebRootPath, "UPLOADS", currentUser.Id);
                            Directory.Delete(folderPath, true);
                            _uow.UserRepository.Delete(currentUser);
                            await _uow.Save();
                            return Redirect("/Auth/LogOut");
                        }
                        else
                        {
                            ViewBag.Message = "Incorrect wachtwoord.";
                            return View(vm);
                        }
                    }
                    else
                    {
                        ViewBag.Message = "Wachtwoord is een verplicht veld.";
                        return View(vm);
                    }
                }
                else
                {
                    ViewBag.Message = "U moet de bevestiging aanduiden.";
                    return View(vm);
                }
            }
            else
            {
                return Redirect("/Home/Index");
            }
        }
    }
}
