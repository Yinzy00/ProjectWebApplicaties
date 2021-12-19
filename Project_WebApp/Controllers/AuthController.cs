using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Project_WebApp.Data.UnitOfWork;
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
            //ViewBag.Message = userName + " - " + firstName + " - " + lastName + " " + password;
            return View();
        }
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (!string.IsNullOrEmpty(model.Email))
            {
                var user = _uow.UserRepository.Get(u => u.Email == model.Email).FirstOrDefault();
                if (user != null)
                {
                    //Send mail & open login view
                    SmtpClient smtpClient = new SmtpClient("send.one.com", 587)
                    {
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        EnableSsl = true,
                        Credentials = new NetworkCredential("school@yarimarien.be", "School123_"),
                    };
                    MailAddress emailSender = new MailAddress("school@yarimarien.be", "Project Web Apps");
                    MailAddress emailReceiver = new MailAddress(model.Email, user.UserName);
                    var newPass = Guid.NewGuid().ToString().Split('-')[0];
                    var identityUser = await UserManager.FindByIdAsync(user.Id);
                    var token = await UserManager.GeneratePasswordResetTokenAsync(identityUser);
                    var result = await UserManager.ResetPasswordAsync(identityUser, token, newPass);
                    MailMessage mailMessage = new MailMessage(emailSender, emailReceiver)
                    {
                        Subject = "Email test",
                        Body = $"Beste {user.UserName}, <br/><br/>Uw tijdelijk wachtwoord is {newPass}.<br/>Meld u aan en wijzig het zo snel mogelijk.<br/>Indien u dit verzoek niet zelf heeft ingevuld heeft wijzig ook zo snel mogelijk uw wachtwoord. Iemand probeerde zich aan te melden met uw account.<br/><br/>Met vriendelijke groeten<br/>Het Question.be Team",
                        IsBodyHtml = true
                    };
                    smtpClient.Send(mailMessage);
                    return Redirect("LogIn");
                }
                else
                {
                    ViewBag.ErrorMessage = "Er bestaat geen account met dit email adres.";
                }
            }
            return View();
        }
        public IActionResult RestorePassword()
        {
            return View();
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
