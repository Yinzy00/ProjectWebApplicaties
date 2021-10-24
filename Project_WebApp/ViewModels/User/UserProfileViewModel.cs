using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project_WebApp.ViewModels.User
{
    public class UserProfileViewModel : UserViewModel
    {
        //props
        public int AmountOfPosts { get; set; }
        public int AmountOfComments { get; set; }
        public int AmountOfLikes { get; set; }
        //Constructor
        public UserProfileViewModel(Project_WebApp.User u): base(u)
        {
            this.AmountOfLikes = u.Messages.Where(m => m.GetType() == typeof(Post)).Count();
            this.AmountOfComments = u.Messages.Where(m => m.GetType() == typeof(Comment)).Count();
            this.AmountOfLikes = u.AmountOfLikes;
        }
    }
}
