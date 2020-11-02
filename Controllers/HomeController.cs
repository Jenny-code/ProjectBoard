using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ProjectBoard.Models;


namespace ProjectBoard.Controllers
{
    public class HomeController : Controller
    {
        static ApplicationDbContext db = new ApplicationDbContext();

        UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

        RoleManager<IdentityRole> roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));

        public bool CreateUser(ApplicationUser user, string password)
        {
            var um = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(new ApplicationDbContext()));
            var idResult = um.Create(user, password);
            return idResult.Succeeded;
        }

        [Authorize(Roles = "Admin")]
        public ActionResult CreateRole()
        {
            return View();
        }

        // POST: /Roles/Create
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult CreateRole(FormCollection collection)
        {
            try
            {
                db.Roles.Add(new Microsoft.AspNet.Identity.EntityFramework.IdentityRole()
                {
                    Name = collection["RoleName"]
                });
                db.SaveChanges();
                ViewBag.ResultMessage = "Role created successfully !";
                return RedirectToAction("ShowAllRoles");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult DeleteRole(FormCollection collection)
        {
            try
            {
                db.Roles.Remove(new Microsoft.AspNet.Identity.EntityFramework.IdentityRole()
                {
                    Name = collection["RoleName"]
                });
                db.SaveChanges();
                ViewBag.ResultMessage = "Role created successfully !";
                return RedirectToAction("ShowAllRoles");
            }
            catch
            {
                return View();
            }
        }





        [Authorize(Roles = "Admin")]
        public ActionResult ShowAllRoles()
        {
            //var roles = db.Roles.Select(r => r.Name).ToList();
            return View(db.Roles);
        }
        [Authorize(Roles = "Admin")]
        public ActionResult ShowAllUsers()
        {
            var users = db.Users.Select(u => u.UserName);

            ViewBag.users = users;
            return View(users);
        }


        public ActionResult AddUserToRole(string userId, string roleName)
        {
            ViewData["userId"] = userId;
            ViewData["roleName"] = roleName;
            userManager.AddToRole(userId, roleName);

            db.SaveChanges();
            return View();
        }


        [Authorize(Roles = "Admin")]
        public ActionResult RoleAddToUser()
        {
            SelectList list = new SelectList(Roles.GetAllRoles());
            ViewBag.Roles = list;

            return View();
        }

        /// <summary>
        /// Add role to the user
        /// </summary>
        /// <param name="RoleName"></param>
        /// <param name="UserName"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RoleAddToUser(string RoleName, string UserName)
        {

            if (Roles.IsUserInRole(UserName, RoleName))
            {
                ViewBag.ResultMessage = "This user already has the role specified !";
            }
            else
            {
                Roles.AddUserToRole(UserName, RoleName);
                ViewBag.ResultMessage = "Username added to the role succesfully !";
            }

            SelectList list = new SelectList(Roles.GetAllRoles());
            ViewBag.Roles = list;
            return View();
        }




        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}

