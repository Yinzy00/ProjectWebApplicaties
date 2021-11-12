using Project_WebApp.ViewModels.Message.Post;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_WebApp.ViewModels.SubjectViewModels
{
    public class SubjectDetailViewModel : SubjectViewModel
    {
        public SubjectDetailViewModel(Project_WebApp.Subject s) : base(s)
        {
            s.PostSubjects.ToList().ForEach(ps=> {
                RecentPosts.Add(new RecentPostViewModel(ps.Post));
            });
        }
        public List<RecentPostViewModel> RecentPosts { get; set; } = new List<RecentPostViewModel>();
    }
}
