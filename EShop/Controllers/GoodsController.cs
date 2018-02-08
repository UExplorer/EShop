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
    public class GoodsController : Controller
    {
        readonly IEshopRepository _repository;

        public GoodsController(IEshopRepository repo)
        {
            _repository = repo;
            ViewBag.Categories = GetCategories();
        }

        public ActionResult List(GoodsModel model)
        {
            GetSort(model);
            ViewBag.Categories = GetCategories();

            model.Goods = Filters(model)
                .OrderBy(g => g.Id)
                .ToList();

            return View("List", model);
        }

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

        public ActionResult Search(GoodsModel model)
        {
            GetSort(model);

            ViewBag.Categories = GetCategories();
            var list = _repository.Goods.Where(g => g.Name.ToUpper().Contains(model.Name.ToUpper())).ToList();
            model.Goods = list;

            return View("List", model);
        }

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