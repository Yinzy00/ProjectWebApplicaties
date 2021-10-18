using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Models
{
    public class Image
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Path { get; set; }
        public string Description { get; set; }

        public User User { get; set; }
        public Post Post { get; set; }
    }
}
