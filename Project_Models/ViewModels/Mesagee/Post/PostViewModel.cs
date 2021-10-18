using Project_Models.ViewModels.Mesagee;
using Project_Models.ViewModels.Mesagee.Comment;
using Project_Models.ViewModels.Subject;
using Project_Models.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project_Models.ViewModels.Message.Post
{
    public class PostViewModel : MessageViewModel
    {
        //props
        public string Title { get; set; }
        public List<SubjectViewModel> Subjects { get; set; }
        public bool? Public { get; set; }
        //Constructor
        public PostViewModel(Project_Models.Post p):base(p)
        {
            List<SubjectViewModel> subjects = new List<SubjectViewModel>();
            p.Subjects.ForEach(s=>subjects.Add(new SubjectViewModel(s)));

            this.Title = p.Title;
            this.Subjects = subjects;
            this.Public = p.Public;
        }
    }
}
