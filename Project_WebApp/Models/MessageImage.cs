using System;
using System.Collections.Generic;
using System.Text;

namespace Project_WebApp
{
    public class MessageImage
    {
        //Props
        public int MessageImageId { get; set; }
        public int MessageId { get; set; }
        public int ImageId { get; set; }

        //Nav props
        public Image Image { get; set; }
        public Message Message { get; set; }
    }
}
