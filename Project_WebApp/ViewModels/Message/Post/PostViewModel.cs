using Project_WebApp.ViewModels.Message;
using Project_WebApp.ViewModels.SubjectViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project_WebApp.ViewModels.Message.Post
{
    public class PostViewModel : MessageViewModel
    {
        //props
        public string Title { get; set; }
        public List<SubjectViewModel> Subjects { get; set; }
        public bool? Public { get; set; }
        //Constructor
        public PostViewModel(Project_WebApp.Post p) : base(p)
        {
            List<SubjectViewModel> subjects = new List<SubjectViewModel>();
            foreach (var item in p.PostSubjects)
            {
                subjects.Add(new SubjectViewModel(item.Subject));
            }
            this.Title = p.Title;
            this.Subjects = subjects;
            this.Public = p.Public;
        }
    }
}
