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
using System.Data.Entity;
using System.Data.Entity.Migrations;
using Microsoft.Ajax.Utilities;
namespace ProjectBoard.Controllers
{
    //comment
    public class HomeController : Controller
    {
        static ApplicationDbContext db = new ApplicationDbContext();
        UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
        RoleManager<IdentityRole> roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
        public ActionResult Welcomepage()
        {
            var currentUserId = User.Identity.GetUserId();
            var currentUser = userManager.FindById(currentUserId);
            //ViewBag.currenUserRoles = userManager.FindById(currentUserId)
            // 我发现User有效，即User.Identity.Name或User.IsInRole("Administrator")。
            return View(currentUser);
        }
        #region Role part
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
            db.Roles.Remove(roleDelete);
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
        [Authorize(Roles = "Admin,Manager")]
        public ActionResult ShowAllRoles()
        {
            //var roles = db.Roles.Select(r => r.Name).ToList();
            ViewBag.users = db.Users.ToList();
            return View(db.Roles);
        }
        #endregion
        #region User Part
        [Authorize(Roles = "Admin,Manager")]
        public ActionResult UsersList()
        {
            var currentUserId = User.Identity.GetUserId();
            var currentUser = userManager.Users.Where(u => u.Id == currentUserId);
            var usersList = userManager.Users.Except(currentUser);
            db.SaveChanges();
            return View(usersList);
        }
        [Authorize(Roles = "Admin,Manager")]
        public ActionResult SalaryToUser(string userId)
        {
            var user = userManager.Users.Where(u => u.Id == userId).ToList()[0];
            return View(user);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Manager")]
        public ActionResult SalaryToUser([Bind(Include = "Id,DailySalary")] ApplicationUser user)
        {
            if (ModelState.IsValid)
            {
                userManager.FindById(user.Id).DailySalary = user.DailySalary;
                db.SaveChanges();
                return RedirectToAction("UsersList");
            }
            return View(user);
        }
        #endregion
        #region UserRole
        [HttpGet]
        [AllowAnonymous]
        [Authorize(Roles = "Admin")]
        public ActionResult UserToRole(string roleId)
        {
            if (string.IsNullOrWhiteSpace(roleId))
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            var role = roleManager.FindById(roleId);
            ViewBag.RoleName = role.Name;
            ViewBag.RoleId = roleId;
            if (role == null)
            {
                return HttpNotFound();
            }
            var memberIDs = role.Users.Select(x => x.UserId).ToArray();
            var members = userManager.Users.Where(x => memberIDs.Any(y => y == x.Id));
            var membersNo = userManager.Users.Except(members);
            return View(membersNo);
        }
        [Authorize(Roles = "Admin")]
        public ActionResult AddToRole(string userId, string roleName, string roleId)
        {
            if (!string.IsNullOrWhiteSpace(userId) && !string.IsNullOrWhiteSpace(roleName))
            {
                userManager.AddToRole(userId, roleName);
                db.SaveChanges();
            }
            return RedirectToAction("ViewRoleUser", "Home", new { roleId = roleId });
        }
        [Authorize(Roles = "Admin,Manager,Developer")]
        public ActionResult ViewRoleUser(string roleId)
        {
            if (string.IsNullOrWhiteSpace(roleId))
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            var role = roleManager.FindById(roleId);
            ViewBag.RoleName = role.Name;
            var memberIDs = role.Users.Select(x => x.UserId).ToArray();
            var members = userManager.Users.Where(x => memberIDs.Any(y => y == x.Id));
            return View(members);
        }
        [Authorize(Roles = "Admin")]
        public ActionResult RemoveRoleUser(string roleName, string userId)
        {
            if (string.IsNullOrWhiteSpace(roleName) && string.IsNullOrWhiteSpace(userId))
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            var result = userManager.RemoveFromRole(userId, roleName);
            var role = roleManager.FindByName(roleName);
            if (!result.Succeeded)
            {
                return View("Error");
            }
            return RedirectToAction("ViewRoleUser", new { roleId = role.Id });
        }
        #endregion
        ////Remove User
        //[Authorize(Roles = "Admin")]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> RemoveUser(string id)
        //{
        //    ApplicationUser user = userManager.FindById(id);
        //    await userManager.DeleteAsync(user);
        //    return RedirectToAction("UsersList", "Home");
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