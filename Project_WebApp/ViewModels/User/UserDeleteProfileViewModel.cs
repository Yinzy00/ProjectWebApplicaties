using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_WebApp.ViewModels.User
{
    public class UserDeleteProfileViewModel
    {
        public string Password { get; set; }
        public string UserId { get; set; }
        public bool Confirmed { get; set; }
    }
}
