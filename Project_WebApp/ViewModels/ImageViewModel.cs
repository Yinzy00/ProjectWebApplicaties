using System;
using System.Collections.Generic;
using System.Text;

namespace Project_WebApp.ViewModels
{
    public class ImageViewModel
    {
        //props
        public int Id { get; set; }
        public string Path { get; set; }
        public string Description { get; set; }
        //Constructor
        public ImageViewModel(Image i)
        {
            this.Id = i.Id;
            this.Path = i.Path;
            this.Description = i.Description;
        }
        //Methods
    }
}
