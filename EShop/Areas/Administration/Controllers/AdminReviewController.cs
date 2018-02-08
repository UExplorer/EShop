using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EShop.Domain.Identity;
using EShop.Domain.Interfaces;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace EShop.Areas.Administration.Controllers
{
    [Authorize(Roles = "Admin,Moderator")]
    public class AdminReviewController : Controller
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private IEshopRepository _repository;

        public AdminReviewController(IEshopRepository repo)
        {
            _repository = repo;
        }

        public ActionResult Index()
        {
            var reviews = _repository.Reviews.ToList();

            return View(reviews);
        }

        public ActionResult DeleteReview(int id)
        {
            _repository.DeleteReview(id);
            logger.Info($"User {User.Identity.Name} have deleted Review");

            return View("Index",_repository.Reviews.ToList());
        }

        public ActionResult Reply(string userName)
        {
            var manager = HttpContext.GetOwinContext().GetUserManager<AppUserManager>();
            var user = manager.FindByName(userName);

            return View(user);
        }

        public RedirectToRouteResult SendMessage()
        {
            return RedirectToAction("Index");
        }
    }
}