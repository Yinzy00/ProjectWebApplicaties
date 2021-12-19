using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project_WebApp.ViewModels.User
{
    public class UserViewModel
    {
        //props
        public string Id { get; set; }
        public string Username { get; set; }
        public string ProfilePicture { get; set; } = null;
        public int AmountOfPosts { get; set; }
        public int AmountOfComments { get; set; }
        public int AmountOfLikes { get; set; }
        //Constructor
        public UserViewModel(Project_WebApp.User u)
        {
            this.Id = u.Id;
            this.Username = u.UserName;
            if (u.Images.Count>0)
            {
                this.ProfilePicture = u.Images.Where(i => i.IsProfilePicture).First().Path;
            }
            this.AmountOfPosts = u.Messages.Where(m => m.GetType() == typeof(Post)).Count();
            this.AmountOfComments = u.Messages.Where(m => m.GetType() == typeof(Comment)).Count();
            this.AmountOfLikes = u.AmountOfLikes;
        }
        //Methods
    }
}
