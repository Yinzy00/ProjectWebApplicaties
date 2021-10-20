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
        [Key]
        [Required]
        public string Title { get; set; }
        public bool? Public { get; set; }
        //Navigation props
        public ICollection<Subject> Subjects { get; set; }
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
            this.Comments.Add(c);
        }
    }
}
