using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Project_WebApp
{
    public class Like
    {
        //props
        [Key]
        [Required]
        public int LikeId { get; set; }
        [Required]
        [Display(Name = "User")]
        public int UserId { get; set; }
        [Required]
        [Display(Name = "Post")]
        public int PostId { get; set; }

        //Navigatie props
        [Required]
        public Post Post { get; set; }
        [Required]
        public User User { get; set; }
    }
}
