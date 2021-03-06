﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EShop.Areas.Login.Models;
using EShop.Domain.Entities;
using EShop.Domain.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace EShop.Areas.Login.Controllers
{
    /// <summary>
    /// Controller for Registration new Users in app
    /// </summary>
    public class RegisterController : Controller
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Action wich return registration form
        /// </summary>
        /// <returns>View with inputs for register</returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Creating new User if for validated
        /// </summary>
        /// <param name="model">List of inputs. Type RegisterModel</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CreateUser(RegisterModel model)
        {
            // Creating new instance of AppUser
            AppUser newUser = new AppUser();

            if (ModelState.IsValid)
            {
                newUser.UserName = model.Name;
                newUser.Email = model.Email;
                newUser.IsEnabled = true;
                newUser.PhoneNumber = model.PhoneNumber;
                ViewBag.ValidationResult = true;
            }
            else
            {
                TempData["ValidationResult"] = false;
                return View("Index",model);
            }

            // Get Usermanager and AuthManager
            var userManager = HttpContext.GetOwinContext().GetUserManager<AppUserManager>();
            var authManager = HttpContext.GetOwinContext().Authentication;

            // Trying to create User With current data
            var res = userManager.Create(newUser, model.Password);
            if (res.Succeeded)
            {
                var ident = userManager.CreateIdentity(newUser,
                    DefaultAuthenticationTypes.ApplicationCookie);
                userManager.AddToRole(newUser.Id, "User");
                authManager.SignIn(
                    new AuthenticationProperties { IsPersistent = false }, ident);
                logger.Info($"New user ({newUser.UserName}) is reggistred");
            }
            else
            {
                return RedirectToAction("Index","Register", model);
            }

            return RedirectToAction("List","Goods",new{Area = string.Empty});
        }
    }
}