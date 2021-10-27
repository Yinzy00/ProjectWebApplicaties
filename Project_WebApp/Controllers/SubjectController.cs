using Microsoft.AspNetCore.Mvc;
using Project_WebApp.Data;
using Project_WebApp.Data.UnitOfWork;
using Project_WebApp.ViewModels.Subject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_WebApp.Controllers
{
    public class SubjectController : Controller
    {
        //List<Subject> subjects = new List<Subject>();
        IUnitOfWork _uow;
        public SubjectController(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Editor(int? id)
        {
            ViewBag.FormTitle = "Onderwerp aanmaken";
            if (id != null)
            {
                var subject = await _uow.SubjectRepository.getById((int)id);
                    /*DbContext.Subjects.Where(s => s.Id == id).FirstOrDefault();*/
                if (subject != null)
                {
                    ViewBag.FormTitle = "Onderwerp bewerken";
                    return View(new SubjectViewModel(subject));
                }
                return View("SubjectNotFound");
            }
            return View(null);
        }
        [HttpPost]
        public async Task<IActionResult> Editor(CreateEditSubjectViewmodel model)
        {
            //var CreatedSubject = new Project_WebApp.Subject() { Id = (int)model.Id, Name = model.Name, Description = model.Description, Created = new DateTime() };

            if (model.Id != null)
            {
                _uow.SubjectRepository.Update(new Project_WebApp.Subject(model));
                //var Subject = _uow.SubjectRepository.getById((int)model.Id);
                //if (Subject == null)
                //{
                //}
                //else
                //{
                //}
                //var subject = await _uow.SubjectRepository.getById((int)model.Id);
                /*DbContext.Subjects.Where(s => s.Id == id).FirstOrDefault();*/
                //if (subject != null)
                //{
                //    return View(new SubjectViewModel(subject));
                //}
                //return View("SubjectNotFound");
            }
            else
            {
                _uow.SubjectRepository.Create(new Project_WebApp.Subject(model));
            }
            await _uow.Save();
            return View(null);
        }

        public IActionResult SubjectNotFound()
        {
            return View();
        }
    }
}
