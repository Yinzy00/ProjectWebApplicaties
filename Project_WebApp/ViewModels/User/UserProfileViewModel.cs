using Project_WebApp.ViewModels.Message.Post;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project_WebApp.ViewModels.User
{
    public class UserProfileViewModel : UserViewModel
    {
        //props
        public List<RecentPostViewModel> MostRecentPosts { get; set; } = new List<RecentPostViewModel>();
        //Constructor
        public UserProfileViewModel(Project_WebApp.User u) : base(u)
        {
            //u.Messages.Where(u => u.GetType() == typeof(Post)).ToList().ForEach(p => { MostRecentPosts.Add(new RecentPostViewModel((Post)p)); });
        }
    }
}
