using ProjectBoard.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectBoard.Controllers
{
    public class ProjectTaskController : Controller
    {
        public ApplicationDbContext db = new ApplicationDbContext();

        // GET: ProjectTask
        public ActionResult Index()
        {
            List<ProjectTaskVM> projectTaskList = new List<ProjectTaskVM>();
            return View(projectTaskList);
        }
    }
}