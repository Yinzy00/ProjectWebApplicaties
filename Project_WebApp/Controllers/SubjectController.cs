using Microsoft.AspNetCore.Mvc;
using Project_WebApp.ViewModels.Subject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_WebApp.Controllers
{
    public class SubjectController : Controller
    {
        List<Subject> subjects = new List<Subject>();
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Editor(int? id)
        {
            if (id!=null)
            {
                var subject = subjects.Where(s => s.SubjectId == id).FirstOrDefault();
                if (subject != null)
                {
                    return View(new SubjectViewModel(subject));
                }
                return View("SubjectNotFound");
            }
            return View(null);
        }

        public IActionResult SubjectNotFound()
        {
            return View();
        }
    }
}
