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
        public int UserId { get; set; }
        [Required]
        public int PostId { get; set; }

        //Navigatie props
        public Post Post { get; set; }
        public User User { get; set; }
    }
}
