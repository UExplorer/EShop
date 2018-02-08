using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EShop.Domain.Entities;
using EShop.Domain.Interfaces;
using EShop.Models;

namespace EShop.Controllers
{
    public class CartController : Controller
    {
        private IEshopRepository _repository;
        public CartController(IEshopRepository repo)
        {
            _repository = repo;
            ViewBag.Categories = _repository.Categories.ToList();
        }

        public ViewResult Index()
        {
            var url = string.Empty;
            if (Request.UrlReferrer != null) url = HttpContext.Request.UrlReferrer.AbsolutePath;
            else url = "~/Goods/List";
            return View(new CartViewModel
            {
                Cart = GetCart(),
                ReturnUrl = url
            });
        }

        public RedirectToRouteResult Add(int id, string returnUrl)
        {
            Goods goods = _repository.Goods
                .FirstOrDefault(g => g.Id == id);

            if (goods != null)
            {
                GetCart().AddItem(goods, 1);
            }
            return RedirectToAction("Index");
        }

        public RedirectToRouteResult RemoveFromCart(int id, string returnUrl)
        {
            Goods goods = _repository.Goods
                .FirstOrDefault(g => g.Id == id);

            if (goods != null)
            {
                GetCart().RemoveLine(goods);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public Cart GetCart()
        {
            Cart cart = (Cart)Session["Cart"];
            if (cart == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;
        }
    }
}