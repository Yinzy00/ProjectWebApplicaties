using Project_WebApp.Data.UnitOfWork;
using Project_WebApp.ViewModels.Message.Post;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_WebApp
{
    public class Post : Message
    {
        //Props
        public string Title { get; set; }
        public bool? Public { get; set; }
        //Navigation props
        [Required]
        public ICollection<PostSubject> PostSubjects { get; set; } = new List<PostSubject>();
        //Constructor
        public Post(): base()
        {

        }

        //Methods
        public void Close()
        {

        }
        public override void AddComment(Comment c)
        {
            //this.Comments.Add(c);
        }
    }
}
