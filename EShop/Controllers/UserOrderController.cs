﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EShop.Areas.Administration.Models;
using EShop.Domain.Entities;
using EShop.Domain.Interfaces;
using EShop.Models;

namespace EShop.Controllers
{
    /// <summary>
    /// Controller helps get information about all Orders of current logged User
    /// </summary>
    [Authorize(Roles = "Admin,Moderator,User")]
    public class UserOrderController : Controller
    {
        private IEshopRepository _repository;

        /// <summary>
        /// Connection to db
        /// </summary>
        /// <param name="repo"></param>
        public UserOrderController(IEshopRepository repo)
        {
            _repository = repo;
            ViewBag.Categories = _repository.Categories.ToList();
        }

        /// <summary>
        /// Action wich is displaing all current logged User Orders
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            // Arrange
            var currentUser = User.Identity.Name;
            var orders = _repository.Orders.Where(o => o.User == currentUser).ToList();
            var cartLines = _repository.CartLines.ToList();
            var status = _repository.Statuses.ToList();

            List<OrderViewModel> model = new List<OrderViewModel>();
            foreach (Order order in orders)
            {
                model.Add(new OrderViewModel()
                {
                    Date = order.Date,
                    OrderCart = order.OrderCart,
                    OrderId = order.Id,
                    OrderStatus = order.OrderStatus,
                    Shipment = order.Shipment,
                    User = order.User
                });
            }

            return View(model);
        }
        
        /// <summary>
        /// Delete order by Id
        /// </summary>
        /// <param name="Id">Id of Order from db</param>
        /// <returns></returns>
        [HttpPost]
        public RedirectToRouteResult Delete(int Id)
        {
            _repository.DeleteOrder(Id);
            return RedirectToAction("Index");
        }
    }
}