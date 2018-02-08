using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EShop.Domain.Entities;
using EShop.Domain.Interfaces;
using EShop.Exceptions;
using EShop.Models;

namespace EShop.Controllers
{
    /// <summary>
    /// Controller for displaying one selected Goods item
    /// </summary>
    [NullIdException]
    public class ItemController : Controller
    {
        readonly IEshopRepository _repository;

        /// <summary>
        /// Connection with db
        /// </summary>
        /// <param name="repo"></param>
        public ItemController(IEshopRepository repo)
        {
            _repository = repo;
            ViewBag.Categories = _repository.Categories.ToList();
        }
        
        /// <summary>
        /// Action return page with selected Goods item
        /// </summary>
        /// <param name="id">Id of Goods from db</param>
        /// <returns></returns>
        [NullIdException]
        public ActionResult Index(int id)
        {
            return View(_repository.Goods.Single(i=>i.Id == id));
        }
    }
}