using Microsoft.AspNetCore.Mvc;
using Project_WebApp.Data;
using Project_WebApp.Data.UnitOfWork;
using Project_WebApp.ViewModels;
using Project_WebApp.ViewModels.SubjectViewModels;
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
            var subjects = _uow.SubjectRepository.Get(s => s.PostSubjects).ToList();
            //List<SubjectViewModel> l = new List<SubjectViewModel>();
            //subjects.ToList().ForEach(s => l.Add(new SubjectViewModel(s)));
            SubjectListViewModel vm = new SubjectListViewModel(subjects.OrderBy(s=>s.Name).ToList());
            return View(vm);
        }
        public IActionResult Filter(SubjectListViewModel _vm)
        {
            ////FilterViewModel _vm = new FilterViewModel()
            //{
            //    FilterData = (FilterType)Convert.ToInt32(Request.Form["ddlFilter"].ToString()),
            //    SearchValue = Request.Form["txtSearch"].ToString()
            //};
            List<Subject> subjects;
            if (!string.IsNullOrEmpty(_vm.SearchValue))
            {
                subjects = _uow.SubjectRepository.Get(s => s.Name.Contains(_vm.SearchValue) || s.Description.Contains(_vm.SearchValue), s => s.PostSubjects).ToList();
            }
            else
            {
                subjects = _uow.SubjectRepository.Get(s=>s.PostSubjects).ToList();
            }


            switch (_vm.FilterData)
            {
                case SubjectListViewModel.FilterType.AZ:
                    subjects = subjects.OrderBy(s => s.Name).ToList();
                    break;
                case SubjectListViewModel.FilterType.ZA:
                    subjects = subjects.OrderBy(s => s.Name).Reverse().ToList();
                    break;
                case SubjectListViewModel.FilterType.Recent:
                    subjects = subjects.OrderBy(s => s.Created).ToList();
                    break;
                case SubjectListViewModel.FilterType.Popular:
                    subjects = subjects.OrderBy(s => s.AmountOfPosts).Reverse().ToList();
                    break;
                default:
                    break;
            }
            SubjectListViewModel vm = new SubjectListViewModel(subjects);
            vm.SearchValue = _vm.SearchValue;
            vm.FilterData = _vm.FilterData;
            return View("Index", vm);
        }
        public async Task<IActionResult> Editor(int? id)
        {
            if (this.User.Identity.IsAuthenticated)
            {
                ViewBag.FormTitle = "Onderwerp aanmaken";
                ViewBag.NewSubject = true;
                if (id != null)
                {
                    var subject = await _uow.SubjectRepository.getById((int)id);
                    /*DbContext.Subjects.Where(s => s.Id == id).FirstOrDefault();*/
                    if (subject != null)
                    {
                        ViewBag.FormTitle = "Onderwerp bewerken";
                        ViewBag.NewSubject = false;
                        return View(new SubjectViewModel(subject));
                    }
                    return View("SubjectNotFound");
                }
                return View(null);
            }
            else
            {
                return Redirect("../Auth/NoAcces");
            }
        }

        public async Task<IActionResult> Update(CreateEditSubjectViewmodel model)
        {
            _uow.SubjectRepository.Update(new Project_WebApp.Subject(model));
            await _uow.Save();
            return Redirect("Detail/" + model.Id);
        }
        public async Task<IActionResult> Create(CreateEditSubjectViewmodel model)
        {
            var NewSubject = new Project_WebApp.Subject(model);
            NewSubject.Created = DateTime.Now;
            _uow.SubjectRepository.Create(NewSubject);
            await _uow.Save();
            return Redirect("Detail/" + model.Id);
        }

        public IActionResult Detail(int id)
        {
            //var subject =_uow.SubjectRepository.Get(s=>s.Id == id, s=>s.PostSubjects).FirstOrDefault();
            var subject = _uow._SubjectRepository.GetSubjectByIdIncludePosts(id);
            if (subject != null)
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
