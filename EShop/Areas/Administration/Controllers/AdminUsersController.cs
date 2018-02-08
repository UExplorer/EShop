﻿using System;
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
    [Authorize(Roles = "Admin,Moderator")]
    public class AdminUsersController : Controller
    {
        private UserManager<AppUser> manager;

        public void AdminUserController()
        {
        }
        // GET: Administration/AdminUsers
        public ActionResult Index()
        {
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

        [HttpGet]
        public ActionResult Disable(string id)
        {
            manager = HttpContext.GetOwinContext().GetUserManager<AppUserManager>();
            var disable = manager.Users.First(u=>u.Id==id);
            disable.IsEnabled = !disable.IsEnabled;
            manager.Update(disable);
            return View("Index",manager.Users.ToList());
        }
    }

}