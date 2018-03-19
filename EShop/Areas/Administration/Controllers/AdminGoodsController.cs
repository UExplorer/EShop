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
    /// <summary>
    /// Controller for creating and editing Goods. Available only for users with Admin Role
    /// </summary>
    [Authorize(Roles = "Admin")]
    public class AdminGoodsController : Controller
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        IEshopRepository _repository;

        /// <summary>
        /// Basic constuctor for current controller. Set connection with database and load Categories from it.
        /// </summary>
        /// <param name="repo"></param>
        public AdminGoodsController(IEshopRepository repo)
        {
            _repository = repo;
            ViewBag.Categories = GetCategories();
        }

        /// <summary>
        /// Displays all Goods from db into View
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View(_repository.GetGoods().ToList());
        }

        /// <summary>
        /// Return View for Goods item with specific id
        /// </summary>
        /// <param name="id">id number in Goods table</param>
        /// <returns></returns>
        public ViewResult Edit(int id)
        {
            Goods goods = _repository.GetGoods()
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

        /// <summary>
        /// Edit Goods item if it data is validated
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(AdminGoodsModel model)
        {
            if (ModelState.IsValid)
            {
                
                if (model.Picture != null)
                {
                    // Get file name
                    string fileName = System.IO.Path.GetFileName(model.Picture.FileName);
                    // Saving to Content folder
                    model.Picture.SaveAs(Server.MapPath("~/Content/Img/" + fileName));
                    model.PictrureName = fileName;
                }

                // Set standart image for Goods without image
                if (model.PictrureName == null
                    && _repository.GetGoods().ToList().First(g => g.Id == model.Id).Pictrure == "No_image_available.png")

                    model.PictrureName = "No_image_available.png";

                if (model.PictrureName == null &&
                    _repository.GetGoods().ToList().First(g => g.Id == model.Id).Pictrure != null)
                    model.PictrureName = _repository.GetGoods().ToList().First(g => g.Id == model.Id).Pictrure;

                if (model.AvailableCount < 0) model.AvailableCount = 0;

                Goods goods = _repository.GetGoods().First(g => g.Id == model.Id);
                goods.AvailableCount = model.AvailableCount;
                goods.Name = model.Name;
                goods.CategoryId = model.CategoryId;
                goods.Color = model.Color;
                goods.Description = model.Description;
                goods.Height = model.Height;
                goods.Length = model.Length;
                goods.Width = model.Width;
                goods.Pictrure = model.PictrureName;
                goods.Price = model.Price;

                // Saving current item and adding record to log file
                if (goods.Id != 0)
                {
                    _repository.EditGoods(goods);
                }
                else
                {
                    _repository.AddGoods(goods);
                }

                logger.Info($"User {User.Identity.Name} have changed Goods by id: {goods.Id}");

                return RedirectToAction("Index");
            }
            else
            {
                model.PictrureName = _repository.GetGoods().First(g => g.Id == model.Id).Pictrure;
                return View(model);
            }
        }

        /// <summary>
        /// Returns View for adding new item to db
        /// </summary>
        /// <returns></returns>
        public ViewResult Create()
        {
            return View("Edit", new AdminGoodsModel());
        }

        /// <summary>
        /// Delete Goods from db with specific id
        /// </summary>
        /// <param name="Id">Id of Goods item in db</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Delete(int Id)
        {
            logger.Info($"User {User.Identity.Name} have deleted Goods with name: {_repository.GetGoods().First(g=>g.Id==Id).Name}");
            _repository.DeleteGoods(Id);
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Load Categories from db
        /// </summary>
        /// <returns></returns>
        private IEnumerable<SelectListItem> GetCategories()
        {
            return _repository.GetCategories().Select(
                s => new SelectListItem
                {
                    Text = s.Name,
                    Value = s.Id.ToString(),
                    Disabled = false
                }).ToList();
        }
    }
}