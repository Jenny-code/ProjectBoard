using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProjectBoard.Models
{
    public class Project
    {
        public Project()
        {
            IsCompleted = false;
            Tasks = new HashSet<ATask>();
            ApplicationUsers = new HashSet<ApplicationUser>();
            //Notifications = new HashSet<Notification>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime Deadline { get; set; }
        public double Budget { get; set; }
        public bool IsCompleted { get; set; }
        public Priority Priority { get; set; }

        public virtual ICollection<ATask> Tasks { get; set; }
        public virtual ICollection<ApplicationUser> ApplicationUsers { get; set; }
        //public virtual ICollection<Notification> Notifications { get; set; }


    }

}