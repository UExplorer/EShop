using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EShop.Areas.Login.Models;
using EShop.Domain.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace EShop.Areas.Login.Controllers
{
    /// <summary>
    /// Controller responsible for authorizing and authenticating Users 
    /// </summary>
    public class LoginController : Controller
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Index Action of Controller
        /// </summary>
        /// <returns>View for Login into the system</returns>
        public ActionResult Index()
        {
            var url = string.Empty;
            if (Request.UrlReferrer != null) url = HttpContext.Request.UrlReferrer.AbsolutePath;
            else url = "~/Goods/List";

            LoginViewModel model = new LoginViewModel(){ ReturnUrl =  url };
            return View(model);
        }

        /// <summary>
        /// Authentication of User 
        /// </summary>
        /// <param name="login"></param>
        /// <returns>redirect to main page or redirect to last url</returns>
        [HttpPost]
        public ActionResult Login(LoginViewModel login)
        {
            if (ModelState.IsValid)
            {
                var userManager = HttpContext.GetOwinContext().GetUserManager<AppUserManager>();
                var authManager = HttpContext.GetOwinContext().Authentication;

                AppUser user = userManager.Find(login.UserName, login.Password);
                if (user != null)
                {
                    if (user.IsEnabled)
                    {
                        var ident = userManager.CreateIdentity(user,
                            DefaultAuthenticationTypes.ApplicationCookie);
                        authManager.SignIn(
                            new AuthenticationProperties {IsPersistent = false}, ident);
                        logger.Info($"User: {user.UserName} is autorised");
                        return Redirect(login.ReturnUrl ?? Url.Action("List", "Goods", new {Areas = ""}, null));
                    }
                    else
                    {
                        TempData["LoginError"] = "You are blocked for a while, please contact Administrator for details";
                        return View(login);
                    }
                }
            }
            TempData["LoginError"]="Invalid username or password";
            return View("Index",login);
        }

        /// <summary>
        /// SingOut of current user
        /// </summary>
        /// <returns>redirect to main page</returns>
        [HttpGet]
        public ActionResult SingOut()
        {
            if(User!=null) logger.Info($"User {User.Identity.Name} is SingOut");
            HttpContext.GetOwinContext().Authentication.SignOut();
            return Redirect("~/Goods/List");
        }
    }
}