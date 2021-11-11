using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Project_WebApp
{
    public class MessageImage
    {
        [Required]
        public int MessageId { get; set; }
        [Required]
        public int ImageId { get; set; }

        //Nav props
        public Image Image { get; set; }
        public Message Message { get; set; }
    }
}
