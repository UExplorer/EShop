using System;
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
    [Authorize(Roles = "Admin,Moderator,User")]
    public class UserOrderController : Controller
    {
        private IEshopRepository _repository;

        public UserOrderController(IEshopRepository repo)
        {
            _repository = repo;
            ViewBag.Categories = _repository.Categories.ToList();
        }

        public ActionResult Index()
        {
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

        [HttpPost]
        public RedirectToRouteResult Delete(int Id)
        {
            _repository.DeleteOrder(Id);
            return RedirectToAction("Index");
        }
    }
}