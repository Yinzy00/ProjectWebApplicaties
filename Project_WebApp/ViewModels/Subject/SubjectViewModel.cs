using System;
using System.Collections.Generic;
using System.Text;

namespace Project_WebApp.ViewModels.Subject
{
    public class SubjectViewModel
    {
        //props
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int AmountOfPosts{ get; set; }
        //Constructor
        public SubjectViewModel(Project_WebApp.Subject s)
        {
            this.Id = s.Id;
            this.Name = s.Name;
            this.Description = s.Description;
            this.AmountOfPosts = s.AmountOfPosts;
        }
    }
}
