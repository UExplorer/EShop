using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;
using EShop.Domain.Entities;
using EShop.Domain.Identity;
using EShop.Domain.Interfaces;
using EShop.Models;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;

namespace EShop.Controllers
{
    /// <summary>
    /// Controller for adding new order
    /// </summary>
    public class OrderController : Controller
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private IEshopRepository _goodsRepository;
        private IOrderRepository _orderRepository;

        /// <summary>
        /// Connection with db
        /// </summary>
        /// <param name="repo"></param>
        public OrderController(IEshopRepository goodsRepo, IOrderRepository orderRepo)
        {
            _goodsRepository = goodsRepo;
            _orderRepository = orderRepo;
            ViewBag.Categories = _goodsRepository.GetCategories().ToList();
        }

        /// <summary>
        /// Action wich creates form with Shipping information for User, and gets current Cart from session
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "Admin,Moderator,User")]
        public ActionResult Index()
        {
            // Get Cart
            List<CartLine> orderCart = new List<CartLine>();
            if ((Cart) HttpContext.Session["Cart"] != null)
            {
                orderCart = ((Cart) HttpContext.Session["Cart"]).Lines.ToList();
            }

            //Just stringB for more undestandble Cart format
            StringBuilder list = new StringBuilder();

            // Typing Goods name and it Quantity
            foreach (var item in orderCart)
            {
                list.Append($"{item.Goods.Name} x {item.Quantity}; ");
            }

            // Model for order
            CreateOrderModel model = new CreateOrderModel()
            {
                GoodsList = list.ToString()
            };

            return View(model);
        }

        // Creating new Order and saving it into db
        public ActionResult CreateOrder(CreateOrderModel model)
        {
            // Arrange
            var goods = _goodsRepository.GetGoods().ToList();
            var status = _orderRepository.GetStatuses().ToList();
            var userName = HttpContext.User.Identity.Name;
            var user = HttpContext.GetOwinContext().GetUserManager<AppUserManager>()
                .Users.FirstOrDefault(u => u.UserName == userName);
            var orderCart = ((Cart)HttpContext.Session["Cart"]).Lines.ToList();

            // Valdation
            if (ModelState.IsValid)
            {
                Order order = new Order()
                {
                    OrderCart = orderCart,
                    Date = DateTime.Now,
                    OrderStatus = status[0],
                    User = userName,
                    Shipment = new Shipment()
                    {
                        Address = model.Address,
                        City = model.City,
                        Country = model.Country,
                        ZIP = model.ZIP,
                        Reciver = model.Reciver
                    }
                };

                // Save order into db
                _orderRepository.AddOrder(order);
                // Send e-mail to admin
                SendNotification(user);
                // Clear Cart
                HttpContext.Session["Cart"] = null;
                // Some log info
                logger.Info($"Registred new order from user:{userName}");

                return View();
            }

            // Return view if model is invalid
            return View("Index",model);
        }

        /// <summary>
        /// Arrange smtp server and send email to Admin with current Order information
        /// </summary>
        /// <param name="user"></param>
        private void SendNotification(AppUser user)
        {
            // Initialize
            MailAddress from = new MailAddress("gemlont@gmail.com");
            MailAddress to = new MailAddress("ordinaryswiss@gmail.com");
            MailMessage message = new MailMessage(from, to);
            // Messbody
            message.Subject = "Новый заказ";
            message.Body = $"Поступил заказ от User:{user.UserName} email:{user.Email} Phone number:{user.PhoneNumber}";
            message.IsBodyHtml = false;
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            // my gmail smurf
            smtp.Credentials = new NetworkCredential("gemlont@gmail.com", "v3r8o5e8");
            smtp.EnableSsl = true;
            smtp.Send(message);
            // log info
            logger.Info("Messege about new order successfully send to Admin mail");
        }
    }
}