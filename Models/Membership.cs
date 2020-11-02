using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectBoard.Models
{
    public class Membership
    {

        //add, delete and update users and roles, assign roles to users.

        static ApplicationDbContext db = new ApplicationDbContext();

        static UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

        static RoleManager<IdentityRole> roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));

        public static bool AddNewRole(string roleName)
        {
            if (!roleManager.RoleExists(roleName))
            {
                roleManager.Create(new IdentityRole { Name = roleName });
                return true;
            }
            else
            {
                return false;
                //false means the operation failed
            }
        }

        //Remove a role 
        public static IdentityResult RemoveRole(string roleName)
        {
            return roleManager.Delete(new IdentityRole { Name = roleName });
        }

        public static IdentityResult UpdateRole(string roleName)
        {
            return roleManager.Update(new IdentityRole { Name = roleName });
        }

        public static IdentityResult AddUser(string userName, string password)
        {
            //return roleManager.Update(new IdentityRole { Name = roleName });
            return userManager.Create(new ApplicationUser { });
        }










        //Add a user to a role  { check if the user is not int this role already }
        public static IdentityResult AddUserToRole(string userId, string roleName)
        {
            return userManager.AddToRole(userId, roleName);
        }











        // Check if user is in role        
        public static bool CheckUserInRole(string userId, string roleName)
        {
            //return userManager.IsInRole(userId, roleName);
            return userManager.IsInRole(userId, roleName);

        }

        //Show a list of all roles
        public static List<string> GetAllRoles()
        {
            //roleManager.Roles.
            return db.Roles.Select(r => r.Name).ToList();
        }

        //Show a list of users
        public static List<string> GetAllUserNames()
        {
            return db.Users.Select(u => u.UserName).ToList();
        }

        // Add a role to the system

        public static List<string> GetAllRolesForUser(string userId)
        {
            return userManager.GetRoles(userId).ToList();
        }




        //show all roles for a user
        public static List<string> ShowAllRolesForAUser(string userId)
        {
            //var user=db.Users.Select(u => u.Id == userId);
            return userManager.GetRoles(userId).ToList();

            // return db.Roles.Select(r=>r.Users.Where(u=>u.UserId==userId)).ToList();

        }

        //remove a user from a role (first check if the user is in this role)

        //show a list of all roles





    }
}