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
    public class LoginController : Controller
    {
        // GET: Login/Login
        public ActionResult Index()
        {
            var url = string.Empty;
            if (Request.UrlReferrer != null) url = HttpContext.Request.UrlReferrer.AbsolutePath;
            else url = "~/Goods/List";

            LoginViewModel model = new LoginViewModel(){ ReturnUrl =  url };
            return View(model);
        }

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
                        return Redirect(login.ReturnUrl ?? Url.Action("List", "Goods", new {Areas = ""}, null));
                    }
                    else
                    {
                        ModelState.AddModelError("", "You are blocked for a while, please contact Administrator for details");
                        return View(login);
                    }
                }
            }
            ModelState.AddModelError("", "Invalid username or password");
            return View(login);
        }

        [HttpGet]
        public ActionResult SingOut()
        {
            HttpContext.GetOwinContext().Authentication.SignOut();
            return Redirect("~/Goods/List");
        }
    }
}