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
    /// <summary>
    /// Controller for modarating Reviews. Available for Users in Admin and Moderator Roles.
    /// </summary>
    [Authorize(Roles = "Admin,Moderator")]
    public class AdminReviewController : Controller
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private IEshopRepository _repository;

        /// <summary>
        /// Basic constructor. Set connection with database.
        /// </summary>
        /// <param name="repo"></param>
        public AdminReviewController(IEshopRepository repo)
        {
            _repository = repo;
        }

        /// <summary>
        /// Returns View with all Reviews from all Users. 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var reviews = _repository.Reviews.ToList();

            return View(reviews);
        }

        /// <summary>
        /// Deleting Review with specific id
        /// </summary>
        /// <param name="id">Id of Review in db</param>
        /// <returns></returns>
        public ActionResult DeleteReview(int id)
        {
            _repository.DeleteReview(id);
            logger.Info($"User {User.Identity.Name} have deleted Review");

            return View("Index",_repository.Reviews.ToList());
        }

        /// <summary>
        /// Repling to the User by mail
        /// </summary>
        /// <param name="userName">Name of User to whome reply</param>
        /// <returns>View with inputs for reply</returns>
        public ActionResult Reply(string userName)
        {
            var manager = HttpContext.GetOwinContext().GetUserManager<AppUserManager>();
            var user = manager.FindByName(userName);

            return View(user);
        }

        /// <summary>
        /// Send email to User
        /// </summary>
        /// <returns>Redirect to Index action of this controller</returns>
        public RedirectToRouteResult SendMessage()
        {
            return RedirectToAction("Index");
        }
    }
}