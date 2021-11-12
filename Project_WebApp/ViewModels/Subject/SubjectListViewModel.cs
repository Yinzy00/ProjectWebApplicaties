using Project_WebApp.ViewModels.SubjectViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_WebApp.ViewModels.SubjectViewModels
{
    public class SubjectListViewModel
    {
        public SubjectListViewModel()
        {

        }
        public SubjectListViewModel(List<Subject> subjects)
        {
            subjects.ForEach(s=>SubjectViewModels.Add(new SubjectViewModel(s)));
        }
        public List<SubjectViewModel> SubjectViewModels { get; set; } = new List<SubjectViewModel>();
        public string SearchValue { get; set; }
        public FilterType FilterData { get; set; }

        public enum FilterType
        {
            AZ,
            ZA,
            Recent,
            Popular

        }
    }
}
