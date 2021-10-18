using System;
using System.Collections.Generic;
using System.Text;

namespace Project_Models.ViewModels.User
{
    public class UserViewModel
    {
        //props
        public int Id { get; set; }
        public string Username { get; set; }
        //Constructor
        public UserViewModel(Project_Models.User u)
        {
            this.Id = u.Id;
            this.Username = u.Username;
        }
        //Methods
    }
}
