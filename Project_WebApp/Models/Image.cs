using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_WebApp
{
    public class Image
    {
        //props
        public int Id { get; set; }
        [Required]
        [Display(Name = "User")]
        public string UserId { get; set; }
        public bool IsProfilePicture { get; set; }
        [Required]
        public string Path { get; set; }
        [MaxLength(60)]
        public string Description { get; set; }

        //Navigatie props
        public User User { get; set; }
        //public User IsProfilePictureOf { get; set; }
        public ICollection<MessageImage> MessageImages { get; set; }
        //Constructor
        public Image()
        {

        }
        //Methods
    }
}
