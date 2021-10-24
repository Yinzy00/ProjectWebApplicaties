using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Project_WebApp
{
    public abstract class Message
    {
        //Props
        public int Id { get; set; }
        [Required]
        [Display(Name = "User")]
        public int UserId { get; set; }
        [Required]
        public string Text { get; set; }
        [Required]
        public DateTime Created { get; set; }

        //Navigation props
        public User User { get; set; }
        public ICollection<MessageImage> MessageImages { get; set; }
        public ICollection<Like> Likes { get; set; }
        public ICollection<Comment> Comments { get; set; }
        //Extra getters
        public int AmountOfComments
        {
            get
            {
                return this.Comments.Count;

            }
        }
        //Methods
        public void Delete()
        {
            //Remove this post from db
        }
        public void Update()
        {
            //Update this post to db
        }
        //Constructor
        public Message()
        {

        }
        //Implement in child classes
        public abstract void AddComment(Comment c);
    }
}
