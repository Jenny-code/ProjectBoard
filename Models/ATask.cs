using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProjectBoard.Models
{
    public enum Priority
    {
        low,
        medium,
        high
    }
    public class ATask
    {
        public ATask()
        {
            IsCompleted = false;
            //Notifications = new HashSet<Notification>();
            ApplicationUsers = new HashSet<ApplicationUser>();
            CompletionPerc = 0;
        }
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int ProjectId { get; set; }
        //public string Comment { get; set; }
        public string Body { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime Deadline { get; set; }
        public bool IsCompleted { get; set; }
        [Range(0, 100)]
        public int CompletionPerc { get; set; }
        public Priority Priority { get; set; }
        public virtual Project Project { get; set; }
        public virtual ICollection<ApplicationUser> ApplicationUsers { get; set; }
        
        //public virtual ICollection<Comment> Comments { get; set; }
        //public virtual ICollection<Notification> Notifications { get; set; }

        //public string GetComment(bool IsCompleted)
        //{
        //    if (!IsCompleted)
        //    {
        //        string Comment = "";
        //        return Comment;
        //    }

        //    else
        //    {
        //        return Comment;
        //    }
        //}
    }

}