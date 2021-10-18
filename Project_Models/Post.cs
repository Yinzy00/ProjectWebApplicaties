using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Models
{
    public class Post : Message
    {
        //Props
        public string Title { get; set; }
        public bool? Public { get; set; }

        public List<Subject> Subjects { get; set; }
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
