using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EShop.Areas.Login.Models;
using EShop.Domain.Identity;
using EShop.Domain.Interfaces;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace EShop.Controllers
{
    public class UserProfileController : Controller
    {
        private IEshopRepository _repository;
        public UserProfileController(IEshopRepository repo)
        {
            _repository = repo;
            ViewBag.Categories = _repository.Categories.ToList();
        }

        public ActionResult Index()
        {
            AppUserManager userManager = HttpContext.GetOwinContext().GetUserManager<AppUserManager>();
            var currUsName = HttpContext.GetOwinContext().Authentication.User.Identity.Name;
            var user = userManager.Users.FirstOrDefault(u =>
                u.UserName == currUsName);
            var shipment = _repository.Shipments.ToList();
            RegisterModel model = new RegisterModel();

            if (user != null)
            {
                model.Name = user.UserName;
                return View(model);
            }

            else
            {
                return View();
            }
        }
    }
}