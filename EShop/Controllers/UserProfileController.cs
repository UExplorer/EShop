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
    /// <summary>
    /// Controller for editin info of current User profile
    /// </summary>
    public class UserProfileController : Controller
    {
        private IEshopRepository _goodsRepository;
        private IOrderRepository _orderRepository;

        /// <summary>
        /// Set connection to db
        /// </summary>
        /// <param name="repo"></param>
        public UserProfileController(IEshopRepository goodsRepo, IOrderRepository orderRepo)
        {
            _goodsRepository = goodsRepo;
            _orderRepository = orderRepo;
            ViewBag.Categories = _goodsRepository.GetCategories().ToList();
        }

        /// <summary>
        /// Display current info of logged User
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            // Arrange
            AppUserManager userManager = HttpContext.GetOwinContext().GetUserManager<AppUserManager>();
            var currUsName = HttpContext.GetOwinContext().Authentication.User.Identity.Name;
            var user = userManager.Users.FirstOrDefault(u =>
                u.UserName == currUsName);
            var shipment = _orderRepository.GetShipments().ToList();
            RegisterModel model = new RegisterModel();

            // We should find our User ;)
            if (user != null)
            {
                model.Name = user.UserName;
                model.Email = user.Email;
                model.PhoneNumber = user.PhoneNumber;
                return View(model);
            }

            else
            {
                return View();
            }
        }
    }
}