using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Project_WebApp
{
    /// <summary>
    /// A subject on 
    /// </summary>
    public class Subject
    {
        //props
        [Key]
        [Required]
        public int SubjectId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public DateTime Created { get; set; }
        //Navigation props
        public ICollection<Post>? Posts { get; set; }
        //Extra getters & setters
        public int AmountOfPosts
        {
            get
            {
                return Posts.Count;
            }
        }
        //Constructor
        public Subject()
        {

        }
        //Methods
    }
}
