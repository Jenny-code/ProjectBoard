using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

            Project = db.Projects.First(x => x.Id == projId);
            var query = from x in db.Tasks where x.ProjectId == projId select x;

            Tasks = query.ToList();
        }
    }
}