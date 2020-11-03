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
using System.Data.SqlClient;
using System.Net;

namespace ProjectBoard.Controllers
{

    //comment
    public class HomeController : Controller
    {

        static ApplicationDbContext db = new ApplicationDbContext();

        UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

        RoleManager<IdentityRole> roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));

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


        [Authorize(Roles = "Admin")]
        public ActionResult DeleteRoles(string roleId)
        {
            //var roles = db.Roles.Select(r => r.Name).ToList();
            if (roleId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var roleDelete = db.Roles.Find(roleId);
            if (roleDelete == null)
            {
                return HttpNotFound();
            }
            return View(roleDelete);
        }

        [HttpPost, ActionName("DeleteRoles")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteRolesConfirmed(string roleId)
        {
            var roleDelete = db.Roles.Find(roleId);
            db.Roles.Remove(roleDelete);
            ViewBag.users = db.Users.ToList();
            db.SaveChanges();
            return RedirectToAction("ShowAllRoles");
        }


        [Authorize(Roles = "Admin")]
        public ActionResult ShowAllRoles()
        {
            //var roles = db.Roles.Select(r => r.Name).ToList();
            ViewBag.users = db.Users.ToList();
            return View(db.Roles);
        }
        [Authorize(Roles = "Admin")]
        public ActionResult ShowAllUsers()
        {
            var users = db.Users.Select(u => u.UserName);
            ViewBag.users = users;
            return View(users);
        }


        [Authorize(Roles = "Admin")]    
        public ActionResult AddUserSalary()
        {
            return View();
        }






        [Authorize(Roles = "Admin")]
        public ActionResult AddUserToRole(string roleId)
        {
            if (roleId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var targetrole = db.Roles.Find(roleId);
            if (targetrole == null)
            {
                return HttpNotFound();
            }
            //var users = Roles.GetUsersInRole("Admin");
            //SelectList list = new SelectList(users);
            //ViewBag.Users = list;
            var torole = db.Roles.Find(roleId);
            var users = db.Users.Select(u => u.Id);
            SelectList list = new SelectList(users);
            ViewBag.users = users;
            return View(targetrole);
        }

        //[HttpPost, ActionName("DeleteRoles")]
        //[ValidateAntiForgeryToken]
        //[Authorize(Roles = "Admin")]
        //public ActionResult AddUserToRoleConfirmed(string userId, string roleName)
        //{
        //    userManager.AddToRole(userId, roleName);
        //    db.SaveChanges();

        //    return RedirectToAction("ShowAllRoles");
        //}

        [Authorize(Roles = "Admin")]
        public ActionResult AddUserToRoleSecond()
        {

            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("AddUserToRole")]
        [ValidateAntiForgeryToken]
        public ActionResult AddUserToRoleSecond(string userId, string roleName)
        {
            var users = db.Users.Select(u => u.Id);
            SelectList list = new SelectList(users);
            ViewBag.users = users;
            //var userss = db.Users.Where(u=>u.na);



            //var users = Roles.GetUsersInRole("RoleName").Select(System.Web.Security.Membership.GetUser).ToList();
            //var list = users.Select(x => new SelectListItem { Text = x.UserName, Value = System.Web.Security.Membership.GetUser(x.UserName).ProviderUserKey.ToString() }).ToList();
            //ViewBag.Users = list;



            userManager.AddToRole(userId, roleName);
            db.SaveChanges();
            return View();
        }






        //public ActionResult CreateUser()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public ActionResult CreateUser(string userName = "AAA", string password = "1234")
        //{
        //    var um = new UserManager<ApplicationUser>(
        //        new UserStore<ApplicationUser>(new ApplicationDbContext()));
        //    ApplicationUser user = new ApplicationUser();
        //    user.PasswordHash = um.PasswordHasher.HashPassword(password);
        //    user.UserName = userName;
        //    user.AccessFailedCount = 0;
        //    //db.ApplicationUsers.Add(user);
        //    //db.Users.Add(user);
        //    //var idResult = um.Create(user);
        //    db.SaveChanges();
        //    return View();
        //}



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

