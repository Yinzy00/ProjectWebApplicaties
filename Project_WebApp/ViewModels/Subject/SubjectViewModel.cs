using System;
using System.Collections.Generic;
using System.Text;

namespace Project_WebApp.ViewModels.Subject
{
    public class SubjectViewModel : CreateEditSubjectViewmodel
    {
        //props
       
        public int AmountOfPosts{ get; set; }
        //Constructor
        public SubjectViewModel(Project_WebApp.Subject s): base(s)
        {
            this.AmountOfPosts = s.AmountOfPosts;
        }
    }
}
