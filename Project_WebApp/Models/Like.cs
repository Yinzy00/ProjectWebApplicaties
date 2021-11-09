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
        public int Id { get; set; }
        //[Required]
        public string UserId { get; set; }
        [Required]
        public int MessageId { get; set; }

        //Navigatie props
        public Message Message{ get; set; }
        public User User { get; set; }
    }
}
