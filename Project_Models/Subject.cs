using System;
using System.Collections.Generic;
using System.Text;

namespace Project_Models
{
    /// <summary>
    /// A subject on 
    /// </summary>
    public class Subject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }

        public List<Post>? Posts { get; set; }
    }
}
