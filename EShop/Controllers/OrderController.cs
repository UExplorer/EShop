﻿using System;
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
    public class OrderController : Controller
    {
        private IEshopRepository _repository;

        public OrderController(IEshopRepository repo)
        {
            _repository = repo;
            ViewBag.Categories = _repository.Categories.ToList();
        }

        // GET: Order
        [Authorize(Roles = "Admin,Moderator,User")]
        public ActionResult Index()
        {
            List<CartLine> orderCart = new List<CartLine>();
            if ((Cart) HttpContext.Session["Cart"] != null)
            {
                orderCart = ((Cart) HttpContext.Session["Cart"]).Lines.ToList();
            }

            StringBuilder list = new StringBuilder();

            foreach (var item in orderCart)
            {
                list.Append($"{item.Goods.Name} x {item.Quantity}; ");
            }

            CreateOrderModel model = new CreateOrderModel()
            {
                GoodsList = list.ToString()
            };

            return View(model);
        }

        public ActionResult CreateOrder(CreateOrderModel model)
        {
            var goods = _repository.Goods.ToList();
            var status = _repository.Statuses.ToList();
            var userName = HttpContext.User.Identity.Name;
            var user = HttpContext.GetOwinContext().GetUserManager<AppUserManager>()
                .Users.FirstOrDefault(u => u.UserName == userName);
            var orderCart = ((Cart)HttpContext.Session["Cart"]).Lines.ToList();

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
                _repository.SaveOrder(order);
                SendNotification(user);
                HttpContext.Session["Cart"] = null;
                return View();
            }

            return View("Index",model);
        }

        private void SendNotification(AppUser user)
        {
            MailAddress from = new MailAddress("gemlont@gmail.com");
            MailAddress to = new MailAddress("ordinaryswiss@gmail.com");
            MailMessage message = new MailMessage(from, to);
            message.Subject = "Новый заказ";
            message.Body = $"Поступил заказ от User:{user.UserName} email:{user.Email} Phone number:{user.PhoneNumber}";
            message.IsBodyHtml = false;
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.Credentials = new NetworkCredential("gemlont@gmail.com", "v3r8o5e8");
            smtp.EnableSsl = true;
            smtp.Send(message);
        }
    }
}