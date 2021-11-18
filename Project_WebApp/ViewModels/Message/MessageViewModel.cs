using Project_WebApp.ViewModels.Message.Comment;
using Project_WebApp.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project_WebApp.ViewModels.Message
{
    public abstract class MessageViewModel
    {
        //props
        public int? Id { get; set; }
        public string Text { get; set; }
        public DateTime Created { get; set; }
        public UserViewModel User { get; set; }
        public List<ImageViewModel> Images { get; set; }
        public int AmountOfLikes { get; set; }
        public List<CommentViewModel> Comments { get; set; }

        //Constructor
        public MessageViewModel(Project_WebApp.Message m)
        {
            List<ImageViewModel> images = new List<ImageViewModel>();
            foreach (var MessageImage in m.MessageImages)
            {
                images.Add(new ImageViewModel(MessageImage.Image));
            }

            List<CommentViewModel> comments = new List<CommentViewModel>();
            foreach (var comment in m.Comments)
            {
                comments.Add(new CommentViewModel(comment));
            }

            this.Id = m.Id;
            this.Text = m.Text;
            this.Created = m.Created;
            this.User = new UserViewModel(m.User);
            this.Images = images;
            this.AmountOfLikes = m.AmountOfComments;
            this.Comments = comments;
        }
        public MessageViewModel()
        {

        }
    }
}
