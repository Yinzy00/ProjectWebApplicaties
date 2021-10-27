using Project_WebApp.ViewModels.Subject;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Project_WebApp
{
    /// <summary>
    /// A subject on 
    /// </summary>
    public class Subject
    {
        //props
        [Key]
        public int? Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public DateTime Created { get; set; }
        //Navigation props
        public ICollection<PostSubject> PostSubjects{ get; set; }
        //Extra getters & setters
        public int AmountOfPosts
        {
            get
            {
                return PostSubjects.Count;
            }
        }
        //Constructor
        public Subject()
        {

        }

        public Subject(CreateEditSubjectViewmodel model)
        {
            Name = model.Name;
            Description = model.Description;
            Created = new DateTime();
        }
        //Methods
    }
}
