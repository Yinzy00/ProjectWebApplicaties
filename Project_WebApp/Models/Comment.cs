using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Project_WebApp
{
    public class Comment : Message
    {
        //props
        [Required]
        [Display(Name = "Parent")]
        public int ParentId { get; set; }
        [Required]
        public Message Parent { get; set; }
        //Constructor
        public Comment() : base()
        {

        }
        //Methods
        public override void AddComment(Comment c)
        {
            //Check current level
            if (this.Parent.GetType() == typeof(Post))
            {
                //If first level => add comment to comment
                this.Comments.Add(c);
            }
            else
            {
                //If level 2 add comment to comment above to prevent big trees
                this.Parent.Comments.Add(c);
            }
        }
    }
}
