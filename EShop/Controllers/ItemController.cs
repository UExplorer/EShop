﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EShop.Domain.Entities;
using EShop.Domain.Interfaces;
using EShop.Models;

namespace EShop.Controllers
{
    public class ItemController : Controller
    {
        readonly IEshopRepository _repository;

        public ItemController(IEshopRepository repo)
        {
            _repository = repo;
            ViewBag.Categories = _repository.Categories.ToList();
        }
        

        public ActionResult Index(int id)
        {
            return View(_repository.Goods.Single(i=>i.Id == id));
        }
    }
}