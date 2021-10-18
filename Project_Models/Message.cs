using System;
using System.Collections.Generic;
using System.Text;

namespace Project_Models
{
    public abstract class Message
    {
        //Props
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Text { get; set; }
        public DateTime Created { get; set; }
        public User User { get; set; }
        public List<Image>? Images { get; set; }
        public List<Like>? Likes { get; set; }
        public List<Comment>? Comments { get; set; }
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
