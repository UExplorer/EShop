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
    /// <summary>
    /// Controller for addin Reviews for Goods
    /// </summary>
    public class ReviewController : Controller
    {
        private IEshopRepository _repository;

        /// <summary>
        /// Setup db connection
        /// </summary>
        /// <param name="repo"></param>
        public ReviewController(IEshopRepository repo)
        {
            _repository = repo;
        }

        /// <summary>
        /// Adding new Review to Goods item
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult Add(ReviewModel model)
        {
            Review review = new Review()
            {
                Author = model.Author,
                DateTime = DateTime.Now,
                Goods = _repository.FindGoodsById(model.GoodsId),
                Stars = model.Stars,
                Text = model.Text
            };

            _repository.AddReview(review);
            return RedirectToAction("Index","Item", new {id = model.GoodsId});
        }
    }
}