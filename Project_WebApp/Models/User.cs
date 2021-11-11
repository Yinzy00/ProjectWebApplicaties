using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_WebApp
{
    //[Table("Users")]
    public class User: IdentityUser
    {
        ////Props
        //[Key]
        //public int UserId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        //[Required]
        //public string UserName { get; set; }
        //[Required]
        //public string Email { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public DateTime Created { get; set; }

        //Navigatieproperties
        //public Image ProfilePicture { get; set; }
        public ICollection<Image> Images { get; set; }
        public ICollection<Message> Messages { get; set; }
        public ICollection<Like> Likes { get; set; }
        //Extra getters & setters
        public int AmountOfLikes
        {
            get
            {
                return Likes.Count;
            }
        }
        //Constructor
        public User()
        {
        }
        //Methods
        public void Delete()
        {
            //Delete this user from database
        }
        public void Update()
        {
            //Update this user to database
        }
    }
}
