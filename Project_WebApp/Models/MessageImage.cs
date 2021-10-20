using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Project_WebApp
{
    public class MessageImage
    {
        //Props
        [Key]
        public int MessageImageId { get; set; }
        [Required]
        [Display(Name = "Message")]
        public int MessageId { get; set; }
        [Required]
        [Display(Name = "Image")]
        public int ImageId { get; set; }

        //Nav props
        [Required]
        public Image Image { get; set; }
        [Required]
        public Message Message { get; set; }
    }
}
