using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_WebApp
{
    public class Image
    {
        //props
        [Key]
        public int ImageId { get; set; }
        [Required]
        [Display(Name = "User")]
        public int UserId { get; set; }
        [Required]
        public string Path { get; set; }
        [MaxLength(60)]
        public string? Description { get; set; }

        //Navigatie props
        [Required]
        public User User { get; set; }
        [Required]
        public Post Post { get; set; }
        //Constructor
        public Image()
        {

        }
        //Methods
    }
}
