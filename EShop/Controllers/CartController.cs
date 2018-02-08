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
        /// <summary>
        /// Controller for creating and manipulating with Cart object and it's data
        /// </summary>
        private IEshopRepository _repository;

        /// <summary>
        /// Set connection with db
        /// </summary>
        /// <param name="repo"></param>
        public CartController(IEshopRepository repo)
        {
            _repository = repo;
            ViewBag.Categories = _repository.Categories.ToList();
        }

        /// <summary>
        /// Action wich displays all Cart contained items
        /// </summary>
        /// <returns>View with current Cart content</returns>
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

        /// <summary>
        /// Adding new items to Cart or updating existed
        /// </summary>
        /// <param name="id">Goods Id in db</param>
        /// <param name="returnUrl">Url of previous page</param>
        /// <returns></returns>
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

        /// <summary>
        /// Removing from cart existing Goods items
        /// </summary>
        /// <param name="id"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Get Cart object from current session or creating new
        /// </summary>
        /// <returns>Cart item</returns>
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