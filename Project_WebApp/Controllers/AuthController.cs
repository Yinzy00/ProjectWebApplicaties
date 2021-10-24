using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_WebApp.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Register()
        {
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
        public IActionResult LogOut()
        {
            return View();
        }
        public IActionResult NoAcces()
        {
            return View();
        }
    }
}
