using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using EShop.Areas.Administration.Models;
using EShop.Domain.Identity;
using EShop.Domain.Interfaces;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace EShop.Areas.Administration.Controllers
{
    /// <summary>
    /// Class for manipulating Users state. Available only for Users in Admin and Moderator Roles
    /// </summary>
    [Authorize(Roles = "Admin, Moderator")]
    public class AdminUsersController : Controller
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private UserManager<AppUser> manager;

        public void AdminUserController()
        {
        }
        
        /// <summary>
        /// Displays list of all users registred in application
        /// </summary>
        /// <returns>View with list of users</returns>
        public ActionResult Index()
        {
            //Arrange 
            List<AdminUsersModel> model = new List<AdminUsersModel>();
            var users = HttpContext.GetOwinContext().GetUserManager<AppUserManager>().Users.ToList();

            AppUserManager userManager = HttpContext.GetOwinContext()
                .GetUserManager<AppUserManager>();          

            foreach (var user in users)
            {
                model.Add(new AdminUsersModel()
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    IsEnabled = user.IsEnabled,
                    PhoneNumber = user.PhoneNumber,
                    Roles = userManager.GetRoles(user.Id)
            });
            }

            return View(model);
        }

        /// <summary>
        /// Toogle method wich enabling/disabling User with specific id
        /// </summary>
        /// <param name="id">Id of User from db</param>
        /// <returns></returns>
        [HttpGet]
        public RedirectToRouteResult Disable(string id)
        {
            manager = HttpContext.GetOwinContext().GetUserManager<AppUserManager>();
            var changeEnable = manager.Users.First(u=>u.Id==id);
            changeEnable.IsEnabled = !changeEnable.IsEnabled;
            manager.Update(changeEnable);
            logger.Info($"User {User.Identity.Name} have changed user's ({changeEnable.UserName}) state");

            return RedirectToAction("Index");
        }
    }

}