using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Project_WebApp.ViewModels.User
{
    public class UserEditProfileViewModel
    {
        //props
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string ProfilePicturePath { get; set; }
        public IFormFile NewProfilePicture { get; set; }
        //Constructor
        public UserEditProfileViewModel()
        {

        }
        public UserEditProfileViewModel(Project_WebApp.User u)
        {
            this.UserName = u.UserName;
            this.FirstName = u.FirstName;
            this.LastName = u.LastName;
            this.Email = u.Email;
            this.DateOfBirth = u.DateOfBirth;
            var img = u.Images.Where(i => i.IsProfilePicture).FirstOrDefault();
            if (img != null)
                this.ProfilePicturePath = img.Path;
            else
                this.ProfilePicturePath = string.Empty;
            //this.ProfilePicture = new ImageViewModel(u.ProfilePicture);
        }
    }
}
