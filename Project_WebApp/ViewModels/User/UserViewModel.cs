using System;
using System.Collections.Generic;
using System.Text;

namespace Project_WebApp.ViewModels.User
{
    public class UserViewModel
    {
        //props
        public string Id { get; set; }
        public string Username { get; set; }
        //Constructor
        public UserViewModel(Project_WebApp.User u)
        {
            this.Id = u.Id;
            this.Username = u.UserName;
        }
        //Methods
    }
}
