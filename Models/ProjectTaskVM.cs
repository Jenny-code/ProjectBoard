using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectBoard.Models
{
    public class ProjectTaskVM
    {
        // project side info
        public string ProjectName { get; set; }
        public DateTime ProjectStart { get; set; }
        public DateTime ProjectEnd { get; set; }
        public Priority ProjectPriority { get; set; }

        // task side info
        public string TaskName { get; set; }
        public DateTime TaskStart { get; set; }
        public DateTime TaskEnd { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
        [Range(0, 100)]
        public int CompletionPerc { get; set; }
        public Priority TaskPriority { get; set; }
    }
}