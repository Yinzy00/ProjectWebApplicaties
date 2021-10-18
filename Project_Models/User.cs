using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Models
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime Created { get; set; }
        public int ProfilePictureId { get; set; }

        public Role Role { get; set; }
        public Image? ProfilePicture { get; set; }
        public List<Image>? Images { get; set; }
        public List<Post> Posts { get; set; }
        public List<Like> Likes { get; set; }
    }
}
