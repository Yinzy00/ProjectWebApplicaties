using System;
using System.Collections.Generic;
using System.Text;

namespace Project_WebApp.ViewModels.User
{
    public class UserEditProfileViewModel : UserViewModel
    {
        //props
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public ImageViewModel? ProfilePicture { get; set; }
        //Constructor
        public UserEditProfileViewModel(Project_WebApp.User u): base(u)
        {
            this.FirstName = u.FirstName;
            this.LastName = u.LastName;
            this.Email = u.Email;
            this.DateOfBirth = u.DateOfBirth;
            this.ProfilePicture = new ImageViewModel(u.ProfilePicture);
        }
    }
}
