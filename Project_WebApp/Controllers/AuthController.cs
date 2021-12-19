using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Project_WebApp.Data.UnitOfWork;
using Project_WebApp.Handlers;
using Project_WebApp.ViewModels.Auth;
using Project_WebApp.ViewModels.Forms;
using Project_WebApp.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Project_WebApp.Controllers
{
    public class AuthController : Controller
    {
        private UserManager<User> UserManager;
        private SignInManager<User> SignInManager;
        private IUnitOfWork _uow;
        public AuthController(UserManager<User> userManager,
            SignInManager<User> signInManager, IUnitOfWork uow)
        {
            _uow = uow;
            UserManager = userManager;
            SignInManager = signInManager;
        }
        public IActionResult LogIn()
        {
            return View();
        }



        [HttpPost]
        //public async Task<IActionResult> LogIn(string userName, string password)
        public async Task<IActionResult> LogIn(LoginFormModel model)
        {
            var result = await SignInManager.PasswordSignInAsync(model.userName, model.password, false, false);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.LoginMessage = "Foute gebruikersnaam of wachtwoord";
            }
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterFormModel model)
        {
            try
            {
                ViewBag.RegisterMessage = "Foutmelding";
                User user = await UserManager.FindByEmailAsync(model.email);
                User UserNameCheck = _uow.UserRepository.Get(u => u.UserName == model.userName).FirstOrDefault();
                if (model.password == model.passwordRepeat)
                {
                    if (user == null && UserNameCheck == null)
                    {
                        user = new User()
                        {
                            UserName = model.userName,
                            Email = model.email,
                            FirstName = model.firstName,
                            LastName = model.lastName,
                            DateOfBirth = model.dateOfBirth,
                            Created = new DateTime(),
                            PhoneNumber = model.phoneNumber
                        };
                        IdentityResult result = await UserManager.CreateAsync(user, model.password);
                        if (result.Succeeded == true)
                        {
                            ViewBag.RegisterMessage = "Account is aangemaakt!";
                            return View("LogIn");
                        }
                        else
                        {
                            ViewBag.RegisterMessage = "Wachtwoord is niet sterk genoeg!";
                        }
                    }
                    else
                    {
                        ViewBag.RegisterMessage = "Dit email adres en of gebruikersnaam is al in gebruik!";
                        return View();
                    }
                }
                else
                {
                    ViewBag.RegisterMessage = "Wachtwoorden komen niet overeen!";
                    return View();
                }
            }
            catch (Exception ex)
            {
                ViewBag.RegisterMessage = ex.Message;
            }
            return View();
        }
        public IActionResult ForgotPassword(ForgotPasswordViewModel model)
        {
            if (!string.IsNullOrEmpty(model.Email))
            {
                var user = _uow.UserRepository.Get(u => u.Email == model.Email).FirstOrDefault();
                if (user != null)
                {
                    string url = $"https://{Request.Host.Value}/Auth/RestorePassword?UserId={user.Id}&RestoreKey={user.RestorePasswordKey}";
                    string urlTag = $"<a href=\"{url}\">Klik hier</a>";

                    string msg = $"Beste {user.UserName}, <br/><br/>" +
                 $"U kan uw password wijzigen via onderstaande link:<br/>" +
                 $"{urlTag}<br/><br/>" +
                 $"Met vriendelijke groeten<br/>" +
                 $"Het Question.be Team";
                    EmailHandler emh = new EmailHandler();
                    emh.SendMail(user.Email, user.UserName, "Wachtwoord vergeten", msg);
                    return Redirect("LogIn");
                }
                else
                {
                    ViewBag.ErrorMessage = "Er bestaat geen account met dit email adres.";
                }
            }
            return View();
        }
        public IActionResult RestorePassword(RestorePasswordViewModel model)
        {
            return View(model);
        }
        public async Task<IActionResult> RestorePasswordSend(RestorePasswordViewModel model)
        {
            if (!string.IsNullOrEmpty(model.UserId) && !string.IsNullOrEmpty(model.RestoreKey))
            {
                if (!string.IsNullOrEmpty(model.NewPass) && !string.IsNullOrEmpty(model.NewPassRepeat) && model.NewPass == model.NewPassRepeat)
                {
                    var user = _uow.UserRepository.Get(u => u.Id == model.UserId).First();
                    var token = await UserManager.GeneratePasswordResetTokenAsync(user);
                    var result = await UserManager.ResetPasswordAsync(user, token, model.NewPass);
                    if (result.Succeeded)
                    {
                        EmailHandler emh = new EmailHandler();
                        emh.SendMail(
                            user.Email,
                            user.UserName,
                            "Wachtwoord gewijzigd", $"Beste {user.UserName}, <br/><br/>" +
                            $"Uw wachtwoord is gewijzigd!<br/><br/>" +
                            $"Met vriendelijke groeten<br/>" +
                            $"Het Question.be Team"
                            );
                        user.RestorePasswordKey = null;
                        _uow.UserRepository.Update(user);
                        return Redirect("Login");
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "Wachtwoord voldoet niet aan de voorwaarden.";
                    }
                }
                else
                {
                    ViewBag.ErrorMessage = "Beide wachtwoord velden zijn verplicht en moeten het zelfde zijn!";
                }
            }
            return View("RestorePassword", model);

        }
        public async Task<IActionResult> LogOut()
        {
            await SignInManager.SignOutAsync();
            return Redirect("LogIn");
        }
        public IActionResult NoAcces()
        {
            return View();
        }
    }
}
