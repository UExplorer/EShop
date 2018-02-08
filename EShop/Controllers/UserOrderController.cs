using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EShop.Domain.Interfaces;

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
            return View(orders);
        }

        [HttpPost]
        public RedirectToRouteResult Delete(int Id)
        {
            _repository.DeleteOrder(Id);
            return RedirectToAction("Index");
        }
    }
}