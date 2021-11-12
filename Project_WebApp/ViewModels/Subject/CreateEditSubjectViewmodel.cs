using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_WebApp.ViewModels.SubjectViewModels
{
    public class CreateEditSubjectViewmodel
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public CreateEditSubjectViewmodel()
        {

        }
        public CreateEditSubjectViewmodel(Project_WebApp.Subject s)
        {
            this.Id = s.Id;
            this.Name = s.Name;
            this.Description = s.Description;
        }
    }
}
