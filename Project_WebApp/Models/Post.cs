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
        public ICollection<PostSubject> PostSubjects { get; set; }
        //Constructor
        public Post(): base()
        {

        }
        public Post(CreateEditPostViewModel vm)
        {
            if (vm.Id!=null)
            {
                this.Id = (int)vm.Id;
                vm.Subjects.ForEach(s =>
                {
                    this.PostSubjects.Add(new PostSubject()
                    {
                        PostId = this.Id,
                        SubjectId = (int)s.Id
                    });
                });
            }
            this.Title = vm.Title;
            this.Text = vm.Text;
            this.Public = vm.Public;
            this.Created = DateTime.Now;
           /* vm.Subjects.ForEach(s=>PostSubjects.)*/;
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
