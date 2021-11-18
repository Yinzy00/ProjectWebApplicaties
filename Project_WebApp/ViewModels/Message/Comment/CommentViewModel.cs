using System;
using System.Collections.Generic;
using System.Text;

namespace Project_WebApp.ViewModels.Message.Comment
{
    public class CommentViewModel : MessageViewModel
    {
        public int ParentId { get; set; }
        //Constructor
        public CommentViewModel(Project_WebApp.Comment c):base(c)
        {
            ParentId = (int)c.ParentId;
        }
        public CommentViewModel(): base()
        {

        }
    }
}
