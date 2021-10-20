﻿using Project_WebApp.ViewModels.Subject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project_WebApp.ViewModels.Message.Post
{
    public class RecentPostViewModel
    {
        //props
        public int Id { get; set; }
        public string Title { get; set; }
        public List<SubjectViewModel> Subjects { get; set; }
        public int AmountOfComments { get; set; }
        //Constructor
        public RecentPostViewModel(Project_WebApp.Post p)
        {
            List<SubjectViewModel> subjects = new List<SubjectViewModel>();
            p.Subjects.ToList().ToList().ForEach(s=>subjects.Add(new SubjectViewModel(s)));

            this.Id = p.PostId;
            this.Title = p.Title;
            this.Subjects = subjects;
            this.AmountOfComments = p.AmountOfComments;
        }
    }
}
