using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectBoard.Models
{
    public class ProjectTaskVM
    {
        public Project Project { get; set; }
        public List<ATask> Tasks { get; set; }

        public ProjectTaskVM(int projId)
        {
            var db = new ApplicationDbContext();

            Project = db.Projects.First(p => p.Id == projId);

            var listProjectTasks = 
                from x in Tasks where x.ProjectId == projId 
                // && x.IsCompleted == false 
                select x;

            Tasks = listProjectTasks.ToList();
        }
    }
}