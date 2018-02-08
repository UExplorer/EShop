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
    public class GoodsComparatorController : Controller
    {
        private IEshopRepository _repository;

        public GoodsComparatorController(IEshopRepository repo)
        {
            _repository = repo;
        }

        [NullIdException]
        public ActionResult Index(int? id)
        {
            CompareModel model = GetCompare();
            var category = _repository.Categories.ToList();
            var goods = _repository.Goods.First(g => g.Id==id);
            var url = string.Empty;
            if (Request.UrlReferrer != null) url = HttpContext.Request.UrlReferrer.AbsolutePath;
            else url = "~/Goods/List";
            model.ReturnUrl = url;

            if (model.Item1 == null)
            {
                model.Item1 = goods;
                Session["Compare"] = model;
            }
            else
            {
                if (model.Item2 == null)
                {
                    model.Item2 = _repository.Goods.FirstOrDefault(g=>g.CategoryId == goods.CategoryId);
                    Session["Compare"] = model;
                }
                else
                {
                    TempData["UserMess"] = "В очереди для сравнения уже есть 2 товара, для начала удалите один из них";
                    return RedirectToAction("Index", "Item", new { id = id, UserMess= "В очереди для " });
                }
            }

            if (model.Item1 != null && model.Item2 != null)
                return View(model);
            else
            {
                TempData["UserMess"] = "Товар успешно добавлен в очередь для сравнения, добавьте еще один товар для сравнения";
                return RedirectToAction("Index","Item", new {id=id});
            }
        }

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