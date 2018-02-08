using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EShop.Domain.Entities;
using EShop.Domain.Interfaces;
using EShop.Models;

namespace EShop.Controllers
{
    /// <summary>
    /// Main page Controller. Displays all Goods existing in db. Could sort, search, filter and more...
    /// </summary>
    public class GoodsController : Controller
    {
        readonly IEshopRepository _repository;

        /// <summary>
        /// Set connection with db
        /// </summary>
        /// <param name="repo"></param>
        public GoodsController(IEshopRepository repo)
        {
            _repository = repo;
            ViewBag.Categories = GetCategories();
        }

        /// <summary>
        /// Just displays all Goods from db
        /// </summary>
        /// <param name="model"></param>
        /// <returns>View with all Goods</returns>
        public ActionResult List(GoodsModel model)
        {
            GetSort(model);
            ViewBag.Categories = GetCategories();

            model.Goods = Filters(model)
                .OrderBy(g => g.Id)
                .ToList();

            return View("List", model);
        }

        /// <summary>
        /// Toogle Action wich can sort by id by asc and desc
        /// </summary>
        /// <param name="model"></param>
        /// <returns>View with sorted Goods</returns>
        public ActionResult ListById(GoodsModel model)
        {
            model.Sort = (SortModel)Session["Sort"];
            ViewBag.Categories = GetCategories();

            if (model.Sort.RevboolId)
            {
                model.Goods = Filters(model)
                    .OrderBy(g => g.Id)
                    .Reverse().ToList();

                ReverseToModel(model, "Id");
                return View("List", model);
            }
            else
            {
                model.Goods = _repository.Goods
                    .OrderBy(g => g.Id).ToList();

                ReverseToModel(model, "Id");
                return View("List", model);
            }
        }

        /// <summary>
        /// Toogle Action wich can sort by Name by asc and desc
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult ListByName(GoodsModel model)
        {
            GetSort(model);
            ViewBag.Categories = GetCategories();

            if (model.Sort.RevboolName)
            {
                model.Goods = Filters(model)
                    .OrderBy(g => g.Name)
                    .Reverse().ToList();

                ReverseToModel(model, "Name");
                return View("List", model);
            }
            else
            {
                model.Goods = Filters(model)
                    .OrderBy(g => g.Name).ToList();

                ReverseToModel(model, "Name");
                return View("List", model);
            }
        }

        /// <summary>
        /// Toogle Action wich can sort by Price by asc and desc
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult ListByPrice(GoodsModel model)
        {
            GetSort(model);
            ViewBag.Categories = GetCategories();

            if (model.Sort.RevboolPrice)
            {
                model.Goods = Filters(model)
                    .OrderBy(g => g.Price)
                    .Reverse().ToList();

                ReverseToModel(model, "Price");
                return View("List", model);
            }
            else
            {
                model.Goods = Filters(model)
                    .OrderBy(g => g.Price).ToList();

                ReverseToModel(model, "Price");
                return View("List", model);
            }
        }

        /// <summary>
        /// Action for Search Goods by name
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult Search(GoodsModel model)
        {
            GetSort(model);

            ViewBag.Categories = GetCategories();
            var list = _repository.Goods.Where(g => g.Name.ToUpper().Contains(model.Name.ToUpper())).ToList();
            model.Goods = list;

            return View("List", model);
        }
        
        /// <summary>
        /// This action filters Goods by set parameters
        /// </summary>
        /// <param name="model">Model with all parametrs wich could be filtered</param>
        /// <returns></returns>
        public List<Goods> Filters(GoodsModel model)
        {
            List<Goods> currFilter = _repository.Goods.Where(i => (model.Color == null || i.Color.ToUpper().Contains(model.Color.ToUpper()))
                                                                   && (i.Category.Id == model.Category || model.Category == null)
                                                                   && (i.Height == model.Height || model.Height == null)
                                                                   && (i.Width == model.Width || model.Width ==null)
                                                                   && (i.Length == model.Length || model.Length ==null)
                                                                   && (i.Price > model.PriceLow || model.PriceLow == null)
                                                                   && (i.Price < model.PriceHight || model.PriceHight == null))
                .ToList();

            return currFilter;
        }

        /// <summary>
        /// Method for Toogle current sort options
        /// </summary>
        /// <param name="model"></param>
        /// <param name="type"></param>
        public void ReverseToModel(GoodsModel model, string type)
        {
            switch (type)
            {
                case "Name":
                {
                    if (!model.Sort.RevboolName)
                    {
                        model.Sort.RevName = "Я-А";
                        model.Sort.RevboolName = true;
                    }
                    else
                    {
                        model.Sort.RevName = "A-Я";
                        model.Sort.RevboolName = false;
                    }

                        break;
                }

                case "Id":
                {
                    if (!model.Sort.RevboolId)
                    {
                        model.Sort.RevId = "сначала новые";
                        model.Sort.RevboolId = true;
                    }
                    else
                    {
                        model.Sort.RevId = "сначала старые";
                        model.Sort.RevboolId = false;
                    }

                        break;
                }

                case "Price":
                {
                    if (!model.Sort.RevboolPrice)
                    {
                        model.Sort.RevPrice = "сначала дорогие";
                        model.Sort.RevboolPrice = true;
                    }
                    else
                    {
                        model.Sort.RevPrice = "сначала дешевые";
                        model.Sort.RevboolPrice = false;
                    }

                    break;
                }

                default: break;
            }
        }

        /// <summary>
        /// Load Categories from db
        /// </summary>
        /// <returns></returns>
        private IEnumerable<SelectListItem> GetCategories()
        {
            return _repository.Categories.Select(
                s => new SelectListItem
                {
                    Text = s.Name,
                    Value = s.Id.ToString(),
                    Disabled = false
                }).ToList();
        }

        /// <summary>
        /// Set or get current Sort parametrs from session
        /// </summary>
        /// <param name="model"></param>
        private void GetSort(GoodsModel model)
        {
            SortModel sess = (SortModel)Session["Sort"];
            if (sess == null)
            {
                SortModel sort = new SortModel()
                {
                    RevName = "A-Я",
                    RevboolName = false,
                    RevId = "сначала новые",
                    RevboolId = true,
                    RevPrice = "сначала дорогие",
                    RevboolPrice = true
                };
                Session["Sort"] = sort;
                model.Sort = sort;
            }
            else
            {
                model.Sort = sess;
            }
        }
    }
}