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
    [Authorize(Roles = "Admin,Moderator")]
    public class AdminOrderController : Controller
    {
        private IEshopRepository _repository;

        public AdminOrderController(IEshopRepository repo)
        {
            _repository = repo;
        }

        // GET: Administration/AdminOrder
        public ActionResult Index()
        {
            var statuses = _repository.Statuses.ToList();
            var orders = _repository.Orders.ToList();
            var sldkfj = _repository.CartLines.ToList();
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

        [HttpPost]
        public RedirectToRouteResult Index(int orderId, int statusId=1)
        { 
            var status = _repository.Statuses.First(s=>s.Id == statusId);
            var result = _repository.Orders.First(c => c.Id == orderId);
            result.OrderStatus = status;
            _repository.SaveOrder(result);
            return RedirectToAction("Index");
        }

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