﻿using Project_WebApp.ViewModels.Subject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_WebApp.ViewModels.Message.Post
{
    public class CreateEditPostViewModel
    {
        public CreateEditPostViewModel()
        {

        }
        public CreateEditPostViewModel(Project_WebApp.Post p)
        {
            this.Id = p.Id;
            this.Title = p.Title;
            this.Text = p.Text;
            this.Public = p.Public;
            foreach (var s in p.PostSubjects)
            {
                this.Subjects.Add(new SubjectViewModel(s.Subject));
            }
        }
        public int? Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string SubjectsString { get; set; }
        public List<SubjectViewModel> Subjects { get; set; }
        public bool? Public { get; set; }
    }
}
