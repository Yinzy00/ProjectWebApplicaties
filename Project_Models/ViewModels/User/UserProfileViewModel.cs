using System;
using System.Collections.Generic;
using System.Text;

namespace Project_Models.ViewModels.User
{
    public class UserProfileViewModel : UserViewModel
    {
        //props
        public int AmountOfPosts { get; set; }
        public int AmountOfComments { get; set; }
        public int AmountOfLikes { get; set; }
        //Constructor
        public UserProfileViewModel(Project_Models.User u): base(u)
        {
            this.AmountOfPosts = u.Messages.FindAll(m=>m.GetType() == typeof(Post)).Count;
            this.AmountOfComments = u.Messages.FindAll(m => m.GetType() == typeof(Comment)).Count;
            this.AmountOfLikes = u.AmountOfLikes;
        }
    }
}
