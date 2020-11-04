using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
namespace ProjectBoard.Models
{
    public enum Notificationtype
    {
        Deadline,
        Urgent,
        Completed
    }
    public class Notification
    {
        public Notification()
        {
            this.Opened = false;
        }
        [Key]
        public int Id { get; set; }
        public string Body { get; set; }
        public bool Opened { get; set; }
        public string ApplicationUserId { get; set; }
        public int? ATaskId { get; set; }
        public int? ProjectId { get; set; }
        public Notificationtype Notificationtype { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual ATask Task { get; set; }
        public virtual Project Project { get; set; }
    }
}