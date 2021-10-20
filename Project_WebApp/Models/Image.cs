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
        [Required]
        public int ImageId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public string Path { get; set; }
        [MaxLength(60)]
        public string? Description { get; set; }

        //Navigatie props
        public User User { get; set; }
        public Post Post { get; set; }
        //Constructor
        public Image()
        {

        }
        //Methods
    }
}
