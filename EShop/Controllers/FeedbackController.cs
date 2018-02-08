using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using EShop.Models;

namespace EShop.Controllers
{
    /// <summary>
    /// Controller of Feedback. Implements comunication of any user with administration by sending email
    /// </summary>
    public class FeedbackController : Controller
    {
        /// <summary>
        /// Form with inputs for email
        /// </summary>
        /// <returns>View with specified inputs</returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Method wich configurates and send email to admin post
        /// </summary>
        /// <param name="model"></param>
        /// <returns>View with result of send message</returns>
        public ActionResult SendMail(FeedbackModel model)
        {
            if (ModelState.IsValid)
            {
                //Configurating smtp server
                MailAddress from = new MailAddress("gemlont@gmail.com");
                MailAddress to = new MailAddress("ordinaryswiss@gmail.com");
                MailMessage message = new MailMessage(from,to);

                message.Subject = model.Subject;
                message.Body = $"(From: {model.From}) User: {User.Identity.Name} \nMessage: {model.TextBody}";
                message.IsBodyHtml = false;
                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                smtp.Credentials = new NetworkCredential("gemlont@gmail.com", "v3r8o5e8");
                smtp.EnableSsl = true;

                smtp.Send(message);
                ViewBag.UserMess = "Письмо успешно отправленно";

                return View();
            }
            else
            {
                return View("Index",model);
            }
            
        }
    }
}