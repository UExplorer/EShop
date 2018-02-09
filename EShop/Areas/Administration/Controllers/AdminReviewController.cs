using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using EShop.Areas.Administration.Models;
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

            ReplyModel model = new ReplyModel()
            {
                User = user.UserName
            };

            return View(model);
        }

        [HttpPost]
        public void Reply(ReplyModel model)
        {

            SendMessage(model.From, model.User, model.Subject, model.TextBody);
        }

        /// <summary>
        /// Send email to User
        /// </summary>
        /// <returns>Redirect to Index action of this controller</returns>
        public RedirectToRouteResult SendMessage(string From, string toUser, string subject, string body)
        {
            MailAddress from = new MailAddress(From);
            MailAddress to = new MailAddress(toUser);
            MailMessage message = new MailMessage(from, to);

            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = false;
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.Credentials = new NetworkCredential("gemlont@gmail.com", "v3r8o5e8");
            smtp.EnableSsl = true;

            smtp.Send(message);
            ViewBag.UserMess = "Письмо успешно отправленно";
            return RedirectToAction("Index");
        }
    }
}