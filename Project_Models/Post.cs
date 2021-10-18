using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Models
{
    public class Post
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public bool Public { get; set; }
        public DateTime Created { get; set; }
        public int ParentId { get; set; }

        public List<Subject> Subjects { get; set; }
        public User User { get; set; }
        public Post? Parent { get; set; }
        public Image? Images { get; set; }
        public List<Like>? Likes { get; set; }
    }
}
