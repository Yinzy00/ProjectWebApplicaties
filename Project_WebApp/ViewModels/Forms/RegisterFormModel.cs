using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_WebApp.ViewModels.Forms
{
    public class RegisterFormModel
    {
        public string userName { get; set; }
        public string password { get; set; }
        public string passwordRepeat { get; set; }
        public string email { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public DateTime dateOfBirth { get; set; }
        public string phoneNumber { get; set; }
    }
}
