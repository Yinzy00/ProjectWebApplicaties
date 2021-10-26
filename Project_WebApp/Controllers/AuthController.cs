using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_WebApp.Controllers
{
    public class AuthController : Controller
    {
        private UserManager<User> UserManager;
        private SignInManager<User> SignInManager;
        public AuthController(UserManager<User> userManager,
            SignInManager<User> signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }
        public IActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LogIn(string userName, string password)
        {
            var result = await SignInManager.PasswordSignInAsync(userName, password, false, false);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Message = "resultaat is: " + result.ToString();
            }
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(string userName, string email, string firstName, string lastName,
            DateTime dateOfBirth, string phoneNumber, string password)
        {
            try
            {
                ViewBag.Message = "Dit email adres is al in gebruik!";

                User user = await UserManager.FindByEmailAsync(email);
                if (user == null)
                {
                    user = new User()
                    {
                        UserName = userName,
                        Email = email,
                        FirstName = firstName,
                        LastName = lastName,
                        DateOfBirth = dateOfBirth,
                        Created = new DateTime(),
                        PhoneNumber = phoneNumber
                    };
                    IdentityResult result = await UserManager.CreateAsync(user, password);
                    ViewBag.Message = "Account is aangemaakt!";
                    return View("LogIn");
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
            }
            //ViewBag.Message = userName + " - " + firstName + " - " + lastName + " " + password;
            return View();
        }
        public IActionResult ForgotPassword()
        {
            return View();
        }
        public IActionResult RestorePassword()
        {
            return View();
        }
        public async Task<IActionResult> LogOut()
        {
            await SignInManager.SignOutAsync();
            return View("LogIn");
        }
        public IActionResult NoAcces()
        {
            return View();
        }
    }
}
