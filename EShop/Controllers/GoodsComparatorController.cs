using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EShop.Domain.Interfaces;
using EShop.Exceptions;
using EShop.Models;

namespace EShop.Controllers
{
    /// <summary>
    /// Controller for comparing Goods
    /// </summary>
    public class GoodsComparatorController : Controller
    {
        private IEshopRepository _repository;

        /// <summary>
        /// Set connection with db
        /// </summary>
        /// <param name="repo"></param>
        public GoodsComparatorController(IEshopRepository repo)
        {
            _repository = repo;
        }

        /// <summary>
        /// Action for adding new item to compare obj
        /// </summary>
        /// <param name="id">Id of Goods from db table</param>
        /// <returns></returns>
        [NullIdException]
        public ActionResult Index(int? id)
        {
            // Arrange
            CompareModel model = GetCompare();
            var category = _repository.GetCategories().ToList();
            var goods = _repository.GetGoods().First(g => g.Id==id);
            var url = string.Empty;

            if (Request.UrlReferrer != null) url = HttpContext.Request.UrlReferrer.AbsolutePath;
            else url = "~/Goods/List";

            // Set url for return
            model.ReturnUrl = url;

            // Adding item to qq
            if (model.Item1 == null)   // first item is empty
            {
                model.Item1 = goods;
                Session["Compare"] = model;
            }
            else
            {
                if (model.Item2 == null) // second item is empty
                {
                    model.Item2 = _repository.GetGoods().FirstOrDefault(g=>g.CategoryId == goods.CategoryId);
                    Session["Compare"] = model;
                }
                else  // qq is full, so we need cant add more items
                {
                    TempData["UserMess"] = "В очереди для сравнения уже есть 2 товара, для начала удалите один из них";
                    return RedirectToAction("Index", "Item", new { id = id, UserMess= "В очереди для " });
                }
            }

            // if we have 2 objects to compare - we compare them 
            if (model.Item1 != null && model.Item2 != null)
                return View(model);
            else // all other ways
            {
                TempData["UserMess"] = "Товар успешно добавлен в очередь для сравнения, добавьте еще один товар для сравнения";
                return RedirectToAction("Index","Item", new {id=id});
            }
        }

        /// <summary>
        /// Clearing item from compare list
        /// </summary>
        /// <param name="position">position of item to clear</param>
        /// <returns>redirect to main page</returns>
        [NullIdException]
        public RedirectToRouteResult DeleteItem(int position)
        {
            CompareModel model = GetCompare();
            switch (position)
            {
                case 1:
                    model.Item1 = null;
                    break;

                case 2:
                    model.Item2 = null;
                    break;
            }

            return RedirectToAction("List", "Goods");
        }

        /// <summary>
        /// Setting and getting Compare obj from session
        /// </summary>
        /// <returns>Compare obj</returns>
        private CompareModel GetCompare()
        {
            CompareModel sess = (CompareModel)Session["Compare"];
            if (sess == null)
            {
                CompareModel compare = new CompareModel();
                Session["Compare"] = compare;
                return compare;
            }
            else
            {
                return sess;
            }
        }

        public ActionResult Compare()
        {
            CompareModel model = GetCompare();
            if (model.Item1 != null && model.Item2 != null)
                return View("Index", model);
            TempData["UserMess"] = "Для сравнения не хватает предметов";
            return RedirectToAction("List", "Goods");
        }
    }
}