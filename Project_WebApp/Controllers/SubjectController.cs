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
            var subjects = _uow.SubjectRepository.Get(s=>s.PostSubjects);
            List<SubjectViewModel> l = new List<SubjectViewModel>();
            subjects.ToList().ForEach(s=>l.Add(new SubjectViewModel(s)));
            return View(l);
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
                var NewSubject = new Project_WebApp.Subject(model);
                NewSubject.Created = DateTime.Now;
                _uow.SubjectRepository.Create(NewSubject);
            }
            await _uow.Save();
            return View(null);
        }

        public IActionResult Detail(int id)
        {
            //var subject =_uow.SubjectRepository.Get(s=>s.Id == id, s=>s.PostSubjects).FirstOrDefault();
            var subject = _uow._SubjectRepository.GetSubjectByIdIncludePosts(id);
            if (subject!=null)
            {
                return View(new SubjectDetailViewModel(subject));

            }
            else
            {
                return View("SubjectNotFound");
            }
        }
        public IActionResult SubjectNotFound()
        {
            return View();
        }
    }
}
