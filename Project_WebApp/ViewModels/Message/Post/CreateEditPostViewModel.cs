using Project_WebApp.ViewModels.SubjectViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_WebApp.ViewModels.Message.Post
{
    public class CreateEditPostViewModel
    {
        public CreateEditPostViewModel()
        {

        }
        public CreateEditPostViewModel(Project_WebApp.Post p)
        {
            this.Id = p.Id;
            this.Title = p.Title;
            this.Text = p.Text;
            if (p.Public != null)
            {
                this.Public = (bool)p.Public;
            }
            else
            {
                this.Public = false;
            }
            foreach (var s in p.PostSubjects)
            {
                this.Subjects.Add(new SubjectViewModel(s.Subject));
            }
        }
        public int? Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string SubjectsString
        {
            get
            {
                var returnString = "";
                Subjects.ForEach(s => returnString += s.Id + ",");
                return returnString;
            }
        }
        public List<SubjectViewModel> Subjects { get; set; } = new List<SubjectViewModel>();
        public bool Public { get; set; }
    }
}
