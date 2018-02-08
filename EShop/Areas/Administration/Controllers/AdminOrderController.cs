using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EShop.Areas.Administration.Models;
using EShop.Domain.Context;
using EShop.Domain.Entities;
using EShop.Domain.Interfaces;

namespace EShop.Areas.Administration.Controllers
{
    /// <summary>
    /// Controller for manipulating Orders. Available only for users with Admin Role.
    /// </summary>
    [Authorize(Roles = "Admin")]
    public class AdminOrderController : Controller
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private IEshopRepository _repository;
        
        /// <summary>
        /// Basic constuctor for current controller. Set connection with database and load Categories from it.
        /// </summary>
        public AdminOrderController(IEshopRepository repo)
        {
            _repository = repo;
        }

        /// <summary>
        /// Displays all Orders from all Users into View
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            // upploading extra data for order from db
            var statuses = _repository.Statuses.ToList();
            var orders = _repository.Orders.ToList();
            var cartLines = _repository.CartLines.ToList();

            List<AdminOrderModel> model = new List<AdminOrderModel>();
            foreach (var order in orders)
            {
                var cart = new Cart(order.OrderCart).ComputeTotalValue();
                model.Add(
                    new AdminOrderModel()
                    {
                        OrderId = order.Id,
                        Date = order.Date,
                        OrderCart = order.OrderCart,
                        Shipment = order.Shipment,
                        TotalCost = cart,
                        User = order.User,
                        OrderStatus = order.OrderStatus.Id,
                    }
                );
            }

            ViewBag.List = GetStatuses();

            return View(model);
        }

        /// <summary>
        /// Changing status of the order to selected
        /// </summary>
        /// <param name="orderId">Id of Order into db</param>
        /// <param name="statusId">Id of Status into db</param>
        /// <returns></returns>
        [HttpPost]
        public RedirectToRouteResult ChangeStatus(int orderId, int statusId=1)
        { 
            var status = _repository.Statuses.First(s=>s.Id == statusId);
            var result = _repository.Orders.First(c => c.Id == orderId);

            result.OrderStatus = status;
            _repository.SaveOrder(result);

            logger.Info($"User {User.Identity.Name} have changed status of order by id: {orderId}");

            return RedirectToAction("Index");
        }

        /// <summary>
        /// Load statuses from db and creates list for dropdown input
        /// </summary>
        /// <returns>List for dropdown items</returns>
        private IEnumerable<SelectListItem> GetStatuses()
        {
            return _repository.Statuses.Select(
                s => new SelectListItem
                {
                    Text = s.Name,
                    Value = s.Id.ToString(),
                    Disabled = false
                }).ToList();
        }
    }
}