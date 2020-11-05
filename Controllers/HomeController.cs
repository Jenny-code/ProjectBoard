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


        [Authorize(Roles = "Admin")]
        public ActionResult ShowAllRoles()
        {
            //var roles = db.Roles.Select(r => r.Name).ToList();
            ViewBag.users = db.Users.ToList();
            return View(db.Roles);
        }


        //RemoveUser
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]

        [HttpPost]
        public async Task<ActionResult> RemoveUser(string id)
        {
            ApplicationUser user = userManager.FindById(id);
            await userManager.DeleteAsync(user);
            return RedirectToAction("UsersList", "Home");
        }


        [Authorize(Roles = "Admin")]
        public ActionResult ShowAllUsers()
        {
            var users = db.Users.Select(u => u.UserName);
            ViewBag.users = users;

            return View(users);
        }

        [Authorize]
        public ActionResult UsersList()
        {
            var currentUserId = User.Identity.GetUserId();
            var currentUser = userManager.Users.Where(u => u.Id == currentUserId);
            var usersList = userManager.Users.Except(currentUser);
            db.SaveChanges();
            return View(usersList);
        }




        public ActionResult SalaryToUser(string userId)
        {
            var user = userManager.Users.Where(u => u.Id == userId).ToList()[0];

            return View(user);
        }
        [HttpPost]
        public ActionResult SalaryToUser(int dailySalary, string Id)
        {
            var currentUserId = User.Identity.GetUserId();
            var currentUser = userManager.Users.Where(u => u.Id == currentUserId);
            var usersList = userManager.Users.Except(currentUser);

            db.SaveChanges();

            return View();
        }






        [AllowAnonymous]
        //[Authorize(Roles = "Super")]
        //[ValidateAntiForgeryToken]


        [HttpGet]
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
        [HttpGet]
        public ActionResult AddToRole(string userId, string roleName, string roleId)
        {
            if (!string.IsNullOrWhiteSpace(userId) && !string.IsNullOrWhiteSpace(roleName))
            {
                userManager.AddToRole(userId, roleName);
            }
            return RedirectToAction("ShowAllRoles/");
        }

        [Authorize(Roles = "Admin")]
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

        [HttpGet]
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
            return RedirectToAction("ViewRoleUser", new { id = role.Id });
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult Install()
        {

            ///不读取任务参数
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

