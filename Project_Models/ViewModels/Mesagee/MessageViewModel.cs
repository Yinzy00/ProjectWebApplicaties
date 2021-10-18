using Project_Models.ViewModels.Mesagee.Comment;
using Project_Models.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project_Models.ViewModels.Mesagee
{
    public abstract class MessageViewModel
    {
        //props
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime Created { get; set; }
        public UserViewModel User { get; set; }
        public List<ImageViewModel>? Images { get; set; }
        public int AmountOfLikes { get; set; }
        public List<CommentViewModel>? Comments { get; set; }

        //Constructor
        public MessageViewModel(Project_Models.Message m)
        {
            List<ImageViewModel> images = new List<ImageViewModel>();
            m.Images.ForEach(i=>images.Add(new ImageViewModel(i)));

            List<CommentViewModel> comments = new List<CommentViewModel>();
            m.Comments.ForEach(c => comments.Add(new CommentViewModel(c)));

            this.Id = m.Id;
            this.Text = m.Text;
            this.Created = m.Created;
            this.User = new UserViewModel(m.User);
            this.Images = images;
            this.AmountOfLikes = m.AmountOfComments;
            this.Comments = comments;
        }
    }
}
