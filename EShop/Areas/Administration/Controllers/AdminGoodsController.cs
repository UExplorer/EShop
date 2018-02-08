using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EShop.Areas.Administration.Models;
using EShop.Domain.Entities;
using EShop.Domain.Interfaces;

namespace EShop.Areas.Administration.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminGoodsController : Controller
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        IEshopRepository _repository;

        public AdminGoodsController(IEshopRepository repo)
        {
            _repository = repo;
            ViewBag.Categories = _repository.Categories.ToList();
        }

        public ActionResult Index()
        {
            return View(_repository.Goods.ToList());
        }

        public ViewResult Edit(int id)
        {
            Goods goods = _repository.Goods
                .FirstOrDefault(g => g.Id == id);
            AdminGoodsModel model = new AdminGoodsModel();
            if (goods != null)
            {
                model.Id = goods.Id;
                model.AvailableCount = goods.AvailableCount;
                model.Name = goods.Name;
                model.CategoryId = goods.CategoryId;
                model.Color = goods.Color;
                model.Description = goods.Description;
                model.Height = goods.Height;
                model.Length = goods.Length;
                model.Width = goods.Width;
                model.PictrureName = goods.Pictrure;
                model.Price = goods.Price;
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(AdminGoodsModel model)
        {
            if (ModelState.IsValid)
            {
                
                if (model.Picture != null)
                {
                    // получаем имя файла
                    string fileName = System.IO.Path.GetFileName(model.Picture.FileName);
                    // сохраняем файл в папку Files в проекте
                    model.Picture.SaveAs(Server.MapPath("~/Content/" + fileName));
                    model.PictrureName = fileName;
                }

                if (model.PictrureName == string.Empty) model.PictrureName = "No_image_available.png";

                Goods goods = new Goods()
                {
                    Id = model.Id,
                    AvailableCount = model.AvailableCount,
                    Name = model.Name,
                    CategoryId = model.CategoryId,
                    Color = model.Color,
                    Description = model.Description,
                    Height = model.Height,
                    Length = model.Length,
                    Width = model.Width,
                    Pictrure = model.PictrureName,
                    Price = model.Price
                };

                _repository.SaveGoods(goods);
                logger.Info($"User {User.Identity.Name} have changed Goods by id: {goods.Id}");

                return RedirectToAction("Index");
            }
            else
            {
                return View(model);
            }
        }

        public ViewResult Create()
        {
            return View("Edit", new AdminGoodsModel());
        }

        [HttpPost]
        public ActionResult Delete(int Id)
        {
            logger.Info($"User {User.Identity.Name} have deleted Goods with name: {_repository.Goods.First(g=>g.Id==Id).Name}");
            _repository.DeleteGoods(Id);
            return RedirectToAction("Index");
        }
    }
}