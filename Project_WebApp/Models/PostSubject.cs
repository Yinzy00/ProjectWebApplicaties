using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project_WebApp
{
    public class PostSubject
    {
        //props
        [Required]
        public int PostId { get; set; }
        [Required]
        public int SubjectId { get; set; }

        //Nav props
        public Post Post { get; set; }
        public Subject Subject { get; set; }
    }
}
