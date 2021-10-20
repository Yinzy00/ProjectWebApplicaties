using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project_WebApp.Models
{
    public class PostSubject
    {
        //props
        [Key]
        public int PostSubjectId { get; set; }
        [Required]
        public int PostId { get; set; }
        [Required]
        public int SubjectId { get; set; }

        //Nav props
        [Required]
        public Post Post{ get; set; }
        [Required]
        public Subject Subject { get; set; }
    }
}
